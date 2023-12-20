using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.Category;
using Core.Utilities.Refit.Models.Response.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebUI.Pages.Category
{
    public class AddModel : PageModel
    {
        private readonly ICategory _category;

        public AddModel(ICategory category)
        {
            _category = category;
        }

        public AddRequestModel AddRequestModel { get; set; }

        [ViewData]
        public string ToastrError { get; set; }

        [TempData]
        public string ToastrSuccess { get; set; }

        public IActionResult OnGet()
        {
            var session = SessionValues();

            if (session[1] == null)
            {
                return new RedirectToPageResult("../User/Login");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync(AddRequestModel addRequestModel)
        {
            addRequestModel.CreateUserId = JsonConvert.DeserializeObject<ViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            var response = await _category.Add(SessionValues()[0], addRequestModel);

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

        public string[] SessionValues()
        {
            var user = "Bearer " + HttpContext.Session.GetString("userToken");
            var userInfo = HttpContext.Session.GetString("userInfo");

            var values = new string[2] { user, userInfo };
            return values;
        }
    }
}
