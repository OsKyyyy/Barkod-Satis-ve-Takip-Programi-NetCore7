﻿using Business.Abstract;
using Business.Constant;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation.User;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        [SecuredOperation("user_add")]
		[ValidationAspect(typeof(RegisterValidator))]
        public IDataResult<User> Register(UserRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Phone = userForRegisterDto.Phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = userForRegisterDto.Status,
                Deleted = false
            };
            var userId = _userService.Add(user);
            user.Id = userId;

            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        [ValidationAspect(typeof(LoginValidator))]
        public IDataResult<User> Login(UserLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email, true);

            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorDataResult<User>(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
    }
}
