using Core.Entities.Concrete;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace WebUI.Pages.User
{
    public class AddModel : PageModel
    {
        private readonly IUser _user;
        
		public AddModel(IUser user)
        {
            _user = user;
		}

        [ViewData]
        public string ToastrError { get; set; }

        [TempData]
        public string ToastrSuccess { get; set; }

        public RegisterRequestModel RegisterModel { get; set; }

        public List<SelectListItem> Options { get; set; }

        public IActionResult OnGet()
        {
            //var response = await _category.ListByActive("Bearer " + HttpContext.Session.GetString("userToken"));

            //if (response.Message == "Authentication Error")
            //{
            //    HttpContext.Session.Remove("userToken");
            //    HttpContext.Session.Remove("userInfo");

            //    return RedirectToPage("../User/Login");
            //}

            //if (response.Status)
            //{
            //    var list = response.Data.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToList();

            //    Options = list;
            //}
            //else
            //{
            //    ToastrError = response.Message;
            //}

            return Page();
        }
        
        public async Task<IActionResult> OnPostRegisterAsync(RegisterRequestModel registerModel)
        {
            var response = await _user.Register("Bearer " + HttpContext.Session.GetString("userToken"), registerModel);
            
            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("Login");
            }

            if (response.Status)
            {
                ToastrSuccess = response.Message;
                return RedirectToPage("Add");
            }

            await GetOperationList();

            ToastrError = response.Message;
            return Page();
        }

        public async Task GetOperationList()
        {
            //var responseCategory = await _category.ListByActive("Bearer " + HttpContext.Session.GetString("userToken"));

            //var list = responseCategory.Data.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToList();

            //Options = list;
        }
    }
}
