using Business.Abstract;
using Core.Entities.Concrete;
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
        private IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
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
                return BadRequest(userExists);
            }

            var result = _authService.Register(userForRegisterDto, userForRegisterDto.Password);

            if (!result.Status)
            {
                return BadRequest(result);
            }

            var userRoleUpdateDto = new UserRoleUpdateDto
            {
                Id = result.Data.Id,
                OperationClaimId = userForRegisterDto.Role
            };

            var resultRole = _userService.AddUserOperationClaim(userRoleUpdateDto);

            if (resultRole.Status)
            {
                return Ok(result);
            }

            return Ok(result);
        }
    }
}
