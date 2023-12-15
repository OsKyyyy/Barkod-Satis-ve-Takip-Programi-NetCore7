using Core.Utilities.Refit.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.Product
{
    public class DeleteModel : PageModel
    {
        private readonly IProduct _product;

        public DeleteModel(IProduct product)
        {
            _product = product;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var session = SessionValues();

            if (session[1] == null)
            {
                return new RedirectToPageResult("Login");
            }

            var response = await _product.Delete(SessionValues()[0], id);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return new RedirectToPageResult("../User/Login");
            }

            if (response.Status)
            {
                TempData["ToastrSuccess"] = response.Message;
                return RedirectToPage("List");

            }
            else
            {
                TempData["ToastrError"] = response.Message;
                return RedirectToPage("List");
            }
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
