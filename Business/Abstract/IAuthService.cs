﻿using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.JWT;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserLoginDto userForLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
