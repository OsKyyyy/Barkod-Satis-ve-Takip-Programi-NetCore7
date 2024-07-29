using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Response;
using Core.Utilities.Refit.Models.Response.HomePage;
using Newtonsoft.Json;
using Refit;

namespace Core.Utilities.Refit.Concrete
{
    public class HomePageManager : IHomePage
    {
        private IHomePage myAPI = RestService.For<IHomePage>("http://localhost:63067/api");

        public async Task<DataResult<StockQuantityViewModel>> GetStockQuantity([Header("Authorization: Bearer")] string token)
        {
            DataResult<StockQuantityViewModel> dataResult = new DataResult<StockQuantityViewModel>();

            try
            {
                dataResult = await myAPI.GetStockQuantity(token);

                return dataResult;
            }
            catch (ApiException exception)
            {
                dynamic response = JsonConvert.DeserializeObject(exception.Content);

                if (response != null && response.Status != null)
                {
                    dataResult.Message = response.Message;
                    dataResult.Status = response.Status;

                    return dataResult;
                }

                dataResult.Message = "Beklenmedik hata ile karşılaşıldı";
                dataResult.Status = false;

                return dataResult;
            }
        }

        public async Task<DataResult<StockValueViewModel>> GetStockValue([Header("Authorization: Bearer")] string token)
        {
            DataResult<StockValueViewModel> dataResult = new DataResult<StockValueViewModel>();

            try
            {
                dataResult = await myAPI.GetStockValue(token);

                return dataResult;
            }
            catch (ApiException exception)
            {
                dynamic response = JsonConvert.DeserializeObject(exception.Content);

                if (response != null && response.Status != null)
                {
                    dataResult.Message = response.Message;
                    dataResult.Status = response.Status;

                    return dataResult;
                }

                dataResult.Message = "Beklenmedik hata ile karşılaşıldı";
                dataResult.Status = false;

                return dataResult;
            }
        }

        public async Task<DataResult<SalesTodayViewModel>> GetSalesToday([Header("Authorization: Bearer")] string token)
        {
            DataResult<SalesTodayViewModel> dataResult = new DataResult<SalesTodayViewModel>();

            try
            {
                dataResult = await myAPI.GetSalesToday(token);

                return dataResult;
            }
            catch (ApiException exception)
            {
                dynamic response = JsonConvert.DeserializeObject(exception.Content);

                if (response != null && response.Status != null)
                {
                    dataResult.Message = response.Message;
                    dataResult.Status = response.Status;

                    return dataResult;
                }

                dataResult.Message = "Beklenmedik hata ile karşılaşıldı";
                dataResult.Status = false;

                return dataResult;
            }
        }

        public async Task<DataResult<TotalCurrentAccountViewModel>> GetTotalUser([Header("Authorization: Bearer")] string token)
        {
            DataResult<TotalCurrentAccountViewModel> dataResult = new DataResult<TotalCurrentAccountViewModel>();

            try
            {
                dataResult = await myAPI.GetTotalUser(token);

                return dataResult;
            }
            catch (ApiException exception)
            {
                dynamic response = JsonConvert.DeserializeObject(exception.Content);

                if (response != null && response.Status != null)
                {
                    dataResult.Message = response.Message;
                    dataResult.Status = response.Status;

                    return dataResult;
                }

                dataResult.Message = "Beklenmedik hata ile karşılaşıldı";
                dataResult.Status = false;

                return dataResult;
            }
        }

        public async Task<DataResult<TotalCurrentAccountViewModel>> GetTotalCustomer([Header("Authorization: Bearer")] string token)
        {
            DataResult<TotalCurrentAccountViewModel> dataResult = new DataResult<TotalCurrentAccountViewModel>();

            try
            {
                dataResult = await myAPI.GetTotalCustomer(token);

                return dataResult;
            }
            catch (ApiException exception)
            {
                dynamic response = JsonConvert.DeserializeObject(exception.Content);

                if (response != null && response.Status != null)
                {
                    dataResult.Message = response.Message;
                    dataResult.Status = response.Status;

                    return dataResult;
                }

                dataResult.Message = "Beklenmedik hata ile karşılaşıldı";
                dataResult.Status = false;

                return dataResult;
            }
        }

        public async Task<DataResult<TotalCurrentAccountViewModel>> GetTotalWholeSaler([Header("Authorization: Bearer")] string token)
        {
            DataResult<TotalCurrentAccountViewModel> dataResult = new DataResult<TotalCurrentAccountViewModel>();

            try
            {
                dataResult = await myAPI.GetTotalWholeSaler(token);

                return dataResult;
            }
            catch (ApiException exception)
            {
                dynamic response = JsonConvert.DeserializeObject(exception.Content);

                if (response != null && response.Status != null)
                {
                    dataResult.Message = response.Message;
                    dataResult.Status = response.Status;

                    return dataResult;
                }

                dataResult.Message = "Beklenmedik hata ile karşılaşıldı";
                dataResult.Status = false;

                return dataResult;
            }
        }

    }
}
