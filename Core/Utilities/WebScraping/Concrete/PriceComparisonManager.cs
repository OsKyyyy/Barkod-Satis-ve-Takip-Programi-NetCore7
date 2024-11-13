using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.WebScraping.Models;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.WebScraping.Abstract;
using HtmlAgilityPack;
using Core.Utilities.Refit.Models.Request.Product;
using Core.Utilities.Refit.Abstract;

namespace Core.Utilities.WebScraping.Concrete
{
    public class PriceComparisonManager : IPriceComparisonService
    {
        private readonly HttpClient _httpClient;
        public PriceComparisonManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<IDataResult<List<PriceComparisonViewModel>>> MarketOnePriceComparison(string barcode)
        {
            string? imgUrl = null;
            string? barcodeImgUrl = null;
            var result = new List<PriceComparisonViewModel>();
            var productUrl = $"https://marketkarsilastir.com/ara/{barcode}";

            var get = await _httpClient.GetStringAsync(productUrl);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(get);

            var priceNode = htmlDocument.DocumentNode.SelectSingleNode("//a[@class='product-img d-flex align-items-center']");
            var hrefValue = priceNode?.GetAttributeValue("href", null);

            if (hrefValue != null)
            {
                var productDetailUrl = $"https://marketkarsilastir.com/{hrefValue}";

                var getDetail = await _httpClient.GetStringAsync(productDetailUrl);
                var htmlDocumentDetail = new HtmlDocument();
                htmlDocumentDetail.LoadHtml(getDetail);

                var priceNodeDetail = htmlDocumentDetail.DocumentNode.SelectSingleNode("//table[@class='table text-center table-hover']");
                var logoNodeDetail = htmlDocumentDetail.DocumentNode.SelectSingleNode("//div[@class='card-img-top']");
                var barcodeLogoNodeDetail = htmlDocumentDetail.DocumentNode.SelectSingleNode("//div[@class='barkodResim']");
                
                var imgNode = logoNodeDetail.SelectSingleNode(".//img");
                if (imgNode != null)
                {
                     imgUrl = imgNode.GetAttributeValue("src", "");
                }

                var barcodeImgNode = barcodeLogoNodeDetail.SelectSingleNode(".//img");
                if (barcodeImgNode != null)
                {
                    barcodeImgUrl = barcodeImgNode.GetAttributeValue("src", "");
                }

                result = ParseHtml(priceNodeDetail, imgUrl, barcodeImgUrl);
            }

            return new SuccessDataResult<List<PriceComparisonViewModel>>(result);
        }

        public async Task<IDataResult<List<PriceComparisonViewModel>>> MarketTwoPriceComparison(string barcode)
        {
            string? imgUrl = null;
            var result = new List<PriceComparisonViewModel>();
            var productUrl = $"https://www.guleckapinda.com/arama/{barcode}";

            var get = await _httpClient.GetStringAsync(productUrl);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(get);

            var getNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='showcase']");

            if (getNode != null)
            {
                var imgNode = getNode.SelectSingleNode(".//div[@class='showcase-image']//img");
                var imgSrc = imgNode?.GetAttributeValue("data-src", "");

                var nameNode = getNode.SelectSingleNode(".//div[@class='showcase-title']//a");
                var name = nameNode?.InnerText.Trim();

                var priceNode = getNode.SelectSingleNode(".//div[@class='showcase-price']//div[@class='showcase-price-new']");
                var price = priceNode?.InnerText.Trim();

                var model = new PriceComparisonViewModel
                {
                    MarketLogo = "https://ideacdn.net/idea/gb/84/themes/selftpl_5ec79bfebfad3/assets/uploads/logo.png",
                    BarcodeLogo = "",
                    ProductLogo = imgSrc,
                    ProductName = name,
                    ProductPrice = price.Replace("TL", "").Trim(),
                    LastPriceChangeDate = ""
                };

                result.Add(model);
            }

            return new SuccessDataResult<List<PriceComparisonViewModel>>(result);
        }

        public async Task<IDataResult<List<PriceComparisonViewModel>>> MarketThreePriceComparison(string barcode)
        {
            string? imgUrl = null;
            var result = new List<PriceComparisonViewModel>();
            var productUrl = $"https://www.develigross.com/arama?k={barcode}";

            var get = await _httpClient.GetStringAsync(productUrl);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(get);

            var getNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='card-product-inner']");

            if (getNode != null)
            {
                var imgNode = getNode.SelectSingleNode(".//div[@class='image-wrapper']//img");
                var imgSrc = imgNode?.GetAttributeValue("data-src", "");

                var nameNode = getNode.SelectSingleNode(".//div[@class='title']");
                var name = nameNode?.InnerText.Trim();

                var priceNode = getNode.SelectSingleNode(".//div[@class='sale-price ']");
                var price = priceNode?.InnerText.Trim();

                var model = new PriceComparisonViewModel
                {
                    MarketLogo = "https://percdn.com/f/422501/cHNDVUoyVTArYkI4Tmk4Z1RvTTZKYms9/l/logo-85925523-sw300sh100.webp",
                    BarcodeLogo = "",
                    ProductLogo = imgSrc,
                    ProductName = name,
                    ProductPrice = price.Replace("TL", "").Trim(),
                    LastPriceChangeDate = ""
                };

                result.Add(model);
            }

            return new SuccessDataResult<List<PriceComparisonViewModel>>(result);
        }

        public async Task<IDataResult<List<PriceComparisonViewModel>>> MarketFourPriceComparison(string barcode)
        {
            string? imgUrl = null;
            var result = new List<PriceComparisonViewModel>();
            var productUrl = $"https://www.mismarsanalmarket.com/arama?Keyword={barcode}";

            var get = await _httpClient.GetStringAsync(productUrl);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(get);

            var getNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='product-item']");

            if (getNode != null)
            {
                var imgNode = getNode.SelectSingleNode(".//div[@class='image']//img");
                var imgSrc = imgNode?.GetAttributeValue("data-src", "");

                var nameNode = getNode.SelectSingleNode(".//div[@class='detail']//a");
                var name = nameNode?.InnerText.Trim();

                var priceNode = getNode.SelectSingleNode(".//div[@class='last-price']");
                var price = priceNode?.InnerText.Trim();

                var model = new PriceComparisonViewModel
                {
                    MarketLogo = "https://www.mismarsanalmarket.com/UserFiles/Fotograflar/37-logo-png-logo.png",
                    BarcodeLogo = "",
                    ProductLogo = imgSrc,
                    ProductName = name,
                    ProductPrice = price.Replace("TL", "").Trim(),
                    LastPriceChangeDate = ""
                };

                result.Add(model);
            }

            return new SuccessDataResult<List<PriceComparisonViewModel>>(result);
        }

        public async Task<IDataResult<List<PriceComparisonViewModel>>> MarketFivePriceComparison(string barcode)
        {
            string? imgUrl = null;
            var result = new List<PriceComparisonViewModel>();
            var productUrl = $"https://www.marketpaketi.com.tr/search?q={barcode}";

            var get = await _httpClient.GetStringAsync(productUrl);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(get);

            var getNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='liste_urun']");

            if (getNode != null)
            {
                var imgNode = getNode.SelectSingleNode(".//a[@class='urun_gorsel']//img");
                var imgSrc = imgNode?.GetAttributeValue("src", "");

                var nameNode = getNode.SelectSingleNode(".//a[@class='urun_adi_ic']");
                var name = HtmlEntity.DeEntitize(nameNode?.InnerText.Trim());

                var priceNode = getNode.SelectSingleNode(".//div[@class='urun_fiyat']");
                var price = priceNode?.InnerText.Trim();

                var model = new PriceComparisonViewModel
                {
                    MarketLogo = "https://www.marketpaketi.com.tr/images/logo.png",
                    BarcodeLogo = "",
                    ProductLogo = imgSrc,
                    ProductName = name,
                    ProductPrice = price.Replace("TL", "").Trim(),
                    LastPriceChangeDate = ""
                };

                result.Add(model);
            }

            return new SuccessDataResult<List<PriceComparisonViewModel>>(result);
        }

        public async Task<IDataResult<List<PriceComparisonViewModel>>> MarketSixPriceComparison(string barcode)
        {
            string? imgUrl = null;
            var result = new List<PriceComparisonViewModel>();
            var productUrl = $"https://www.obakmarket.com.tr/arama/{barcode}";

            var get = await _httpClient.GetStringAsync(productUrl);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(get);

            var getNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='showcase']");

            if (getNode != null)
            {
                var imgNode = getNode.SelectSingleNode(".//div[@class='showcase-image']//img");
                var imgSrc = imgNode?.GetAttributeValue("data-src", "");

                var nameNode = getNode.SelectSingleNode(".//div[@class='showcase-title']//a");
                var name = nameNode?.InnerText.Trim();

                var priceNode = getNode.SelectSingleNode(".//div[@class='showcase-price']//div[@class='showcase-price-new']");
                var price = priceNode?.InnerText.Trim();

                var model = new PriceComparisonViewModel
                {
                    MarketLogo = "https://ideacdn.net/idea/if/95/themes/selftpl_60798480899b3/assets/uploads/logo.png",
                    BarcodeLogo = "",
                    ProductLogo = imgSrc,
                    ProductName = name,
                    ProductPrice = price.Replace("TL", "").Trim(),
                    LastPriceChangeDate = ""
                };

                result.Add(model);
            }

            return new SuccessDataResult<List<PriceComparisonViewModel>>(result);
        }

        public List<PriceComparisonViewModel> ParseHtml(HtmlNode tableNode, string? imgUrl, string? barcodeImgUrl)
        {
            var priceComparisonList = new List<PriceComparisonViewModel>();

            var rows = tableNode.SelectNodes(".//tbody/tr");

            foreach (var row in rows)
            {
                var columns = row.SelectNodes("td");
                if (columns != null && columns.Count >= 3)
                {
                    var marketLogoUrl = columns[0].SelectSingleNode(".//img").GetAttributeValue("src", "");
                    var productNameNode = columns[1].SelectSingleNode(".//a");
                    var productNameFull = productNameNode.InnerText.Trim();
                    var productName = productNameFull.Split(new[] { "Son Fiyat Değişim Tarihi" }, System.StringSplitOptions.None)[0].Trim();
                    var lastPriceChangeDate = System.Text.RegularExpressions.Regex.Match(productNameNode.InnerHtml, @"Son Fiyat Değişim Tarihi:\s*\((.*?)\)").Groups[1].Value;
                    var price = columns[2].InnerText.Trim();

                    var model = new PriceComparisonViewModel
                    {
                        MarketLogo = marketLogoUrl,
                        BarcodeLogo = barcodeImgUrl,
                        ProductLogo = imgUrl,
                        ProductName = productName,
                        ProductPrice = price,
                        LastPriceChangeDate = lastPriceChangeDate
                    };

                    priceComparisonList.Add(model);
                }
            }

            return priceComparisonList;
        }

        public async Task<IDataResult<UpdateImageRequestModel>> SavePhoto(string imgUrl, string barcode)
        {
            var directoryPath = "C:\\Users\\POSWIN10\\Desktop\\MarketOtomasyon\\WebAPI\\Uploads\\Products";
            //C:\\Users\\oguzh\\source\\repos\\OsKyyyy\\MarketOtomasyon\\WebAPI\\Uploads\\Products"
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(imgUrl);
                    response.EnsureSuccessStatusCode();

                    var contentType = response.Content.Headers.ContentType.MediaType;
                    string fileExtension = GetFileExtension(contentType);
                    if (fileExtension == null)
                    {
                        return new ErrorDataResult<UpdateImageRequestModel>("Bilinmeyen Resim Formatı");
                    }

                    var imageBytes = await response.Content.ReadAsByteArrayAsync();

                    var fileName = $"{barcode}_{DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()}{fileExtension}";
                    var filePath = Path.Combine(directoryPath, fileName);

                    await File.WriteAllBytesAsync(filePath, imageBytes);

                    UpdateImageRequestModel updateImageRequestModel = new UpdateImageRequestModel
                    {
                        Image = $"Uploads/Products/{fileName}"
                    };

                    return new SuccessDataResult<UpdateImageRequestModel>(updateImageRequestModel, "Resim Başarılı Bir Şekilde Kaydedildi");
                }
                catch (Exception ex)
                {
                    return new ErrorDataResult<UpdateImageRequestModel>(ex.Message);
                }
            }
        }

        public string GetFileExtension(string contentType)
        {
            return contentType switch
            {
                "image/jpeg" => ".jpg",
                "image/png" => ".png",
                "image/gif" => ".gif",
                "image/bmp" => ".bmp",
                "image/tiff" => ".tiff",
                _ => null
            };
        }
    }
}
