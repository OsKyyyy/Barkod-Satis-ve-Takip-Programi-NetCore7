using Core.Utilities.WebScraping.Models;
using Core.Utilities.WebScraping;
using Core.Utilities.WebScraping.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class PriceTrackingMarketModel : PageModel
{
    private readonly IPriceTrackingService _priceTrackingService;

    public PriceTrackingMarketModel(IPriceTrackingService priceTrackingService)
    {
        _priceTrackingService = priceTrackingService;
    }

    public async Task<IActionResult> OnPostMarketAsync([FromBody] PriceTrackingMarketRequestModel priceTrackingMarketRequestModel)
    {
        var response = await _priceTrackingService.Market(priceTrackingMarketRequestModel.MarketName);

        return new JsonResult(response);
    }
}