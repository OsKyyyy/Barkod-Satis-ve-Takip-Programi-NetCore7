using Core.Utilities.Refit.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.Category
{
    public class DeleteModel : PageModel
    {
        private readonly ICategory _category;

        public DeleteModel(ICategory category)
        {
            _category = category;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _category.Delete("Bearer " + HttpContext.Session.GetString("userToken"), id);

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

            return RedirectToPage("List");
        }
    }
}
