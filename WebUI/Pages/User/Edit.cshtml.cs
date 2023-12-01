using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request;
using Core.Utilities.Refit.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Models.User;

namespace WebUI.Pages.User
{
    public class EditModel : PageModel
    {
        [ViewData]
        public string ToastrError { get; set; }

        [TempData]
        public string ToastrSuccess { get; set; }

        public UserInfo UserList { get; set; }

        public EditMdl editModel { get; set; }

        private readonly IUser _user;

        public EditModel(IUser user)
        {
            _user = user;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var session = SessionValues();

            if (session[1] == null)
            {
                return new RedirectToPageResult("Login");
            }

            var response = await _user.ListById(SessionValues()[0], id);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return new RedirectToPageResult("Login");
            }

            if (response.Status)
            {
                UserList = response.Data;
            }
            else
            {
                ToastrError = response.Message;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostEditAsync(UserInfo userInfo)
        {
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
