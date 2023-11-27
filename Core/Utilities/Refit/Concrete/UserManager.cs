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

namespace Core.Utilities.Refit.Concrete
{
    public class UserManager : IUser
    {
        public async Task<DataResult<Login>> Login([Body] LoginRequest loginRequest)
        {
            DataResult<Login> dataResult = new DataResult<Login>();

            var myAPI = RestService.For<IUser>("http://localhost:63067/api");
            
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

        public async Task<DataResult<UserInfo>> GetInfoByMail(string email)
        {
            DataResult<UserInfo> dataResult = new DataResult<UserInfo>();

            var myAPI = RestService.For<IUser>("http://localhost:63067/api");

            try
            {
                dataResult = await myAPI.GetInfoByMail(email);

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
