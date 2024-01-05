using Core.Utilities.Refit.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.Customer
{
    public class DeleteMovementModel : PageModel
    {
        private readonly ICustomerMovement _customerMovement;

        public DeleteMovementModel(ICustomerMovement customerMovement)
        {
            _customerMovement = customerMovement;
        }

        public async Task<IActionResult> OnGetAsync(int customerId, int id)
        {
            var response = await _customerMovement.Delete("Bearer " + HttpContext.Session.GetString("userToken"), id);

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

            return RedirectToPage("Movements", new { id = customerId });
        }
    }
}
