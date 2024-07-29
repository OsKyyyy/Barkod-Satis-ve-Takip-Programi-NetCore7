using Core.Utilities.Refit.Models.Response.HomePage;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response;

namespace Core.Utilities.Refit.Abstract
{
    public interface IHomePage
    {
        [Get("/homepage/GetStockQuantity")]
        Task<DataResult<StockQuantityViewModel>> GetStockQuantity([Header("Authorization")] string token);

        [Get("/homepage/GetStockValue")]
        Task<DataResult<StockValueViewModel>> GetStockValue([Header("Authorization")] string token);

        [Get("/homepage/GetSalesToday")]
        Task<DataResult<SalesTodayViewModel>> GetSalesToday([Header("Authorization")] string token);

        [Get("/homepage/GetTotalUser")]
        Task<DataResult<TotalCurrentAccountViewModel>> GetTotalUser([Header("Authorization")] string token);

        [Get("/homepage/GetTotalCustomer")]
        Task<DataResult<TotalCurrentAccountViewModel>> GetTotalCustomer([Header("Authorization")] string token);

        [Get("/homepage/GetTotalWholeSaler")]
        Task<DataResult<TotalCurrentAccountViewModel>> GetTotalWholeSaler([Header("Authorization")] string token);
    }
}
