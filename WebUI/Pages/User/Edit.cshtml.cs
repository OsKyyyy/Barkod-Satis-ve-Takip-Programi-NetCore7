using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request;
using Core.Utilities.Refit.Models.Request.User;
using Core.Utilities.Refit.Models.Response.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.User
{
    public class EditModel : PageModel
    {
        private readonly IUser _user;

        public EditModel(IUser user)
        {
            _user = user;
        }

        [ViewData]
        public string ToastrError { get; set; }

        [TempData]
        public string ToastrSuccess { get; set; }

        [BindProperty]
        public UpdateRequestModel UpdateRequestModel { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _user.ListById("Bearer " + HttpContext.Session.GetString("userToken"), id);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("Login");
            }

            if (response.Status)
            {
                UpdateRequestModel setModel = new UpdateRequestModel();

                setModel.Id = response.Data.Id;
                setModel.FirstName = response.Data.FirstName;
                setModel.LastName = response.Data.LastName;
                setModel.Phone = response.Data.Phone;
                setModel.Email = response.Data.Email;
                setModel.Status = response.Data.Status;

                UpdateRequestModel = setModel;

                return Page();
            }
            
            TempData["ToastrError"] = response.Message;
            return RedirectToPage("List");
        }

        public async Task<IActionResult> OnPostEditAsync(UpdateRequestModel updateRequestModel)
        {
            var response = await _user.Update("Bearer " + HttpContext.Session.GetString("userToken"), updateRequestModel);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("Login");
            }

            if (response.Status)
            {
                ToastrSuccess = response.Message;
                return RedirectToPage("Edit", response.Data.Id);
            }

            ToastrError = response.Message;
            return Page();
        }
    }
}
