using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.WholeSaler;
using Core.Utilities.Refit.Models.Response.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebUI.Pages.WholeSaler
{
    public class EditModel : PageModel
    {
        private readonly IWholeSaler _wholeSaler;

        public EditModel(IWholeSaler wholeSaler)
        {
            _wholeSaler = wholeSaler;
        }

        [ViewData]
        public string ToastrError { get; set; }

        [TempData]
        public string ToastrSuccess { get; set; }

        [BindProperty]
        public UpdateRequestModel UpdateRequestModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _wholeSaler.ListById("Bearer " + HttpContext.Session.GetString("userToken"), id);

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
                setModel.AuthorizedPerson = response.Data.AuthorizedPerson;
                setModel.Phone = response.Data.Phone;
                setModel.Email = response.Data.Email;
                setModel.Address = response.Data.Address;
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

            var response = await _wholeSaler.Update("Bearer " + HttpContext.Session.GetString("userToken"), updateRequestModel);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (response.Status)
            {
                ToastrSuccess = response.Message;
                return RedirectToPage("Edit", UpdateRequestModel.Id);
            }

            ToastrError = response.Message;
            return Page();
        }
    }
}
