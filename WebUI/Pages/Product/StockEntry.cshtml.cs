using System.Configuration;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request;
using Core.Utilities.Refit.Models.Request.Product;
using Core.Utilities.Refit.Models.Response.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebUI.Pages.Product
{
    public class StockEntryModel : PageModel
    {
        private readonly IProduct _product;
        private readonly IWholeSaler _wholeSaler;

        public StockEntryModel(IProduct product, IWholeSaler wholeSaler)
        {
            _product = product;
            _wholeSaler = wholeSaler;
        }

        [ViewData]
        public string ToastrError { get; set; }

        [TempData]
        public string ToastrSuccess { get; set; }
        
        public StockEntryRequestModel StockEntryRequestModel { get; set; }

        public List<SelectListItem> Options { get; set; }

        [BindProperty]
        public ImageRequestModel Image { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await _wholeSaler.List("Bearer " + HttpContext.Session.GetString("userToken"));

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

        public async Task<IActionResult> OnPostStockEntrAsync(StockEntryRequestModel stockEntryRequestModel)
        {
            if (Image.File != null)
            {
                using var ms = new MemoryStream();
                await Image.File.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                stockEntryRequestModel.Image = Convert.ToBase64String(fileBytes);
            }

            stockEntryRequestModel.UpdateUserId = JsonConvert.DeserializeObject<ViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            var response = await _product.StockEntry("Bearer " + HttpContext.Session.GetString("userToken"), stockEntryRequestModel);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (response.Status)
            {
                ToastrSuccess = response.Message;
                return RedirectToPage("StockEntry");
            }

            await GetWholeSalerList();

            ToastrError = response.Message;
            return Page();
        }

        public async Task<IActionResult> OnPostStockEntryAsync([FromBody] StockEntryRequestModel stockEntryRequestModel)
        {   
            stockEntryRequestModel.UpdateUserId = JsonConvert.DeserializeObject<ViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            var response = await _product.StockEntry("Bearer " + HttpContext.Session.GetString("userToken"), stockEntryRequestModel);

            return new JsonResult(response);
        }

        public async Task GetWholeSalerList()
        {
            var response = await _wholeSaler.List("Bearer " + HttpContext.Session.GetString("userToken"));

            var list = response.Data.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToList();

            Options = list;
        }
    }
}
