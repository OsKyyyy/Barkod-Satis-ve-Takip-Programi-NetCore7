using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : Controller
	{
		private IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[Route("List")]
		[HttpGet]
		public ActionResult List()
		{
			var userToLogin = _userService.List();
			if (!userToLogin.Status)
			{
				return BadRequest(userToLogin);
			}

			return Ok(userToLogin);
		}

		[Route("ListByMail")]
		[HttpGet]
		public ActionResult ListByMail(string email)
		{
			var userToLogin = _userService.ListByMail(email);
			if (!userToLogin.Status)
			{
				return BadRequest(userToLogin);
			}

			return Ok(userToLogin);
		}

        [Route("ListById")]
        [HttpGet]
        public ActionResult ListById(int id)
        {
            var userToLogin = _userService.ListById(id);
            if (!userToLogin.Status)
            {
                return BadRequest(userToLogin);
            }

            return Ok(userToLogin);
        }
    }
}
