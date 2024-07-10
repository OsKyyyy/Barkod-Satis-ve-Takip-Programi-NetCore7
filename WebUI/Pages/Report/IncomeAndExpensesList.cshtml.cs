using Core.Entities.Concrete;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Response.Report;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebUI.Pages.Report
{
    public class IncomeAndExpensesListModel : PageModel
    {
        private readonly IReport _report;

        public IncomeAndExpensesListModel(IReport report)
        {
            _report = report;
        }

        [ViewData]
        public string? ToastrError { get; set; }

        [ViewData]
        public string? ToastrSuccess { get; set; }

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

        public async Task<IActionResult> OnPostYearlyExternalIncomeAsync()
        {
            var response = await _report.YearlyExternalIncome("Bearer " + HttpContext.Session.GetString("userToken"));

            return new JsonResult(response);
        }

        public async Task<IActionResult> OnPostYearlyExternalExpensesAsync()
        {
            var response = await _report.YearlyExternalExpenses("Bearer " + HttpContext.Session.GetString("userToken"));

            return new JsonResult(response);
        }
        
        public async Task<IActionResult> OnPostYearlySalesIncomeAsync()
        {
            var response = await _report.YearlySalesIncome("Bearer " + HttpContext.Session.GetString("userToken"));

            return new JsonResult(response);
        }
        
        public async Task<IActionResult> OnPostYearlyWholeSalerExpensesAsync()
        {
            var response = await _report.YearlyWholeSalerExpenses("Bearer " + HttpContext.Session.GetString("userToken"));

            return new JsonResult(response);
        }
    }
}

