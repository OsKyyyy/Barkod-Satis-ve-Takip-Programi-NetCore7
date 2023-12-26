using System.Configuration;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request;
using Core.Utilities.Refit.Models.Request.Product;
using Core.Utilities.Refit.Models.Response.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebUI.Pages.Product
{
    public class AddModel : PageModel
    {
        private readonly IProduct _product;
        private readonly ICategory _category;

        public AddModel(IProduct product, ICategory category)
        {
            _product = product;
            _category = category;
        }

        [ViewData]
        public string ToastrError { get; set; }

        [TempData]
        public string ToastrSuccess { get; set; }
        
        public AddRequestModel AddRequestModel { get; set; }

        [BindProperty]
        public ImageRequestModel Image { get; set; }

        public List<SelectListItem> Options { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await _category.ListByActive("Bearer " + HttpContext.Session.GetString("userToken"));

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (response.Status)
            {
                var list = response.Data.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToList();

                Options = list;
            }
            else
            {
                ToastrError = response.Message;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync(AddRequestModel addRequestModel)
        {
            if (Image.File != null)
            {
                using var ms = new MemoryStream();
                await Image.File.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                addRequestModel.Image = Convert.ToBase64String(fileBytes);
            }

            addRequestModel.CreateUserId = JsonConvert.DeserializeObject<ViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            var response = await _product.Add("Bearer " + HttpContext.Session.GetString("userToken"), addRequestModel);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (response.Status)
            {
                ToastrSuccess = response.Message;
                return RedirectToPage("Add");
            }

            await GetCategoryList();

            ToastrError = response.Message;
            return Page();
        }

        public async Task GetCategoryList()
        {
            var responseCategory = await _category.List("Bearer " + HttpContext.Session.GetString("userToken"));

            var list = responseCategory.Data.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToList();

            Options = list;
        }
    }
}
