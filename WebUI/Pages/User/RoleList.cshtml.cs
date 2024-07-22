using Core.Entities.Concrete;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.Role;
using Core.Utilities.Refit.Models.Request.User;
using Core.Utilities.Refit.Models.Response.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace WebUI.Pages.User
{
    public class RoleList : PageModel
    {
        private readonly IUser _user;
        
		public RoleList(IUser user)
        {
            _user = user;
		}

        [ViewData]
        public string ToastrError { get; set; }

        [TempData]
        public string ToastrSuccess { get; set; }

        public AddRoleRequestModel AddRoleRequestModel { get; set; }

        public UpdateRoleRequestModel UpdateRoleRequestModel { get; set; }

        public List<RoleListViewModel> RoleListViewModels { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await _user.RoleList("Bearer " + HttpContext.Session.GetString("userToken"));

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (response.Status)
            {
                RoleListViewModels = response.Data;
            }
            else
            {
                ToastrError = response.Message;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync(AddRoleRequestModel addRoleRequestModel)
        {
            var response = await _user.AddRole("Bearer " + HttpContext.Session.GetString("userToken"), addRoleRequestModel);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return new RedirectToPageResult("../User/Login");
            }

            if (response.Status)
            {
                ToastrSuccess = response.Message;
                return new RedirectToPageResult("RoleList");
            }

            await GetRoleList();
            ToastrError = response.Message;
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync(UpdateRoleRequestModel updateRoleRequestModel)
        {
            var response = await _user.UpdateRole("Bearer " + HttpContext.Session.GetString("userToken"), updateRoleRequestModel);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return new RedirectToPageResult("../User/Login");
            }

            if (response.Status)
            {
                ToastrSuccess = response.Message;
                return new RedirectToPageResult("RoleList");
            }

            await GetRoleList();
            ToastrError = response.Message;
            return Page();
        }

        public async Task GetRoleList()
        {
            var response = await _user.RoleList("Bearer " + HttpContext.Session.GetString("userToken"));

            RoleListViewModels = response.Data;
        }

        public async Task<IActionResult> OnPostGetRoleByNameAsync([FromBody] GetRoleByNameRequestModel getRoleByNameRequestModel)
        {
            var response = await _user.GetRoleByName("Bearer " + HttpContext.Session.GetString("userToken"), getRoleByNameRequestModel.Name);

            return new JsonResult(response);
        }
    }
}
