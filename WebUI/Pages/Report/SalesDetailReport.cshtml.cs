using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.Pos;
using Core.Utilities.Refit.Models.Response.Report;
using Core.Utilities.Refit.Models.Response.Sale;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebUI.Pages.Report
{
    public class SalesDetailReportModel : PageModel
    {
        private readonly IReport _report;

        public SalesDetailReportModel(IReport report)
        {
            _report = report;
        }

        [ViewData]
        public string? ToastrError { get; set; }

        [ViewData]
        public string? ToastrSuccess { get; set; }

        public List<ViewModel> SalesDetailReportViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(DateTime date)
        {
            ToastrError = TempData["ToastrError"] as string;
            ToastrSuccess = TempData["ToastrSuccess"] as string;

            if (date.Year == 1)
            {
                date = DateTime.Now;
            }

            var response = await _report.SalesDetailReport("Bearer " + HttpContext.Session.GetString("userToken"), date);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (response.Status)
            {
                SalesDetailReportViewModel = response.Data;
            }
            else
            {
                ToastrError = response.Message;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostListAsync([FromBody] SaleDetailRequestModel saleDetailRequestModel)
        {
            var response = await _report.SalesDetailReport("Bearer " + HttpContext.Session.GetString("userToken"), saleDetailRequestModel.Date);

            return new JsonResult(response);
        }
    }
}

