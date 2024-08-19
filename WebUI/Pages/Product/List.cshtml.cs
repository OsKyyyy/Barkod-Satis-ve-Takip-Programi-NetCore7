using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Response.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.Product
{
    public class ListModel : PageModel
    {
        private readonly IProduct _product;

        public ListModel(IProduct product)
        {
            _product = product;
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

            var response = await _product.List("Bearer " + HttpContext.Session.GetString("userToken"));

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

        public async Task<IActionResult> OnGetProductAsync(int productId)
        {
            var response = await _product.ListById("Bearer " + HttpContext.Session.GetString("userToken"), productId);

            return new JsonResult(response);
        }
    }
}
