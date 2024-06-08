using Core.Utilities.WebScraping.Models;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.WebScraping.Abstract
{
    public interface IPriceProduceMarketService
    {
        Task<IDataResult<List<PriceProduceMarketViewModel>>> List();
    }
}