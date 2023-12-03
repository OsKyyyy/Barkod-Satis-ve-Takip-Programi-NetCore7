using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace WebUI.Pages.User
{
    public class LoginModel : PageModel
    {
        private readonly IUser _user;

        public LoginModel(IUser user)
        {
            _user = user;
        }

        [ViewData]
        public string AlertError { get; set; }

        public LoginRequestModel loginModel { get; set; }

        public IActionResult OnGet()
        {
            var user = HttpContext.Session.GetString("userToken");

            if (user != null)
            {
                return new RedirectToPageResult("../Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostLoginAsync(LoginRequestModel loginModel)
        {
            var response = await _user.Login(loginModel);

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
