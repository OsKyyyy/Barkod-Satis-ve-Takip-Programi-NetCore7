using Core.Entities.Concrete;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request;
using Core.Utilities.Refit.Models.Request.WholeSalerMovement;
using Core.Utilities.Refit.Models.Response.WholeSalerMovement;   
using UserViewModel = Core.Utilities.Refit.Models.Response.User.ViewModel;
using WholeSalerViewModel = Core.Utilities.Refit.Models.Response.WholeSaler.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebUI.Pages.WholeSaler
{
    public class MovementsModel : PageModel
    {
        private readonly IWholeSalerMovement _wholeSalerMovement;
        private readonly IWholeSaler _wholeSaler;

        public MovementsModel(IWholeSalerMovement wholeSalerMovement, IWholeSaler wholeSaler)
        {
            _wholeSalerMovement = wholeSalerMovement;
            _wholeSaler = wholeSaler;
        }

        [ViewData]
        public string? ToastrError { get; set; }

        [TempData]
        public string? ToastrSuccess { get; set; }

        public WholeSalerViewModel WholeSalerViewModel { get; set; }

        public List<ViewModel> WholeSalerMovementViewModel { get; set; }

        public AddRequestModel AddRequestModel { get; set; }

        public UpdateRequestModel UpdateRequestModel { get; set; }

        [BindProperty]
        public ImageRequestModel Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ToastrError = TempData["ToastrError"] as string;
            ToastrSuccess = TempData["ToastrSuccess"] as string;

            var response = await _wholeSalerMovement.ListByWholeSalerId("Bearer " + HttpContext.Session.GetString("userToken"), id);
            var responseWholeSaler = await _wholeSaler.ListById("Bearer " + HttpContext.Session.GetString("userToken"), id);

            if (response.Message == "Authentication Error" || responseWholeSaler.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (response.Status)
            {
                WholeSalerMovementViewModel = response.Data;
            }
            else
            {
                ToastrError = response.Message;
            }

            if (responseWholeSaler.Status)
            {
                WholeSalerViewModel = responseWholeSaler.Data;
            }
            else
            {
                ToastrError = responseWholeSaler.Message;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync(AddRequestModel addRequestModel)
        {
            if (Image.File != null)
            {
                using var ms = new MemoryStream();
                await Image.File.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                addRequestModel.Image = Convert.ToBase64String(fileBytes);
            }

            addRequestModel.CreateUserId = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            var response = await _wholeSalerMovement.Add("Bearer " + HttpContext.Session.GetString("userToken"), addRequestModel);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (response.Status)
            {
                ToastrSuccess = response.Message;
                return RedirectToPage("Movements", addRequestModel.WholeSalerId);
            }

            var responseWholeSalerMovement = await _wholeSalerMovement.ListByWholeSalerId("Bearer " + HttpContext.Session.GetString("userToken"), addRequestModel.WholeSalerId);
            var responseWholeSaler = await _wholeSaler.ListById("Bearer " + HttpContext.Session.GetString("userToken"), addRequestModel.WholeSalerId);

            WholeSalerViewModel = responseWholeSaler.Data;
            WholeSalerMovementViewModel = responseWholeSalerMovement.Data;

            ToastrError = response.Message;
            return Page();
        }

        public async Task<IActionResult> OnPostEditAsync(UpdateRequestModel updateRequestModel)
        {
            if (Image.File != null)
            {
                using var ms = new MemoryStream();
                await Image.File.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                updateRequestModel.Image = Convert.ToBase64String(fileBytes);
                updateRequestModel.ImageChanged = true;
            }

            updateRequestModel.UpdateUserId = JsonConvert.DeserializeObject<UserViewModel>(HttpContext.Session.GetString("userInfo")).Id;

            var response = await _wholeSalerMovement.Update("Bearer " + HttpContext.Session.GetString("userToken"), updateRequestModel);

            if (response.Message == "Authentication Error")
            {
                HttpContext.Session.Remove("userToken");
                HttpContext.Session.Remove("userInfo");

                return RedirectToPage("../User/Login");
            }

            if (response.Status)
            {
                ToastrSuccess = response.Message;
                return RedirectToPage("Movements", updateRequestModel.WholeSalerId);
            }

            var responseWholeSalerMovement = await _wholeSalerMovement.ListByWholeSalerId("Bearer " + HttpContext.Session.GetString("userToken"), updateRequestModel.WholeSalerId);
            var responseWholeSaler = await _wholeSaler.ListById("Bearer " + HttpContext.Session.GetString("userToken"), updateRequestModel.WholeSalerId);

            WholeSalerViewModel = responseWholeSaler.Data;
            WholeSalerMovementViewModel = responseWholeSalerMovement.Data;

            ToastrError = response.Message;
            return Page();
        }

        public async Task<IActionResult> OnPostListByIdAsync([FromBody] int Id)
        {
            var response = await _wholeSalerMovement.ListById("Bearer " + HttpContext.Session.GetString("userToken"), Id);

            return new JsonResult(response);
        }
    }
}
