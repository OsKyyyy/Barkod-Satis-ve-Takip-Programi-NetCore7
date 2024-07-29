using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response.HomePage;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IHomePageService
    {
        IDataResult<StockQuantityViewModel> GetStockQuantity();
        IDataResult<StockValueViewModel> GetStockValue();
        IDataResult<SalesTodayViewModel> GetSalesToday();
        IDataResult<TotalCurrentAccountViewModel> GetTotalUser();
        IDataResult<TotalCurrentAccountViewModel> GetTotalCustomer();
        IDataResult<TotalCurrentAccountViewModel> GetTotalWholeSaler();
    }
}
