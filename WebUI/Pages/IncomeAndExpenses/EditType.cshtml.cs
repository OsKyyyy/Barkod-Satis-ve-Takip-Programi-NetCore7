using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.IncomeAndExpenses;
using Core.Utilities.Refit.Models.Response.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebUI.Pages.IncomeAndExpenses
{
    public class EditTypeModel : PageModel
    {
        private readonly IIncomeAndExpenses _incomeAndExpenses;

        public EditTypeModel(IIncomeAndExpenses incomeAndExpenses)
        {
            _incomeAndExpenses = incomeAndExpenses;
        }

        [ViewData]
        public string ToastrError { get; set; }

        [TempData]
        public string ToastrSuccess { get; set; }

        [BindProperty]
        public UpdateTypeRequestModel UpdateTypeRequestModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _incomeAndExpenses.ListTypeById("Bearer " + HttpContext.Session.GetString("userToken"), id);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (response.Status)
            {
                UpdateTypeRequestModel setModel = new UpdateTypeRequestModel();

                setModel.Id = response.Data.Id;
                setModel.Name = response.Data.Name;
                setModel.Status = response.Data.Status;

                UpdateTypeRequestModel = setModel;

                return Page();
            }

            TempData["ToastrError"] = response.Message;

            return RedirectToPage("ListType");
        }

        public async Task<IActionResult> OnPostEditAsync(UpdateTypeRequestModel updateTypeRequestModel)
        {
            updateTypeRequestModel.UpdateUserId = JsonConvert.DeserializeObject<ViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            var response = await _incomeAndExpenses.UpdateType("Bearer " + HttpContext.Session.GetString("userToken"), updateTypeRequestModel);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (response.Status)
            {
                ToastrSuccess = response.Message;
                return RedirectToPage("EditType", updateTypeRequestModel.Id);
            }

            ToastrError = response.Message;
            return Page();
        }
    }
}
