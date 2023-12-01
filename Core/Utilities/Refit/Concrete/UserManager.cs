using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request;
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

        public async Task<DataResult<Login>> Login([Body] LoginRequest loginRequest)
        {
            DataResult<Login> dataResult = new DataResult<Login>();

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

        public async Task<DataResult<Register>> Register([Header("Authorization: Bearer")] string token, [Body] RegisterRequest registerRequest)
        {
            DataResult<Register> dataResult = new DataResult<Register>();

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

        public async Task<DataResult<List<UserInfo>>> List([Header("Authorization: Bearer")] string token)
        {
	        DataResult<List<UserInfo>> dataResult = new DataResult<List<UserInfo>>();

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

		public async Task<DataResult<UserInfo>> ListByMail([Header("Authorization: Bearer")] string token, string email)
        {
            DataResult<UserInfo> dataResult = new DataResult<UserInfo>();

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

        public async Task<DataResult<UserInfo>> ListById([Header("Authorization: Bearer")] string token, int id)
        {
            DataResult<UserInfo> dataResult = new DataResult<UserInfo>();

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
