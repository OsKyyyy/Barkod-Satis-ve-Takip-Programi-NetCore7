using Core.Utilities.Refit.Models.Request.Pos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.Pos
{
    public class SaleModel : PageModel
    {
        public AddMoneyRequestModel AddMoneyRequestModel { get; set; }

        public ReceivedMoneyRequestModel ReceivedMoneyRequestModel { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAddMoneyAsync(AddMoneyRequestModel addMoneyRequestModel)
        {
            return Page();
        }
        public async Task<IActionResult> OnPostReceivedMoneyAsync(ReceivedMoneyRequestModel receivedMoneyRequestModel)
        {
            return Page();
        }
    }
}
