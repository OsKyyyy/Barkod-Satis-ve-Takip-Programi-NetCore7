using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request;
using Core.Utilities.Refit.Models.Response.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.User
{
    public class ListModel : PageModel
    {
        private readonly IUser _user;

        public ListModel(IUser user)
        {
            _user = user;
        }

        [ViewData]
	    public string ToastrError { get; set; }

        [ViewData]
        public string ToastrSuccess{ get; set; }

        public List<ViewModel> ViewModel { get; set; }
        
		public async Task<IActionResult> OnGetAsync()
	    {
            ToastrError = TempData["ToastrError"] as string;
            ToastrSuccess = TempData["ToastrSuccess"] as string;

			var response = await _user.List("Bearer " + HttpContext.Session.GetString("userToken"));

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return new RedirectToPageResult("Login");
            }

			if (response.Status)
            {
                ViewModel = response.Data;
            }
            else
            {
                ToastrError = response.Message;
            }

            return Page();
	    }
	}
}
