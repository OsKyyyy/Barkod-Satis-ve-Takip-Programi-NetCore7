using Core.Entities.Concrete;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.Pos;
using Core.Utilities.Refit.Models.Response.Pos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using System;
using UserViewModel = Core.Utilities.Refit.Models.Response.User.ViewModel;
using ProductViewModel = Core.Utilities.Refit.Models.Response.Product.ViewModel;
using CustomerViewModel = Core.Utilities.Refit.Models.Response.Customer.ViewModel;
using SaleAddRequestModel = Core.Utilities.Refit.Models.Request.Sale.AddRequestModel;

namespace WebUI.Pages.Pos
{
    public class SaleModel : PageModel
    {
        private readonly IPos _pos;
        private readonly ICustomer _customer;
        private readonly IProduct _product;
        private readonly ISale _sale;
        private readonly IReport _report;

        public SaleModel(IPos pos, ICustomer customer, IProduct product, ISale sale, IReport report)
        {
            _pos = pos;
            _customer = customer;
            _product = product;
            _sale = sale;
            _report = report;
        }

        [ViewData]
        public string ToastrError { get; set; }
        
        [BindProperty]
        public string ToastrSuccess { get; set; }

        public AddRequestModel AddRequestModel { get; set; }

        public AddMoneyRequestModel AddMoneyRequestModel { get; set; }
        
        public ReceivedMoneyRequestModel ReceivedMoneyRequestModel { get; set; }
        
        public List<ViewModel> ViewModel { get; set; }

        public List<ProductViewModel> ViewModelFavorite { get; set; }

        public List<ProductViewModel> ViewModelCategory { get; set; }

        public List<CustomerViewModel> ViewModelCustomer { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ToastrError = TempData["ToastrError"] as string;

            var CreateUserId = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            var response = await _pos.List("Bearer " + HttpContext.Session.GetString("userToken"), CreateUserId);
            var responseFavorite = await _product.ListByFavorite("Bearer " + HttpContext.Session.GetString("userToken"));
            var responseCategory = await _product.ListByCategory("Bearer " + HttpContext.Session.GetString("userToken"), 26);
            var responseCustomer = await _customer.ListActive("Bearer " + HttpContext.Session.GetString("userToken"));

            if (response.Message == "Authentication Error" || responseFavorite.Message == "Authentication Error")
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

            if (responseFavorite.Status)
            {
                ViewModelFavorite = responseFavorite.Data;
            }
            else
            {
                ToastrError = responseFavorite.Message;
            }

            if (responseCategory.Status)
            {
                ViewModelCategory = responseCategory.Data;
            }
            else
            {
                ToastrError = responseCategory.Message;
            }

            if (responseCustomer.Status)
            {
                ViewModelCustomer = responseCustomer.Data;
            }
            else
            {
                ToastrError = responseCustomer.Message;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync(AddRequestModel addRequestModel)
        {
            addRequestModel.CreateUserId = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            if (addRequestModel.Quantity == null)
            {
                addRequestModel.Quantity = 1;
            }

            var response = await _pos.Add("Bearer " + HttpContext.Session.GetString("userToken"), addRequestModel);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            TempData["ToastrError"] = response.Message;
            return RedirectToPage("Sale");
        }

        public async Task<IActionResult> OnPostAddMoneyAsync(AddMoneyRequestModel addMoneyRequestModel)
        {
            addMoneyRequestModel.CreateUserId = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            var response = await _pos.AddMoney("Bearer " + HttpContext.Session.GetString("userToken"), addMoneyRequestModel);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (response.Status)
            {
                return RedirectToPage("Sale");
            }

            ToastrError = response.Message;

            return Page();
        }

        public async Task<IActionResult> OnPostSearchProductAsync([FromBody] SearchProductRequestModel searchProductRequestModel)
        {
            var response = await _product.ListByName("Bearer " + HttpContext.Session.GetString("userToken"), searchProductRequestModel.Name);

            return new JsonResult(response);
        }

        public async Task<IActionResult> OnPostFindPriceAsync([FromBody] FindPriceRequestModel findPriceRequestModel)
        {
            var response = await _product.ListByBarcode("Bearer " + HttpContext.Session.GetString("userToken"), findPriceRequestModel.Barcode);
            
            return new JsonResult(response);
        }

        public async Task<IActionResult> OnPostAddAjaxAsync([FromBody] AddRequestModel addRequestModel)
        {
            addRequestModel.CreateUserId = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            if (addRequestModel.Quantity == null)
            {
                addRequestModel.Quantity = 1;
            }

            var response = await _pos.Add("Bearer " + HttpContext.Session.GetString("userToken"), addRequestModel);

            return new JsonResult(response);
        }

        public async Task<IActionResult> OnPostCancelSaleAsync(int basket)
        {
            var CreateUserId = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            var response = await _pos.CancelSale("Bearer " + HttpContext.Session.GetString("userToken"), basket, CreateUserId);

            return new JsonResult(response);
        }

        public async Task<IActionResult> OnPostComplateSaleAsync([FromBody] SaleAddRequestModel addRequestModel)
        {
            addRequestModel.CreateUserId = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            var response = await _sale.Add("Bearer " + HttpContext.Session.GetString("userToken"), addRequestModel);

            return new JsonResult(response);
        }

        public async Task<IActionResult> OnGetSaleProductsAsync(int saleId)
        {
            var response = await _report.SaleProductListById("Bearer " + HttpContext.Session.GetString("userToken"), saleId);

            return new JsonResult(response);
        }
    }
}
