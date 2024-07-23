using Business.Abstract;
using Core.Utilities.Refit.Models.Request.User;
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

			var result = _userService.Update(userUpdateDto);

			if (result.Status)
			{
				return Ok(result);
			}

			return BadRequest(result);
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

            var result = _userService.Delete(id);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
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

        [Route("AddRole")]
        [HttpPost]
        public ActionResult AddRole(RoleAddDto roleAddDto)
        {
            var checkExistsByBarcode = _userService.CheckExistsByName(roleAddDto.Name);
            if (!checkExistsByBarcode.Status)
            {
                return BadRequest(checkExistsByBarcode);
            }

            var operationClaim = _userService.AddOperationClaim(roleAddDto.Name);
            if (!operationClaim.Status)
            {
                return BadRequest(operationClaim);
            }

            var result = _userService.AddPageClaim(roleAddDto.SelectedItems, operationClaim.Data.Id);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("UpdateRole")]
        [HttpPut]
        public ActionResult UpdateRole(RoleUpdateDto roleUpdateDto)
        {
            var checkExistsByBarcode = _userService.CheckExistsByNameAndId(roleUpdateDto.Id, roleUpdateDto.Name);
            if (!checkExistsByBarcode.Status)
            {
                return BadRequest(checkExistsByBarcode);
            }

            var operationClaim = _userService.UpdateOperationClaim(roleUpdateDto.Id, roleUpdateDto.Name);
            if (!operationClaim.Status)
            {
                return BadRequest(operationClaim);
            }

            var resultRemove = _userService.DeletePageClaim(roleUpdateDto.Id);

            if (!resultRemove.Status)
            {
                return BadRequest(resultRemove);
            }

            var result = _userService.AddPageClaim(roleUpdateDto.SelectedItems, roleUpdateDto.Id);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("RoleList")]
        [HttpGet]
        public ActionResult RoleList()
        {
            var list = _userService.RoleList();
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }

        [Route("GetRoleById")]
        [HttpGet]
        public ActionResult GetRoleById(int id)
        {
            var list = _userService.GetRoleById(id);
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }

        [Route("GetRoleByName")]
        [HttpGet]
        public ActionResult GetRoleByName(string name)
        {
            var list = _userService.GetRoleByName(name);
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }
    }
}
