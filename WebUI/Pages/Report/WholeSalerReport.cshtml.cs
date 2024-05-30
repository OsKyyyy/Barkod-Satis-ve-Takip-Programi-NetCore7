using Core.Entities.Concrete;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Response.Report;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebUI.Pages.Report
{
    public class WholeSalerReportModel : PageModel
    {
        private readonly IReport _report;

        public WholeSalerReportModel(IReport report)
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

        public async Task<IActionResult> OnPostGetLastWholeSalerWithDebtAsync()
        {
            var response = await _report.GetLastWholeSalerWithDebt("Bearer " + HttpContext.Session.GetString("userToken"));

            return new JsonResult(response);
        }

        public async Task<IActionResult> OnPostGetLastWholeSalerWithDebtPaymentAsync()
        {
            var response = await _report.GetLastWholeSalerWithDebtPayment("Bearer " + HttpContext.Session.GetString("userToken"));
            
            return new JsonResult(response);
        }

        public async Task<IActionResult> OnPostGetWholeSalerTotalDebtAsync()
        {
            var response = await _report.GetWholeSalerTotalDebt("Bearer " + HttpContext.Session.GetString("userToken"));

            return new JsonResult(response);
        }

        public async Task<IActionResult> OnPostGetWholeSalerDebtAsync()
        {
            var response = await _report.GetWholeSalerDebt("Bearer " + HttpContext.Session.GetString("userToken"));

            return new JsonResult(response);
        }

        public async Task<IActionResult> OnPostGetWholeSalerNonPayersAsync()
        {
            var response = await _report.GetWholeSalerNonPayers("Bearer " + HttpContext.Session.GetString("userToken"));

            return new JsonResult(response);
        }

        public async Task<IActionResult> OnPostGetWholeSalerThisMonthDebtAsync()
        {
            var response = await _report.GetWholeSalerThisMonthDebt("Bearer " + HttpContext.Session.GetString("userToken"));

            return new JsonResult(response);
        }

        public async Task<IActionResult> OnPostGetWholeSalerPreviousMonthDebtAsync()
        {
            var response = await _report.GetWholeSalerPreviousMonthDebt("Bearer " + HttpContext.Session.GetString("userToken"));

            return new JsonResult(response);
        }

        public async Task<IActionResult> OnPostGetWholeSalerMonthlyDebtOfOneYearAsync()
        {
            var response = await _report.GetWholeSalerMonthlyDebtOfOneYear("Bearer " + HttpContext.Session.GetString("userToken"));

            return new JsonResult(response);
        }
    }
}

