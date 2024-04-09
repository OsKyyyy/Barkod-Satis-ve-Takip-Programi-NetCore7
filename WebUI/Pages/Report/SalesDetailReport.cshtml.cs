using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.Pos;
using Core.Utilities.Refit.Models.Response.Report;
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

        [BindProperty]
        public DateTime Datee { get; set; }

        [ViewData]
        public string? ToastrError { get; set; }

        [ViewData]
        public string? ToastrSuccess { get; set; }

        public List<SalesDetailReportViewModel> SalesDetailReportViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(DateTime? dateTime)
        {
            ToastrError = TempData["ToastrError"] as string;
            ToastrSuccess = TempData["ToastrSuccess"] as string;

            if (dateTime == null)
            {
                dateTime = DateTime.Now;
            }
            
            var response = await _report.SalesDetailReport("Bearer " + HttpContext.Session.GetString("userToken"), DateTime.Now);

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

