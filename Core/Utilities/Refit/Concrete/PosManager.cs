using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.Pos;
using Core.Utilities.Refit.Models.Response;
using Core.Utilities.Refit.Models.Response.Pos;
using Newtonsoft.Json;
using Refit;
using ProductViewModel = Core.Utilities.Refit.Models.Response.Product.ViewModel;

namespace Core.Utilities.Refit.Concrete
{
    public class PosManager : IPos
    {
        private IPos myAPI = RestService.For<IPos>("http://localhost:63067/api");

        public async Task<Result> Add([Header("Authorization")] string token, [Body] AddRequestModel addRequestModel)
        {
            Result dataResult = new Result();

            try
            {
                dataResult = await myAPI.Add(token, addRequestModel);

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

        public async Task<Result> AddMoney([Header("Authorization")] string token, [Body] AddMoneyRequestModel addMoneyRequestModel)
        {
            Result dataResult = new Result();

            try
            {
                dataResult = await myAPI.AddMoney(token, addMoneyRequestModel);

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

        public async Task<DataResult<List<ViewModel>>> List([Header("Authorization: Bearer")] string token, int createUserId)
        {
            DataResult<List<ViewModel>> dataResult = new DataResult<List<ViewModel>>();

            try
            {
                dataResult = await myAPI.List(token, createUserId);

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

        public async Task<DataResult<List<ProductViewModel>>> ListByFavorite([Header("Authorization: Bearer")] string token)
        {
            DataResult<List<ProductViewModel>> dataResult = new DataResult<List<ProductViewModel>>();

            try
            {
                dataResult = await myAPI.ListByFavorite(token);

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

        public async Task<DataResult<ProductViewModel>> ListByBarcode([Header("Authorization: Bearer")] string token, string barcode)
        {
            DataResult<ProductViewModel> dataResult = new DataResult<ProductViewModel>();

            try
            {
                dataResult = await myAPI.ListByBarcode(token, barcode);

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

        public async Task<Result> QuantityIncrease([Header("Authorization: Bearer")] string token, int id)
        {
            Result dataResult = new Result();

            try
            {
                dataResult = await myAPI.QuantityIncrease(token, id);

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
        
        public async Task<Result> QuantityReduce([Header("Authorization: Bearer")] string token, int id)
        {
            Result dataResult = new Result();

            try
            {
                dataResult = await myAPI.QuantityReduce(token, id);

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

        public async Task<Result> Delete([Header("Authorization: Bearer")] string token, int id)
        {
            Result dataResult = new Result();

            try
            {
                dataResult = await myAPI.Delete(token, id);

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

        public async Task<Result> CancelSale([Header("Authorization: Bearer")] string token, int basket)
        {
            Result dataResult = new Result();

            try
            {
                dataResult = await myAPI.CancelSale(token, basket);

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
