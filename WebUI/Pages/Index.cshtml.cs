using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebUI.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            var userInfo = HttpContext.Session.GetString("userInfo");
            var user = HttpContext.Session.GetString("userToken");

            if (user == null || userInfo == null)
            {
                return new RedirectToPageResult("User/Login");
            }

            return Page();
        }
    }
}