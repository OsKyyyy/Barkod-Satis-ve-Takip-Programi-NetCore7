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
            var response = await _pos.QuantityIncrease("Bearer " + HttpContext.Session.GetString("userToken"), id);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (!response.Status)
            {
                TempData["ToastrError"] = response.Message;
            }

            return RedirectToPage("Sale");
        }
    }
}
