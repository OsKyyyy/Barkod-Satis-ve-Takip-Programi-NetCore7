using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.Sale;
using Core.Utilities.Refit.Models.Response;
using Newtonsoft.Json;
using Refit;

namespace Core.Utilities.Refit.Concrete
{
    public class SaleManager : ISale
    {
        private ISale myAPI = RestService.For<ISale>("http://localhost:63067/api");

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
    }
}
