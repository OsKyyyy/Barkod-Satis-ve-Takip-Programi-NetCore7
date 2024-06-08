using Core.Utilities.WebScraping.Models;
using Core.Utilities.WebScraping;
using Core.Utilities.WebScraping.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class PriceProduceMarketModel : PageModel
{
    private readonly IPriceProduceMarketService _priceProduceMarketService;

    public PriceProduceMarketModel(IPriceProduceMarketService priceProduceMarketService)
    {
        _priceProduceMarketService = priceProduceMarketService;
    }

    public async Task<IActionResult> OnGetListAsync()
    {
        var response = await _priceProduceMarketService.List();

        return new JsonResult(response);
    }
}