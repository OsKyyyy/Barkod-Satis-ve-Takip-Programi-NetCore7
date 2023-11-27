using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.User
{
    public class AddModel : PageModel
    {
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostTest()
        {
            return Page();
        }
    }
}
