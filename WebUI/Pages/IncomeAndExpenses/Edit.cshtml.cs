using Core.Entities.Concrete;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.IncomeAndExpenses;
using Core.Utilities.Refit.Models.Response.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Globalization;

namespace WebUI.Pages.IncomeAndExpenses
{
    public class EditModel : PageModel
    {
        private readonly IIncomeAndExpenses _incomeAndExpenses;
        private readonly IIncomeAndExpensesType _incomeAndExpensesType;

        public EditModel(IIncomeAndExpenses incomeAndExpenses, IIncomeAndExpensesType incomeAndExpensesType)
        {
            _incomeAndExpenses = incomeAndExpenses;
            _incomeAndExpensesType = incomeAndExpensesType;
        }

        [ViewData]
        public string ToastrError { get; set; }

        [TempData]
        public string ToastrSuccess { get; set; }

        [BindProperty]
        public UpdateRequestModel UpdateRequestModel { get; set; }

        public List<SelectListItem> Options { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _incomeAndExpenses.ListById("Bearer " + HttpContext.Session.GetString("userToken"), id);

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
                setModel.Amount = response.Data.Amount.ToString(CultureInfo.InvariantCulture);
                setModel.Description = response.Data.Description;
                setModel.Type = response.Data.Type;
                setModel.IncomeExpensesTypeId = response.Data.IncomeExpensesTypeId;
                setModel.Status = response.Data.Status;

                UpdateRequestModel = setModel;

                await GetIncomeAndExpensesType(response.Data.IncomeExpensesTypeId);

                return Page();
            }

            TempData["ToastrError"] = response.Message;

            return RedirectToPage("List");
        }

        public async Task<IActionResult> OnPostEditAsync(UpdateRequestModel updateRequestModel)
        {
            updateRequestModel.UpdateUserId = JsonConvert.DeserializeObject<ViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            var response = await _incomeAndExpenses.Update("Bearer " + HttpContext.Session.GetString("userToken"), updateRequestModel);

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

            ToastrError = response.Message;
            return Page();
        }

        public async Task GetIncomeAndExpensesType(int incomeAndExpensesTypeId)
        {
            var response = await _incomeAndExpensesType.ListByActive("Bearer " + HttpContext.Session.GetString("userToken"));

            var list = (from item in response.Data let selected = incomeAndExpensesTypeId == item.Id select new SelectListItem { Value = item.Id.ToString(), Text = item.Name, Selected = selected }).ToList();

            Options = list;
        }
    }
}
