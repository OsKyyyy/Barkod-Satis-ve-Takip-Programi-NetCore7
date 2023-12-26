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

namespace WebUI.Pages.Pos
{
    public class SaleModel : PageModel
    {
        private readonly IPos _pos;

        public SaleModel(IPos pos)
        {
            _pos = pos;
        }

        [ViewData]
        public string ToastrError { get; set; }

        public AddRequestModel AddRequestModel { get; set; }

        public AddMoneyRequestModel AddMoneyRequestModel { get; set; }
        
        public ReceivedMoneyRequestModel ReceivedMoneyRequestModel { get; set; }
        
        public List<ViewModel> ViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ToastrError = TempData["ToastrError"] as string;

            var CreateUserId = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            var response = await _pos.List("Bearer " + HttpContext.Session.GetString("userToken"), CreateUserId);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (response.Status)
            {
                ViewModel = response.Data;
                return Page();
            }

            ToastrError = response.Message;
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

            if (response.Status)
            {
                return RedirectToPage("Sale");
            }

            var responseList = await _pos.List("Bearer " + HttpContext.Session.GetString("userToken"), addRequestModel.CreateUserId);
            ViewModel = responseList.Data;

            ToastrError = response.Message;
            return Page();
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
        
        public async Task<IActionResult> OnPostFindPriceAsync([FromBody] FindPriceRequestModel findPriceRequestModel)
        {
            var response = await _pos.ListByBarcode("Bearer " + HttpContext.Session.GetString("userToken"), findPriceRequestModel.Barcode);
            
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
    }
}
