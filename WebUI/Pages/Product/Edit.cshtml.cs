using System.Globalization;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request;
using Core.Utilities.Refit.Models.Request.Product;
using Core.Utilities.Refit.Models.Response.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace WebUI.Pages.Product
{
    public class EditModel : PageModel
    {
        private readonly IProduct _product;
        private readonly ICategory _category;

        public EditModel(IProduct product, ICategory category)
        {
            _product = product;
            _category = category;
        }

        [ViewData]
        public string ToastrError { get; set; }

        [TempData]
        public string ToastrSuccess { get; set; }

        [BindProperty]
        public UpdateRequestModel UpdateRequestModel { get; set; }

        [BindProperty]
        public ImageRequestModel Image { get; set; }

        public List<SelectListItem> Options { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _product.ListById("Bearer " + HttpContext.Session.GetString("userToken"), id);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (response.Status)
            {
                UpdateRequestModel setModel = new UpdateRequestModel();

                setModel.Id = response.Data.Id;
                setModel.Name = response.Data.Name;
                setModel.PurchasePrice = response.Data.PurchasePrice.ToString(CultureInfo.InvariantCulture);
                setModel.SalePrice = response.Data.SalePrice.ToString(CultureInfo.InvariantCulture);
                setModel.Barcode = response.Data.Barcode;
                setModel.Image = response.Data.Image;
                setModel.Stock = response.Data.Stock;
                setModel.CriticalStock = response.Data.CriticalStock;
                setModel.Favorite = response.Data.Favorite;
                setModel.Status = response.Data.Status;
                setModel.CategoryId = response.Data.CategoryId;
                setModel.UpdateUserId = response.Data.UpdateUserId;
                setModel.Origin = response.Data.Origin;
				setModel.UnitType = response.Data.UnitType;
				setModel.UnitPrice = Convert.ToString(response.Data.UnitPrice, CultureInfo.InvariantCulture);

				UpdateRequestModel = setModel;

                await GetCategories(response.Data.CategoryId);

                return Page();
            }

            TempData["ToastrError"] = response.Message;

            return RedirectToPage("List");
        }

        public async Task<IActionResult> OnPostEditAsync(UpdateRequestModel updateRequestModel)
        {
            if (Image.File != null)
            {
                using var ms = new MemoryStream();
                await Image.File.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                updateRequestModel.Image = Convert.ToBase64String(fileBytes);
                updateRequestModel.ImageChanged = true;
            }

            updateRequestModel.UpdateUserId = JsonConvert.DeserializeObject<ViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            var response = await _product.Update("Bearer " + HttpContext.Session.GetString("userToken"), updateRequestModel);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (response.Status)
            {
                ToastrSuccess = response.Message;
                return RedirectToPage("Edit", updateRequestModel.Id);
            }

            await GetCategories(updateRequestModel.CategoryId);

            ToastrError = response.Message;

            return Page();
        }

        public async Task GetCategories(int categoryId)
        {
            var response = await _category.ListByActive("Bearer " + HttpContext.Session.GetString("userToken"));

            var list = (from item in response.Data let selected = categoryId == item.Id select new SelectListItem { Value = item.Id.ToString(), Text = item.Name, Selected = selected }).ToList();

            Options = list;
        }
    }
}
