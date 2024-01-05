using System.Collections;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request;
using Core.Utilities.Refit.Models.Request.CustomerMovement;
using Core.Utilities.Refit.Models.Response.CustomerMovement;   
using UserViewModel = Core.Utilities.Refit.Models.Response.User.ViewModel;
using CustomerViewModel = Core.Utilities.Refit.Models.Response.Customer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebUI.Pages.Customer
{
    public class MovementsModel : PageModel
    {
        private readonly ICustomerMovement _customerMovement;
        private readonly ICustomer _customer;

        public MovementsModel(ICustomerMovement customerMovement, ICustomer customer)
        {
            _customerMovement = customerMovement;
            _customer = customer;
        }

        [ViewData]
        public string? ToastrError { get; set; }

        [TempData]
        public string? ToastrSuccess { get; set; }

        public CustomerViewModel CustomerViewModel { get; set; }

        public List<ViewModel> CustomerMovementViewModel { get; set; }

        public AddRequestModel AddRequestModel { get; set; }

        public UpdateRequestModel UpdateRequestModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ToastrError = TempData["ToastrError"] as string;
            ToastrSuccess = TempData["ToastrSuccess"] as string;

            var response = await _customerMovement.ListByCustomerId("Bearer " + HttpContext.Session.GetString("userToken"), id);
            var responseCustomer = await _customer.ListById("Bearer " + HttpContext.Session.GetString("userToken"), id);

            if (response.Message == "Authentication Error" || responseCustomer.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (response.Status)
            {
                CustomerMovementViewModel = response.Data;
            }
            else
            {
                ToastrError = response.Message;
            }

            if (responseCustomer.Status)
            {
                CustomerViewModel = responseCustomer.Data;
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

            var response = await _customerMovement.Add("Bearer " + HttpContext.Session.GetString("userToken"), addRequestModel);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (response.Status)
            {
                ToastrSuccess = response.Message;
                return RedirectToPage("Movements", addRequestModel.CustomerId);
            }

            var responseCustomerMovement = await _customerMovement.ListByCustomerId("Bearer " + HttpContext.Session.GetString("userToken"), addRequestModel.CustomerId);
            var responseCustomer = await _customer.ListById("Bearer " + HttpContext.Session.GetString("userToken"), addRequestModel.CustomerId);

            CustomerViewModel = responseCustomer.Data;
            CustomerMovementViewModel = responseCustomerMovement.Data;

            ToastrError = response.Message;
            return Page();
        }

        public async Task<IActionResult> OnPostEditAsync(UpdateRequestModel updateRequestModel)
        {
            updateRequestModel.UpdateUserId = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            var response = await _customerMovement.Update("Bearer " + HttpContext.Session.GetString("userToken"), updateRequestModel);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (response.Status)
            {
                ToastrSuccess = response.Message;
                return RedirectToPage("Movements", updateRequestModel.CustomerId);
            }

            var responseCustomerMovement = await _customerMovement.ListByCustomerId("Bearer " + HttpContext.Session.GetString("userToken"), updateRequestModel.CustomerId);
            var responseCustomer = await _customer.ListById("Bearer " + HttpContext.Session.GetString("userToken"), updateRequestModel.CustomerId);

            CustomerViewModel = responseCustomer.Data;
            CustomerMovementViewModel = responseCustomerMovement.Data;

            ToastrError = response.Message;
            return Page();
        }

        public async Task<IActionResult> OnPostListByIdAsync([FromBody] int Id)
        {
            var response = await _customerMovement.ListById("Bearer " + HttpContext.Session.GetString("userToken"), Id);

            return new JsonResult(response);
        }
    }
}
