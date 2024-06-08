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
using System.Text.RegularExpressions;

namespace Core.Utilities.WebScraping.Concrete
{
    public class PriceProduceMarketManager : IPriceProduceMarketService
    {
        private readonly HttpClient _httpClient;
        public PriceProduceMarketManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IDataResult<List<PriceProduceMarketViewModel>>> List()
        {
            var products = new List<PriceProduceMarketViewModel>();
            var productUrl = $"https://www.hal.gov.tr/Sayfalar/AnaSayfa.aspx";

            var get = await _httpClient.GetStringAsync(productUrl);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(get);

            var productsNodes = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='productCosts']//div[@class='right']");
            var rows = productsNodes.SelectNodes(".//span");

            foreach (var productNode in rows)
            {
                var text = productNode.InnerHtml.Trim();
                var splitByBTag = text.Split(new[] { "<b>" }, StringSplitOptions.RemoveEmptyEntries);
                var productPriceAndUnitParts = splitByBTag[1].Split(new[] { "&nbsp(" }, StringSplitOptions.RemoveEmptyEntries);
                productPriceAndUnitParts = productPriceAndUnitParts[0].Split(new[] { "</b>" }, StringSplitOptions.RemoveEmptyEntries);
                productPriceAndUnitParts = productPriceAndUnitParts[0].Split(new[] { ") " }, StringSplitOptions.RemoveEmptyEntries);

                var productName = splitByBTag[0].Trim();
                var unitName = productPriceAndUnitParts[0].Trim();
                var productPrice = productPriceAndUnitParts[1].Replace("TL", "").Trim();

                var product = new PriceProduceMarketViewModel
                {
                    UnitName = unitName,
                    ProductName = productName,
                    ProductPrice = productPrice
                };

                products.Add(product);
            }

            return new SuccessDataResult<List<PriceProduceMarketViewModel>>(products);
        }
    }
}
