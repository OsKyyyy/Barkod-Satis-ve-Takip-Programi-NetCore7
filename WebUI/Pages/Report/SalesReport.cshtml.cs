using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Response.Report;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.Report
{
    public class SalesReportModel : PageModel
    {
        private readonly IReport _report;

        public SalesReportModel(IReport report)
        {
            _report = report;
        }

        [ViewData]
        public string? ToastrError { get; set; }

        [ViewData]
        public string? ToastrSuccess { get; set; }

        public List<SalesReportViewModel> SalesReportViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ToastrError = TempData["ToastrError"] as string;
            ToastrSuccess = TempData["ToastrSuccess"] as string;

            var response = await _report.SalesReport("Bearer " + HttpContext.Session.GetString("userToken"));

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (response.Status)
            {
                SalesReportViewModel = response.Data;
            }
            else
            {
                ToastrError = response.Message;
            }

            return Page();
        }
    }
}
