using Core.Utilities.Refit.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.WholeSaler
{
    public class DeleteMovementModel : PageModel
    {
        private readonly IWholeSalerMovement _wholeSalerMovement;

        public DeleteMovementModel(IWholeSalerMovement wholeSalerMovement)
        {
            _wholeSalerMovement = wholeSalerMovement;
        }

        public async Task<IActionResult> OnGetAsync(int wholeSalerId, int id)
        {
            var response = await _wholeSalerMovement.Delete("Bearer " + HttpContext.Session.GetString("userToken"), id);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (response.Status)
            {
                TempData["ToastrSuccess"] = response.Message;
            }
            else
            {
                TempData["ToastrError"] = response.Message;
            }

            return RedirectToPage("Movements", new { id = wholeSalerId });
        }
    }
}
