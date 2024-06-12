using Core.Utilities.WebScraping.Models;
using Core.Utilities.WebScraping;
using Core.Utilities.WebScraping.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Utilities.Refit.Abstract;
using Azure;
using Core.Utilities.Refit.Models.Request.Product;

public class PriceComparisonModel : PageModel
{
    private readonly IPriceComparisonService _priceComparison;
    private readonly IProduct _product;

    public PriceComparisonModel(IPriceComparisonService priceComparison,IProduct product)
    {
        _priceComparison = priceComparison;
        _product = product;
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

    public async Task<IActionResult> OnPostSavePhotoAsync([FromBody] SavePhotoRequestModel savePhotoRequestModel)
    {
        bool checkByBarcode = await _product.ListToSavePhoto("Bearer " + HttpContext.Session.GetString("userToken"), savePhotoRequestModel.Barcode);

        if (checkByBarcode)
        {
            var response = await _priceComparison.SavePhoto(savePhotoRequestModel.ImgUrl, savePhotoRequestModel.Barcode);

            if (response.Status)
            {
                UpdateImageRequestModel updateImageRequestModel = new UpdateImageRequestModel
                {
                    Barcode = savePhotoRequestModel.Barcode,
                    Image = response.Data.Image
                };

                await _product.UpdateImage("Bearer " + HttpContext.Session.GetString("userToken"), updateImageRequestModel);
            }

            return new JsonResult(response);
        }
        
        return new JsonResult(checkByBarcode);
    }
}