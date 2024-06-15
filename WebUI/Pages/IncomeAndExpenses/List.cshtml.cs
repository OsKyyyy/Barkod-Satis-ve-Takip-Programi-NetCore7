using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Response.IncomeAndExpenses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.IncomeAndExpenses
{
    public class ListModel : PageModel
    {
        private readonly IIncomeAndExpenses _incomeAndExpenses;

        public ListModel(IIncomeAndExpenses incomeAndExpenses)
        {
            _incomeAndExpenses = incomeAndExpenses;
        }

        [ViewData]
        public string? ToastrError { get; set; }

        [ViewData]
        public string? ToastrSuccess { get; set; }

        public List<ViewModel> ViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ToastrError = TempData["ToastrError"] as string;
            ToastrSuccess = TempData["ToastrSuccess"] as string;

            var response = await _incomeAndExpenses.List("Bearer " + HttpContext.Session.GetString("userToken"));

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (response.Status)
            {
                ViewModel = response.Data;
            }
            else
            {
                ToastrError = response.Message;
            }

            return Page();
        }
    }
}
