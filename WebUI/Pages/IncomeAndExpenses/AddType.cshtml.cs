using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.IncomeAndExpenses;
using Core.Utilities.Refit.Models.Response.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebUI.Pages.IncomeAndExpenses
{
    public class AddTypeModel : PageModel
    {
        private readonly IIncomeAndExpenses _incomeAndExpenses;

        public AddTypeModel(IIncomeAndExpenses incomeAndExpenses)
        {
            _incomeAndExpenses = incomeAndExpenses;
        }

        [ViewData]
        public string ToastrError { get; set; }

        [TempData]
        public string ToastrSuccess { get; set; }

        public AddTypeRequestModel AddTypeRequestModel { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync(AddTypeRequestModel addTypeRequestModel)
        {
            addTypeRequestModel.CreateUserId = JsonConvert.DeserializeObject<ViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            var response = await _incomeAndExpenses.AddType("Bearer " + HttpContext.Session.GetString("userToken"), addTypeRequestModel);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return new RedirectToPageResult("../User/Login");
            }

            if (response.Status)
            {
                ToastrSuccess = response.Message;
                return new RedirectToPageResult("AddType");
            }

            ToastrError = response.Message;
            return Page();
        }
    }
}
