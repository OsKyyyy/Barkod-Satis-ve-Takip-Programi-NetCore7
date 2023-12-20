using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public IActionResult OnGet()
        {
            var session = SessionValues();

			if (session[1] == null)
            {
                return new RedirectToPageResult("Login");
            }

            return Page();
        }
        
        public async Task<IActionResult> OnPostRegisterAsync(RegisterRequestModel registerModel)
        {
            var response = await _user.Register(SessionValues()[0], registerModel);
            
            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return new RedirectToPageResult("Login");
            }

            if (response.Status)
            {
                ToastrSuccess = response.Message;
                return new RedirectToPageResult("Add");
            }

            ToastrError = response.Message;
            return Page();
        }

        public string[] SessionValues()
        {
	        var user = "Bearer " + HttpContext.Session.GetString("userToken");
	        var userInfo = HttpContext.Session.GetString("userInfo");

	        var values = new string[2] { user, userInfo };
	        return values;
        }
    }
}
