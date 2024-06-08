using Core.Utilities.WebScraping.Models;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.WebScraping.Abstract
{
    public interface IPriceComparisonService
    {
        Task<IDataResult<List<PriceComparisonViewModel>>> MarketOnePriceComparison(string barcode);
        Task<IDataResult<List<PriceComparisonViewModel>>> MarketTwoPriceComparison(string barcode);
        Task<IDataResult<List<PriceComparisonViewModel>>> MarketThreePriceComparison(string barcode);
        Task<IDataResult<List<PriceComparisonViewModel>>> MarketFourPriceComparison(string barcode);
        Task<IDataResult<List<PriceComparisonViewModel>>> MarketFivePriceComparison(string barcode);
        Task<IDataResult<List<PriceComparisonViewModel>>> MarketSixPriceComparison(string barcode);
    }
}
