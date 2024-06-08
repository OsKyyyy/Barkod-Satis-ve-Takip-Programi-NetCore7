using Core.Utilities.WebScraping.Models;
using Core.Utilities.WebScraping;
using Core.Utilities.WebScraping.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class PriceComparisonModel : PageModel
{
    private readonly IPriceComparisonService _priceComparison;

    public PriceComparisonModel(IPriceComparisonService priceComparison)
    {
        _priceComparison = priceComparison;
    }

    public async Task<IActionResult> OnPostMarketOneAsync([FromBody] PriceComparisonRequestModel priceComparisonRequestModel)
    {
        var response = await _priceComparison.MarketOnePriceComparison(priceComparisonRequestModel.Barcode);

        return new JsonResult(response);
    }

    public async Task<IActionResult> OnPostMarketTwoAsync([FromBody] PriceComparisonRequestModel priceComparisonRequestModel)
    {
        var response = await _priceComparison.MarketTwoPriceComparison(priceComparisonRequestModel.Barcode);

        return new JsonResult(response);
    }

    public async Task<IActionResult> OnPostMarketThreeAsync([FromBody] PriceComparisonRequestModel priceComparisonRequestModel)
    {
        var response = await _priceComparison.MarketThreePriceComparison(priceComparisonRequestModel.Barcode);

        return new JsonResult(response);
    }

    public async Task<IActionResult> OnPostMarketFourAsync([FromBody] PriceComparisonRequestModel priceComparisonRequestModel)
    {
        var response = await _priceComparison.MarketFourPriceComparison(priceComparisonRequestModel.Barcode);

        return new JsonResult(response);
    }

    public async Task<IActionResult> OnPostMarketFiveAsync([FromBody] PriceComparisonRequestModel priceComparisonRequestModel)
    {
        var response = await _priceComparison.MarketFivePriceComparison(priceComparisonRequestModel.Barcode);

        return new JsonResult(response);
    }

    public async Task<IActionResult> OnPostMarketSixAsync([FromBody] PriceComparisonRequestModel priceComparisonRequestModel)
    {
        var response = await _priceComparison.MarketSixPriceComparison(priceComparisonRequestModel.Barcode);

        return new JsonResult(response);
    }
}