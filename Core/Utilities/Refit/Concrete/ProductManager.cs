﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.Product;
using Core.Utilities.Refit.Models.Response;
using Core.Utilities.Refit.Models.Response.Product;
using Newtonsoft.Json;
using Refit;

namespace Core.Utilities.Refit.Concrete
{
    public class ProductManager : IProduct
    {
        private IProduct myAPI = RestService.For<IProduct>("http://localhost:63067/api");

        public async Task<Result> Add([Header("Authorization")] string token, [Body] AddRequestModel addRequest)
        {
            Result dataResult = new Result();

            try
            {
                dataResult = await myAPI.Add(token, addRequest);

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

        public async Task<Result> Update([Header("Authorization: Bearer")] string token, [Body] UpdateRequestModel updateRequestModel)
        {
            Result dataResult = new Result();

            try
            {
                dataResult = await myAPI.Update(token, updateRequestModel);

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

        public async Task<DataResult<List<ViewModel>>> List([Header("Authorization: Bearer")] string token)
        {
            DataResult<List<ViewModel>> dataResult = new DataResult<List<ViewModel>>();

            try
            {
                dataResult = await myAPI.List(token);

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

        public async Task<DataResult<ViewModel>> ListById([Header("Authorization: Bearer")] string token, int id)
        {
            DataResult<ViewModel> dataResult = new DataResult<ViewModel>();

            try
            {
                dataResult = await myAPI.ListById(token, id);

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

        public async Task<DataResult<List<ViewModel>>> ListByFavorite([Header("Authorization: Bearer")] string token)
        {
            DataResult<List<ViewModel>> dataResult = new DataResult<List<ViewModel>>();

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

        public async Task<DataResult<List<ViewModel>>> ListByCategory([Header("Authorization: Bearer")] string token, int id)
        {
            DataResult<List<ViewModel>> dataResult = new DataResult<List<ViewModel>>();

            try
            {
                dataResult = await myAPI.ListByCategory(token, id);

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

        public async Task<DataResult<ViewModel>> ListByBarcode([Header("Authorization: Bearer")] string token, string barcode)
        {
            DataResult<ViewModel> dataResult = new DataResult<ViewModel>();

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

        public async Task<DataResult<List<ViewModel>>> ListByName([Header("Authorization: Bearer")] string token, string name)
        {
            DataResult<List<ViewModel>> dataResult = new DataResult<List<ViewModel>>();

            try
            {
                dataResult = await myAPI.ListByName(token,name);

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

        public async Task<Result> StockEntry([Header("Authorization")] string token, [Body] StockEntryRequestModel stockEntryRequestModel)
        {
            Result dataResult = new Result();

            try
            {
                dataResult = await myAPI.StockEntry(token, stockEntryRequestModel);

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

        public async Task<bool> ListToSavePhoto([Header("Authorization: Bearer")] string token, string barcode)
        {            
            bool dataResult = await myAPI.ListToSavePhoto(token, barcode);

            return dataResult;
        }

        public async Task<Result> UpdateImage([Header("Authorization: Bearer")] string token, [Body] UpdateImageRequestModel updateImageRequestModel)
        {
            Result dataResult = new Result();

            try
            {
                dataResult = await myAPI.UpdateImage(token, updateImageRequestModel);

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
