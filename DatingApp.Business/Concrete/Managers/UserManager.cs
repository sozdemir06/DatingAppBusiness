using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.Business.Abstract;
using DatingApp.Business.Extensions;
using DatingApp.Business.Mappings.AutoMapper.Dtos;
using DatingApp.Business.ValidationRules.FluentValidation;
using DatingApp.Core.Aspects.PostSharp.ValidationAspects;
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
        public UserManager(IUserDal userDal, IMapper mapper, IAuthHelper authHelper, IHttpContextAccessor httpContextAccessor)
        {
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

        public async Task<IEnumerable<UserForListDto>> GetUSersWithPhotos(HttpResponse response,UserParams userParams)
        {
            var currentUserId=int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userFromRepo=await userDal.GetUserWithPhotos(currentUserId);

            userParams.UserId=currentUserId;

            if(string.IsNullOrEmpty(userParams.Gender)){
                userParams.Gender=userFromRepo.Gender=="male"?"female":"male";
            }

            var minDob=DateTime.Now;
            var maxDob=DateTime.Now;
            if(userParams.MinAge!=18 || userParams.MaxAge!=99)
            {
               minDob=DateTime.Today.AddYears(-userParams.MaxAge-1);
               maxDob=DateTime.Today.AddYears(-userParams.MinAge);
            }

            var users = await userDal.GetUsersWithPhotos(userParams,minDob,maxDob);

            if (users == null)
            {
                throw new Exception("Failed to retrieve user list.!!");
            }
            var userForReturn = mapper.Map<IEnumerable<UserForListDto>>(users);
            response.AddPagination(users.CurrentPage,users.PageSize,users.TotalCount,users.TotalPages);    
            return userForReturn;
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
            string[] userRoles = new string[] { };

            var token = authHelper.GenerateJwtToken(userFromRepo.Id, userFromRepo.UserName, userRoles);
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
            if (saveUser==null)
            {
                throw new Exception("Could not registered.!!");
            }

            var userToReturn = mapper.Map<UserForDetailedDto>(userForCreate);
            return userToReturn;
        }

        [FluentValidationAspect(typeof(UserForUpdateDtoValidator))]
        public async Task<UserForUpdateDto> UpdateUser(int userId, UserForUpdateDto userForUpdateDto)
        {
                if(userId!=int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))
                {
                    throw new Exception("You can't update this profile because the profile does not belog to you.!!");
                }

                var userFromRepo=await userDal.Get(u=>u.Id==userId);
                if(userFromRepo==null)
                {
                    throw new Exception("User profile could't be find.!!");
                }

                var userForUpdate=mapper.Map(userForUpdateDto,userFromRepo);
                var updateUser=await userDal.Update(userForUpdate);
                if(!updateUser)
                {
                    throw new Exception("Your profile could not be updated");
                }

                return userForUpdateDto;    

        }
    }
}