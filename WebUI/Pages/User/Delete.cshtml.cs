using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Response.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.User
{
    public class DeleteModel : PageModel
    {
        private readonly IUser _user;

        public DeleteModel(IUser user)
        {
            _user = user;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _user.Delete("Bearer " + HttpContext.Session.GetString("userToken"), id);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("Login");
            }

            if (response.Status)
            {
                TempData["ToastrSuccess"] = response.Message;
            }
            else
            {
                TempData["ToastrError"] = response.Message;
            }

            return RedirectToPage("List");
        }
    }
}
