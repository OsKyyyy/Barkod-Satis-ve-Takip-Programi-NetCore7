using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Response.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.Category
{
    public class ListModel : PageModel
    {
        private readonly ICategory _category;

        public ListModel(ICategory category)
        {
            _category = category;
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

            var response = await _category.List("Bearer " + HttpContext.Session.GetString("userToken"));

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
