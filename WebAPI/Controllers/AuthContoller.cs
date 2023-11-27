﻿using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Route("Login")]
        [HttpPost]
        public ActionResult Login(UserLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Status)
            {
                return BadRequest(userToLogin);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("Register")]
        [HttpPost]
        public ActionResult Register(UserRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Status)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("GetInfoByMail")]
        [HttpGet]
        public ActionResult GetInfoByMail(string email)
        {
            var userToLogin = _authService.GetInfoByMail(email);
            if (!userToLogin.Status)
            {
                return BadRequest(userToLogin);
            }

            return Ok(userToLogin);
        }
    }
}
