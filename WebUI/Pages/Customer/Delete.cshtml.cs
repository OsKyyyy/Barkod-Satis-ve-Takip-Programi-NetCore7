using Core.Utilities.Refit.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.Customer
{
    public class DeleteModel : PageModel
    {
        private readonly ICustomer _customer;

        public DeleteModel(ICustomer customer)
        {
            _customer = customer;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _customer.Delete("Bearer " + HttpContext.Session.GetString("userToken"), id);

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

            return RedirectToPage("List");
        }
    }
}
