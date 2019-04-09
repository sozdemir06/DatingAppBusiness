using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.Business.Abstract;
using DatingApp.Business.Extensions;
using DatingApp.Business.Mappings.AutoMapper.Dtos;
using DatingApp.Business.ValidationRules.FluentValidation;
using DatingApp.Core.Aspects.PostSharp.LogAspects;
using DatingApp.Core.Aspects.PostSharp.ValidationAspects;
using DatingApp.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using DatingApp.Core.Utilities.Helpers.AppHeaderHelper;
using DatingApp.Core.Utilities.Helpers.AuthHelpers;
using DatingApp.DataAccess.Abstract;
using DatingApp.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;


namespace DatingApp.Business.Concrete.Managers
{
    public class UserManager : IUserService
    {
        private readonly IUserDal userDal;
        private readonly IMapper mapper;
        private readonly IAuthHelper authHelper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILikeDal likeDal;
        private readonly IUserRoleDal userRoleDal;
        public UserManager(IUserDal userDal, IMapper mapper, IAuthHelper authHelper, IHttpContextAccessor httpContextAccessor, ILikeDal likeDal, IUserRoleDal userRoleDal)
        {
            this.userRoleDal = userRoleDal;
            this.likeDal = likeDal;
            this.httpContextAccessor = httpContextAccessor;
            this.authHelper = authHelper;
            this.mapper = mapper;
            this.userDal = userDal;

        }

        public async Task<UserForDetailedDto> GetUser(int userId)
        {
            var user = await userDal.GetUserWithPhotos(userId);
            if (user == null)
            {
                throw new Exception("User not found.!!");
            }
            var userMap = mapper.Map<UserForDetailedDto>(user);
            return userMap;
        }

        public async Task<IEnumerable<UserForListDto>> GetUserLikers(HttpResponse response, UserParams userParams)
        {
            var currentUserId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            userParams.UserId = currentUserId;
            if (!userParams.Likers && !userParams.Likees)
            {
                throw new Exception("Not Found.!!");
            }
            var userFromRepo = await userDal.Get(u => u.Id == userParams.UserId);

            if (string.IsNullOrEmpty(userParams.Gender))
            {
                userParams.Gender = userFromRepo.Gender == "male" ? "female" : "male";
            }

            var users = await userDal.GetUserLikers(userParams);
            if (userDal == null)
            {
                throw new Exception("Not found any Likers or Likees.!!");
            }

            var userForReturn = mapper.Map<IEnumerable<UserForListDto>>(users);
            response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            return userForReturn;

        }

        [LogAspect(typeof(PostgreSqlLogger))]
        public async Task<IEnumerable<UserForListDto>> GetUSersWithPhotos(HttpResponse response, UserParams userParams)
        {
            var currentUserId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userFromRepo = await userDal.GetUserWithPhotos(currentUserId);

            userParams.UserId = currentUserId;

            if (string.IsNullOrEmpty(userParams.Gender))
            {
                userParams.Gender = userFromRepo.Gender == "male" ? "female" : "male";
            }

            var users = await userDal.GetUsersWithPhotos(userParams);

            if (users == null)
            {
                throw new Exception("Failed to retrieve user list.!!");
            }
            var userForReturn = mapper.Map<IEnumerable<UserForListDto>>(users);
            response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            return userForReturn;
        }

       
        public async Task<UserWithRolesDto> GetUserWithRoles(int userId)
        {
            if (userId != int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                throw new Exception("You can't access this profile.!!");
            }
            var userWithRolesFromRepo = await userDal.GetUserWithRoles(userId);
            var userWithRolesForReturn = mapper.Map<UserWithRolesDto>(userWithRolesFromRepo);
            return userWithRolesForReturn;
        }

        public async Task<IEnumerable<UserWithRolesDto>> GetUsersWithRoles()
        {

            var userWtihRoles = await userDal.GetUsersWithRoles();
            if (userWtihRoles == null)
            {
                throw new Exception("Failed to retrieve users list with their roles.!!");
            }

            var userWithRolesForReturn = mapper.Map<IEnumerable<UserWithRolesDto>>(userWtihRoles);
            return userWithRolesForReturn;
        }

        [FluentValidationAspect(typeof(UserForLoginDtoValidator))]
        public async Task<UserForReturnTokenDto> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await userDal.CheckUserForLogin(userForLoginDto.Email);
            if (userFromRepo == null)
            {
                throw new Exception("No registered users with this email address");
            }

            var checkPassword = authHelper.VerifyPasswordHash(userForLoginDto.Password, userFromRepo.PasswordHash, userFromRepo.PasswordSalt);
            if (!checkPassword)
            {
                throw new Exception("Your Email address or password are Incorrect");
            }

            var userToReturn = mapper.Map<UserForReturnTokenDto>(userFromRepo);
            var userRoles = await userDal.GetUserWithRoles(userFromRepo.Id);
            var userWithTheirRoles = mapper.Map<UserWithRolesDto>(userRoles);

            List<string> userRolesList = new List<string>();
            userRolesList.AddRange(userWithTheirRoles.UserRoles.Select(t => t.RoleName));

            var token = authHelper.GenerateJwtToken(userFromRepo.Id, userFromRepo.UserName, userRolesList);
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Jwt token Unable to created.!!");
            }
            userToReturn.Token = token;
            return userToReturn;
        }




        [FluentValidationAspect(typeof(UserForRegisterDtoValidator))]
        public async Task<UserForDetailedDto> Register(UserForRegisterDto userForRegisterDto)
        {
            var userForCreate = mapper.Map<User>(userForRegisterDto);

            byte[] passwordHash, passwordSalt;
            authHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);

            userForCreate.PasswordHash = passwordHash;
            userForCreate.PasswordSalt = passwordSalt;

            var userExist = await userDal.UserExist(userForCreate.Email);
            if (userExist)
            {
                throw new Exception("This Email address is already registered.!!");
            }
            var saveUser = await userDal.Add(userForCreate);
            if (saveUser == null)
            {
                throw new Exception("Could not registered.!!");
            }

            var addDefaultRole=new UserRole()
            {
                UserId=userForCreate.Id,
                RoleId=1
            };

            var addRoles=await userRoleDal.Add(addDefaultRole);
            if(addRoles==null)
            {
                throw new Exception("User created but cant added");
            }

            var userToReturn = mapper.Map<UserForDetailedDto>(userForCreate);
            return userToReturn;
        }

        [FluentValidationAspect(typeof(UserForUpdateDtoValidator))]
        public async Task<UserForUpdateDto> UpdateUser(int userId, UserForUpdateDto userForUpdateDto)
        {
            if (userId != int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                throw new Exception("You can't update this profile because the profile does not belog to you.!!");
            }

            var userFromRepo = await userDal.Get(u => u.Id == userId);
            if (userFromRepo == null)
            {
                throw new Exception("User profile could't be find.!!");
            }

            var userForUpdate = mapper.Map(userForUpdateDto, userFromRepo);
            var updateUser = await userDal.Update(userForUpdate);
            if (!updateUser)
            {
                throw new Exception("Your profile could not be updated");
            }

            return userForUpdateDto;

        }

        public async Task<UserWithRolesDto> EditUserRoles(UserEditRolesDto userEditRolesDto)
        {
            var userRolesFromRepo = await userDal.GetUserWithRoles(userEditRolesDto.UserId);
            var mappedUserRoles=mapper.Map<UserWithRolesDto>(userRolesFromRepo);
            var userFromRepo = await userDal.Get(u => u.Id == userEditRolesDto.UserId);

            var selectedRoles = userEditRolesDto.RoleId;
            selectedRoles = selectedRoles ?? new int[]{};

            var addroles=new UserRole();
                
            foreach (var item in selectedRoles.Except(mappedUserRoles.UserRoles.Select(p=>p.Id)))
            {
               addroles= await userRoleDal.Add(new UserRole{UserId=userFromRepo.Id,RoleId=item});

            }

            if(addroles==null)
            {
                throw new Exception("Can't added user roles.!!");
            }

            var userroles=mappedUserRoles.UserRoles.Select(p=>p.Id);
            bool removeRoles=false;
           foreach (var item in userroles.Except(selectedRoles))
           {
               removeRoles= await userRoleDal.Delete(new UserRole{UserId=userFromRepo.Id,RoleId=item});
           }
           if(!removeRoles)
           {
               throw new Exception("Could not remove old user roles.!!");
           }

           return mapper.Map<UserWithRolesDto>(await userDal.GetUserWithRoles(userFromRepo.Id));


        }
    }
}