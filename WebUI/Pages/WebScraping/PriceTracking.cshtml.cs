using Core.Utilities.WebScraping.Models;
using Core.Utilities.WebScraping;
using Core.Utilities.WebScraping.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class PriceTrackingModel : PageModel
{
    private readonly IPriceTrackingService _priceTrackingService;

    public PriceTrackingModel(IPriceTrackingService priceTrackingService)
    {
        _priceTrackingService = priceTrackingService;
    }

    public async Task<IActionResult> OnGetMarketListAsync()
    {
        var response = await _priceTrackingService.MarketList();

        return new JsonResult(response);
    }
}