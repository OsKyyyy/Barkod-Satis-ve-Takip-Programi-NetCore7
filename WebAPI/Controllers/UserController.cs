using Business.Abstract;
using Entities.Dtos;
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

        [Route("Update")]
        [HttpPut]
        public ActionResult Update(UserUpdateDto userUpdateDto)
        {
            var userExists = _userService.UserExistsByUpdate(userUpdateDto.Email, userUpdateDto.Id);
            if (!userExists.Status)
            {
                return BadRequest(userExists);
            }

			var updateResult = _userService.Update(userUpdateDto);

			if (updateResult.Status)
			{
				return Ok(updateResult);
			}

			return BadRequest(updateResult);
		}

        [Route("Delete")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var listById = _userService.ListById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var deleteResult = _userService.Delete(id);

            if (deleteResult.Status)
            {
                return Ok(deleteResult);
            }

            return Ok(deleteResult);
        }

        [Route("List")]
		[HttpGet]
		public ActionResult List()
		{
			var list = _userService.List();
			if (!list.Status)
			{
				return BadRequest(list);
			}

			return Ok(list);
		}

		[Route("ListByMail")]
		[HttpGet]
		public ActionResult ListByMail(string email)
		{
			var listByMail = _userService.ListByMail(email);
			if (!listByMail.Status)
			{
				return BadRequest(listByMail);
			}

			return Ok(listByMail);
		}

        [Route("ListById")]
        [HttpGet]
        public ActionResult ListById(int id)
        {
            var listById = _userService.ListById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            return Ok(listById);
        }
    }
}
