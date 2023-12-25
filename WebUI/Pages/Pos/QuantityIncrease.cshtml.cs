using Core.Utilities.Refit.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.Pos
{
    public class QuantityIncreaseModel : PageModel
    {
        private readonly IPos _pos;

        public QuantityIncreaseModel(IPos pos)
        {
            _pos = pos;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var session = SessionValues();

            if (session[1] == null)
            {
                return new RedirectToPageResult("../User/Login");
            }

            var response = await _pos.QuantityIncrease(SessionValues()[0], id);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return new RedirectToPageResult("../User/Login");
            }

            return RedirectToPage("Sale");
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
