using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using WebUI.Models.User;

namespace WebUI.Pages.User
{
    public class AddModel : PageModel
    {
	    [ViewData]
	    public string ToastrError { get; set; }
	    [TempData]
	    public string ToastrSuccess { get; set; }

		public RegisterMdl registerModel { get; set; }

        private readonly IUser _user;
        
		public AddModel(IUser user)
        {
            _user = user;
		}

        public IActionResult OnGet()
        {
            var session = SessionValues();

			if (session[1] == null)
            {
                return new RedirectToPageResult("Login");
            }

            return Page();
        }
        
        public async Task<IActionResult> OnPostRegisterAsync(RegisterMdl registerModel)
        {
            RegisterRequest registerRequest = new RegisterRequest();

            registerRequest.FirstName = registerModel.FirstName;
            registerRequest.LastName = registerModel.LastName;
            registerRequest.Phone = registerModel.Phone;
            registerRequest.Email = registerModel.Email;
            registerRequest.Password = registerModel.Password;
            registerRequest.Status = registerModel.Status;

            var response = await _user.Register(SessionValues()[0], registerRequest);
            
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

	        string[] values = new string[2] { user, userInfo };
	        return values;
        }
    }
}
