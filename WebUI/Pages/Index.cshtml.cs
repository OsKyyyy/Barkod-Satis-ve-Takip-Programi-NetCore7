using Core.Entities.Concrete;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.User;
using Core.Utilities.Refit.Models.Response.HomePage;
using Core.Utilities.Refit.Models.Response.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUser _user;
        private readonly IHomePage _homePage;

        public IndexModel(IUser user, IHomePage homePage)
        {
            _user = user;
            _homePage = homePage;
        }

        [ViewData]
        public string? ToastrError { get; set; }

        [ViewData]
        public string? ToastrSuccess { get; set; }

        public StockQuantityViewModel StockQuantityViewModels { get; set; }
        public StockValueViewModel StockValueViewModels { get; set; }
        public SalesTodayViewModel SalesTodayViewModels { get; set; }
        public TotalCurrentAccountViewModel TotalUserViewModels { get; set; }
        public TotalCurrentAccountViewModel TotalCustomerViewModels { get; set; }
        public TotalCurrentAccountViewModel TotalWholeSalerViewModels { get; set; }
        public int UserId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
			ToastrError = TempData["ToastrError"] as string;
            ToastrSuccess = TempData["ToastrSuccess"] as string;

            var stockQuantity = await _homePage.GetStockQuantity("Bearer " + HttpContext.Session.GetString("userToken"));
            var stockValue = await _homePage.GetStockValue("Bearer " + HttpContext.Session.GetString("userToken"));
            var salesToday = await _homePage.GetSalesToday("Bearer " + HttpContext.Session.GetString("userToken"));
            var totalUser = await _homePage.GetTotalUser("Bearer " + HttpContext.Session.GetString("userToken"));
            var totalCustomer = await _homePage.GetTotalCustomer("Bearer " + HttpContext.Session.GetString("userToken"));
            var totalWholeSaler = await _homePage.GetTotalWholeSaler("Bearer " + HttpContext.Session.GetString("userToken"));

            if (stockQuantity.Message == "Authentication Error" || stockValue.Message == "Authentication Error" || salesToday.Message == "Authentication Error" || totalUser.Message == "Authentication Error" || totalCustomer.Message == "Authentication Error" || totalWholeSaler.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (stockQuantity.Status && stockValue.Status && totalUser.Status)
            {
                StockQuantityViewModels = stockQuantity.Data;
                StockValueViewModels = stockValue.Data;
                SalesTodayViewModels = salesToday.Data;
                TotalUserViewModels = totalUser.Data;
                TotalCustomerViewModels = totalCustomer.Data;
                TotalWholeSalerViewModels = totalWholeSaler.Data;
            }

            return Page();
        }

        public Task<JsonResult> OnGetUserIdAsync()
        {
            var userId = JsonConvert.DeserializeObject<ViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            return Task.FromResult(new JsonResult(userId));
        }

        public async Task<IActionResult> OnGetUserByIdAsync(int id)
        {
            var response = await _user.ListById("Bearer " + HttpContext.Session.GetString("userToken"), id);

            return new JsonResult(response);
        }

        public async Task<IActionResult> OnGetRoleAsync(string roleName)
        {
            var response = await _user.GetRoleByName("Bearer " + HttpContext.Session.GetString("userToken"), roleName);

            return new JsonResult(response);
        }
    }
}