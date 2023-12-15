using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.User;
using Core.Utilities.Refit.Models.Response.User;
using Core.Utilities.Refit.Models.Response;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Refit.Concrete
{
    public class UserManager : IUser
    {
        private IUser myAPI = RestService.For<IUser>("http://localhost:63067/api");

        public async Task<DataResult<LoginModel>> Login([Body] LoginRequestModel loginRequest)
        {
            DataResult<LoginModel> dataResult = new DataResult<LoginModel>();

            try
            {
                dataResult = await myAPI.Login(loginRequest);

                return dataResult;
            }
            catch (ApiException exception)
            {
                dynamic response = JsonConvert.DeserializeObject(exception.Content);
                
                if (response.Status != null)
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

        public async Task<DataResult<RegisterModel>> Register([Header("Authorization: Bearer")] string token, [Body] RegisterRequestModel registerRequest)
        {
            DataResult<RegisterModel> dataResult = new DataResult<RegisterModel>();

            try
            {
                dataResult = await myAPI.Register(token,registerRequest);

                return dataResult;
            }
            catch (ApiException exception)
            {
                dynamic response = JsonConvert.DeserializeObject(exception.Content);

                if (response.Status != null)
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

        public async Task<DataResult<UpdateModel>> Update([Header("Authorization: Bearer")] string token, [Body] UpdateRequestModel updateRequest)
        {
            DataResult<UpdateModel> dataResult = new DataResult<UpdateModel>();

            try
            {
                dataResult = await myAPI.Update(token, updateRequest);

                return dataResult;
            }
            catch (ApiException exception)
            {
                dynamic response = JsonConvert.DeserializeObject(exception.Content);

                if (response.Status != null)
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

        public async Task<DataResult<ViewModel>> Delete([Header("Authorization: Bearer")] string token, int id)
        {
            DataResult<ViewModel> dataResult = new DataResult<ViewModel>();

            try
            {
                dataResult = await myAPI.Delete(token, id);

                return dataResult;
            }
            catch (ApiException exception)
            {
                dynamic response = JsonConvert.DeserializeObject(exception.Content);

                if (response.Status != null)
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
                
                if (response.Status != null)
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

        public async Task<DataResult<List<ViewModel>>> InActiveList([Header("Authorization: Bearer")] string token)
        {
            DataResult<List<ViewModel>> dataResult = new DataResult<List<ViewModel>>();

            try
            {
                dataResult = await myAPI.InActiveList(token);

                return dataResult;
            }
            catch (ApiException exception)
            {
                dynamic response = JsonConvert.DeserializeObject(exception.Content);

                if (response.Status != null)
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

        public async Task<DataResult<ViewModel>> ListByMail([Header("Authorization: Bearer")] string token, string email)
        {
            DataResult<ViewModel> dataResult = new DataResult<ViewModel>();

            try
            {
                dataResult = await myAPI.ListByMail(token,email);

                return dataResult;
            }
            catch (ApiException exception)
            {
                dynamic response = JsonConvert.DeserializeObject(exception.Content);

                if (response.Status != null)
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

                if (response.Status != null)
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
