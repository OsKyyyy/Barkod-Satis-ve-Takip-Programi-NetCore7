using Core.Utilities.Refit.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.Report
{
    public class SalesDeleteModel : PageModel
    {
        private readonly IReport _report;

        public SalesDeleteModel(IReport report)
        {
            _report = report;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _report.SalesDelete("Bearer " + HttpContext.Session.GetString("userToken"), id);

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

            return RedirectToPage("SalesDetailReport");
        }
    }
}
