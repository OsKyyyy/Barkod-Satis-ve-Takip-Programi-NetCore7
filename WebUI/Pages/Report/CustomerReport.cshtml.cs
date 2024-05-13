using Core.Entities.Concrete;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Response.Report;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebUI.Pages.Report
{
    public class CustomerReportModel : PageModel
    {
        private readonly IReport _report;

        public CustomerReportModel(IReport report)
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

        public async Task<IActionResult> OnPostGetLastCustomerWithDebtAsync()
        {
            var response = await _report.GetLastCustomerWithDebt("Bearer " + HttpContext.Session.GetString("userToken"));

            return new JsonResult(response);
        }

        public async Task<IActionResult> OnPostGetLastCustomerWithDebtPaymentAsync()
        {
            var response = await _report.GetLastCustomerWithDebtPayment("Bearer " + HttpContext.Session.GetString("userToken"));

            return new JsonResult(response);
        }

        public async Task<IActionResult> OnPostGetCustomerTotalDebtAsync()
        {
            var response = await _report.GetCustomerTotalDebt("Bearer " + HttpContext.Session.GetString("userToken"));

            return new JsonResult(response);
        }

        public async Task<IActionResult> OnPostGetCustomerDebtAsync()
        {
            var response = await _report.GetCustomerDebt("Bearer " + HttpContext.Session.GetString("userToken"));

            return new JsonResult(response);
        }

        public async Task<IActionResult> OnPostGetCustomerNonPayersAsync()
        {
            var response = await _report.GetCustomerNonPayers("Bearer " + HttpContext.Session.GetString("userToken"));

            return new JsonResult(response);
        }
    }
}

