using Core.Entities.Concrete;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.Customer;
using Core.Utilities.Refit.Models.Response.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebUI.Pages.Customer
{
    public class AddModel : PageModel
    {
        private readonly ICustomer _customer;

        public AddModel(ICustomer customer)
        {
            _customer = customer;
        }

        [ViewData]
        public string ToastrError { get; set; }

        [TempData]
        public string ToastrSuccess { get; set; }

        public AddRequestModel AddRequestModel { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync(AddRequestModel addRequestModel)
        {
            addRequestModel.CreateUserId = JsonConvert.DeserializeObject<ViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            var response = await _customer.Add("Bearer " + HttpContext.Session.GetString("userToken"), addRequestModel);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return new RedirectToPageResult("../User/Login");
            }

            if (response.Status)
            {
                ToastrSuccess = response.Message;
                return new RedirectToPageResult("Add");
            }

            ToastrError = response.Message;
            return Page();
        }

    }
}
