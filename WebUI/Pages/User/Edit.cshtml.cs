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
        [ViewData]
        public string ToastrError { get; set; }

        [TempData]
        public string ToastrSuccess { get; set; }

        [BindProperty]
        public UpdateRequestModel updateRequestModel { get; set; }

        private readonly IUser _user;

        public EditModel(IUser user)
        {
            _user = user;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var session = SessionValues();

            if (session[1] == null)
            {
                return new RedirectToPageResult("Login");
            }

            var response = await _user.ListById(SessionValues()[0], id);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return new RedirectToPageResult("Login");
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

                updateRequestModel = setModel;
            }
            else
            {
                ToastrError = response.Message;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostEditAsync(UpdateRequestModel updateRequestModel)
        {
            var response = await _user.Update(SessionValues()[0], updateRequestModel);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return new RedirectToPageResult("Login");
            }

            if (response.Status)
            {
                ToastrSuccess = response.Message;
                return new RedirectToPageResult("Edit", response.Data.Id);
            }

            ToastrError = response.Message;
            return Page();
        }

        public string[] SessionValues()
        {
            var user = "Bearer " + HttpContext.Session.GetString("userToken");
            var userInfo = HttpContext.Session.GetString("userInfo");

            string[] values = new string[2] { user, userInfo };
            return values;
        }
    }
}
