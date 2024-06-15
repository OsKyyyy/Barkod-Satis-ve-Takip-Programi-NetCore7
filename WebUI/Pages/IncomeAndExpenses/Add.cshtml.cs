using System.Configuration;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request;
using Core.Utilities.Refit.Models.Request.IncomeAndExpenses;
using Core.Utilities.Refit.Models.Response.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebUI.Pages.IncomeAndExpenses
{
    public class AddModel : PageModel
    {
        private readonly IIncomeAndExpenses _incomeAndExpenses;
        private readonly IIncomeAndExpensesType _incomeAndExpensesType;

        public AddModel(IIncomeAndExpenses incomeAndExpenses, IIncomeAndExpensesType incomeAndExpensesType)
        {
            _incomeAndExpenses = incomeAndExpenses;
            _incomeAndExpensesType = incomeAndExpensesType;
        }

        [ViewData]
        public string ToastrError { get; set; }

        [TempData]
        public string ToastrSuccess { get; set; }

        public AddRequestModel AddRequestModel { get; set; }

        public List<SelectListItem> Options { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await _incomeAndExpensesType.ListByActive("Bearer " + HttpContext.Session.GetString("userToken"));

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
            addRequestModel.CreateUserId = JsonConvert.DeserializeObject<ViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            var response = await _incomeAndExpenses.Add("Bearer " + HttpContext.Session.GetString("userToken"), addRequestModel);

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

            await GetIncomeAndExpensesTypeList();

            ToastrError = response.Message;
            return Page();
        }

        public async Task GetIncomeAndExpensesTypeList()
        {
            var responseCategory = await _incomeAndExpensesType.ListByActive("Bearer " + HttpContext.Session.GetString("userToken"));

            var list = responseCategory.Data.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToList();

            Options = list;
        }
    }
}
