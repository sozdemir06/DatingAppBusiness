using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.Business.Abstract;
using DatingApp.Business.Mappings.AutoMapper.Dtos;
using DatingApp.Business.ValidationRules.FluentValidation;
using DatingApp.Core.Aspects.PostSharp.ValidationAspects;
using DatingApp.Core.Utilities.Helpers.AuthHelpers;
using DatingApp.DataAccess.Abstract;
using DatingApp.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;


namespace DatingApp.Business.Concrete.Managers
{
    public class UserManager : IUserService
    {
        private readonly IUserDal userDal;
        private readonly IMapper mapper;
        private readonly IAuthHelper authHelper;
        public UserManager(IUserDal userDal, IMapper mapper,IAuthHelper authHelper)
        {
            this.authHelper = authHelper;
            this.mapper = mapper;
            this.userDal = userDal;

        }

        [FluentValidationAspect(typeof(UserForLoginDtoValidator))]
        public async Task<UserForReturnTokenDto> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await userDal.Get(u => u.Email == userForLoginDto.Email);
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
            string[] userRoles=new string[]{};

            var token = authHelper.GenerateJwtToken(userFromRepo.Id,userFromRepo.UserName,userRoles);
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
            if (!saveUser)
            {
                throw new Exception("Could not registered.!!");
            }

            var userToReturn = mapper.Map<UserForDetailedDto>(userForCreate);
            return userToReturn;
        }
    }
}