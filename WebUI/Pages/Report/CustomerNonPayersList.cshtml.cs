using Core.Entities.Concrete;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Response.Report;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebUI.Pages.Report
{
    public class CustomerNonPayersListModel : PageModel
    {
        private readonly IReport _report;

        public CustomerNonPayersListModel(IReport report)
        {
            _report = report;
        }

        [ViewData]
        public string? ToastrError { get; set; }

        [ViewData]
        public string? ToastrSuccess { get; set; }

        public List<CustomerNonPayersViewModel> ViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ToastrError = TempData["ToastrError"] as string;
            ToastrSuccess = TempData["ToastrSuccess"] as string;      

            var response = await _report.GetCustomerNonPayers("Bearer " + HttpContext.Session.GetString("userToken"));

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

