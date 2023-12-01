using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using WebUI.Models.User;

namespace WebUI.Pages.User
{
    public class LoginModel : PageModel
    {
        public LoginMdl loginModel { get; set; }

        private readonly IUser _user;

        public LoginModel(IUser user)
        {
            _user = user;
        }

        [ViewData]
        public string AlertError { get; set; }

        public IActionResult OnGet()
        {
            var user = HttpContext.Session.GetString("userToken");

            if (user != null)
            {
                return new RedirectToPageResult("../Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostLoginAsync(LoginMdl loginModel)
        {
            LoginRequest loginRequest = new LoginRequest();

            loginRequest.Email = loginModel.Email;
            loginRequest.Password = loginModel.Password;

            var response = await _user.Login(loginRequest);

            if (response.Status)
            {
                var responseInfo = await _user.ListByMail("Bearer " + response.Data.Token,loginModel.Email);

                var serializeData = JsonConvert.SerializeObject(responseInfo.Data);

                HttpContext.Session.SetString("userInfo", serializeData);
                HttpContext.Session.SetString("userToken", response.Data.Token);

                return new RedirectToPageResult("../Index");
            }

            AlertError = response.Message;
            return Page();

        }
    }
}
