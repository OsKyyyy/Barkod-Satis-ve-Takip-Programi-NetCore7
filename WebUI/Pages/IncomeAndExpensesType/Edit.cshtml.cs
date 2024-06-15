using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.IncomeAndExpensesType;
using Core.Utilities.Refit.Models.Response.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebUI.Pages.IncomeAndExpensesType
{
    public class EditModel : PageModel
    {
        private readonly IIncomeAndExpensesType _incomeAndExpensesType;

        public EditModel(IIncomeAndExpensesType incomeAndExpensesType)
        {
            _incomeAndExpensesType = incomeAndExpensesType;
        }

        [ViewData]
        public string ToastrError { get; set; }

        [TempData]
        public string ToastrSuccess { get; set; }

        [BindProperty]
        public UpdateRequestModel UpdateRequestModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _incomeAndExpensesType.ListById("Bearer " + HttpContext.Session.GetString("userToken"), id);

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
                setModel.Status = response.Data.Status;

                UpdateRequestModel = setModel;

                return Page();
            }

            TempData["ToastrError"] = response.Message;

            return RedirectToPage("List");
        }

        public async Task<IActionResult> OnPostEditAsync(UpdateRequestModel updateRequestModel)
        {
            updateRequestModel.UpdateUserId = JsonConvert.DeserializeObject<ViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            var response = await _incomeAndExpensesType.Update("Bearer " + HttpContext.Session.GetString("userToken"), updateRequestModel);

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
    }
}
