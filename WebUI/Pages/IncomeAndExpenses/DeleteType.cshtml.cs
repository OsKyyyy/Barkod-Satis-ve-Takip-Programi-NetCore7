using Core.Utilities.Refit.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.IncomeAndExpenses
{
    public class DeleteTypeModel : PageModel
    {
        private readonly IIncomeAndExpenses _incomeAndExpenses;

        public DeleteTypeModel(IIncomeAndExpenses incomeAndExpenses)
        {
            _incomeAndExpenses = incomeAndExpenses;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _incomeAndExpenses.DeleteType("Bearer " + HttpContext.Session.GetString("userToken"), id);

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

            return RedirectToPage("ListType");
        }
    }
}
