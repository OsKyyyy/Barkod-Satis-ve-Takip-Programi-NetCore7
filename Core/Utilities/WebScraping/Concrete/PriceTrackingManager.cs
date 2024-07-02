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
using Core.Utilities.Refit.Models.Response;
using Core.Utilities.Refit.Abstract;

namespace Core.Utilities.WebScraping.Concrete
{
    public class PriceTrackingManager : IPriceTrackingService
    {
        private readonly HttpClient _httpClient;
        public PriceTrackingManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IDataResult<List<MarketListViewModel>>> MarketList()
        {
            var markets = new List<MarketListViewModel>();
            var productUrl = $"https://marketkarsilastir.com/kayitli-marketler";

            var get = await _httpClient.GetStringAsync(productUrl);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(get);

            var marketNodes = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='products']");
            var rows = marketNodes.SelectNodes(".//ul/li");

            foreach (var marketNode in rows)
            {
                var marketUrlNode = marketNode.SelectSingleNode(".//a[@class='product-img d-flex align-items-center']");
                var marketUrl = marketUrlNode?.GetAttributeValue("href", string.Empty);
                var marketLogoNode = marketNode.SelectSingleNode(".//img");
                var marketLogo = marketLogoNode?.GetAttributeValue("src", string.Empty);
                var marketNameNode = marketNode.SelectSingleNode(".//div[@class='pi-name mt-1']/a");
                var marketName = marketNameNode?.InnerText.Trim();

                var market = new MarketListViewModel
                {
                    MarketName = marketName,
                    MarketLogo = marketLogo,
                    MarketUrl = marketUrl
                };

                markets.Add(market);
            }
            return new SuccessDataResult<List<MarketListViewModel>>(markets);
        }

        public async Task<IDataResult<List<MarketViewModel>>> Market(string marketName)
        {
            var products = new List<MarketViewModel>();
            var marketUrl = $"https://marketkarsilastir.com/market/{marketName}";

            var get = await _httpClient.GetStringAsync(marketUrl);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(get);

            var productsNodes = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='products']");             
            var rows = productsNodes.SelectNodes(".//ul/li");
            var marketInfo = productsNodes.SelectSingleNode(".//div[@class='main-title p-3']").InnerText.Trim();

            if (rows != null)
            {
                foreach (var marketNode in rows)
                {
                    var productLogoNode = marketNode.SelectSingleNode(".//img");
                    var productLogo = productLogoNode?.GetAttributeValue("src", string.Empty);
                    var productNameNode = marketNode.SelectSingleNode(".//div[@class='pi-name mt-1']/a");
                    var productName = productNameNode?.InnerText.Trim();
                    var productPriceNode = marketNode.SelectNodes(".//div[@class='pi-name mt-1']/div");
                    var productNewPrice = productPriceNode[0].InnerText.Trim();
                    var productOldPrice = productPriceNode[1].InnerText.Trim();

                    var product = new MarketViewModel
                    {
                        ProductName = productName,
                        ProductLogo = productLogo,
                        NewPrice = productNewPrice,
                        OldPrice = productOldPrice,
                        MarketInfo = marketInfo
                    };

                    products.Add(product);
                }
            }
           

            return new SuccessDataResult<List<MarketViewModel>>(products);
        }
    }
}
