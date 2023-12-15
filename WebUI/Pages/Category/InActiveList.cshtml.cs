using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Response.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.Category
{
    public class InActiveListModel : PageModel
    {
        [ViewData]
        public string? ToastrError { get; set; }

        [ViewData]
        public string? ToastrSuccess { get; set; }

        public List<ViewModel> viewModel { get; set; }

        private readonly ICategory _category;

        public InActiveListModel(ICategory category)
        {
            _category = category;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            this.ToastrError = TempData["ToastrError"] as string;
            this.ToastrSuccess = TempData["ToastrSuccess"] as string;

            var session = SessionValues();

            if (session[1] == null)
            {
                return new RedirectToPageResult("../User/Login");
            }

            var response = await _category.InActiveList(SessionValues()[0]);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return new RedirectToPageResult("../User/Login");
            }

            if (response.Status)
            {
                viewModel = response.Data;
            }
            else
            {
                ToastrError = response.Message;
            }
            return Page();
        }

        public string[] SessionValues()
        {
            var user = "Bearer " + HttpContext.Session.GetString("userToken");
            var userInfo = HttpContext.Session.GetString("userInfo");

            string[] values = new string[2] { user, userInfo };
            return values;
        }
    }
}
