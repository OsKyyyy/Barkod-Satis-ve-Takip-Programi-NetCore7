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
//using Microsoft.Extensions.Configuration;

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

        public async Task<Result> AddRole([Header("Authorization")] string token, [Body] AddRoleRequestModel addRoleRequestModel)
        {
            Result dataResult = new Result();

            try
            {
                dataResult = await myAPI.AddRole(token, addRoleRequestModel);

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

        public async Task<Result> UpdateRole([Header("Authorization")] string token, [Body] UpdateRoleRequestModel updateRoleRequestModel)
        {
            Result dataResult = new Result();

            try
            {
                dataResult = await myAPI.UpdateRole(token, updateRoleRequestModel);

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

        public async Task<DataResult<List<RoleListViewModel>>> RoleList([Header("Authorization: Bearer")] string token)
        {
            DataResult<List<RoleListViewModel>> dataResult = new DataResult<List<RoleListViewModel>>();

            try
            {
                dataResult = await myAPI.RoleList(token);

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

        public async Task<DataResult<RoleListViewModel>> GetRoleById([Header("Authorization: Bearer")] string token, int id)
        {
            DataResult<RoleListViewModel> dataResult = new DataResult<RoleListViewModel>();

            try
            {
                dataResult = await myAPI.GetRoleById(token, id);

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

        public async Task<DataResult<RoleListViewModel>> GetRoleByName([Header("Authorization: Bearer")] string token, string name)
        {
            DataResult<RoleListViewModel> dataResult = new DataResult<RoleListViewModel>();

            try
            {
                dataResult = await myAPI.GetRoleByName(token, name);

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

        public async Task<Result> UpdateUserRole([Header("Authorization")] string token, [Body] UpdateUserRoleRequestModel updateUserRoleRequestModel)
        {
            Result dataResult = new Result();

            try
            {
                dataResult = await myAPI.UpdateUserRole(token, updateUserRoleRequestModel);

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

        public async Task<Result> UpdateUserPassword([Header("Authorization")] string token, [Body] UpdateUserPasswordRequestModel updateUserPasswordRequestModel)
        {
            Result dataResult = new Result();

            try
            {
                dataResult = await myAPI.UpdateUserPassword(token, updateUserPasswordRequestModel);

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

        public async Task<Result> UpdateUserEmail([Header("Authorization")] string token, [Body] UpdateUserEmailRequestModel updateUserEmailRequestModel)
        {
            Result dataResult = new Result();

            try
            {
                dataResult = await myAPI.UpdateUserEmail(token, updateUserEmailRequestModel);

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
