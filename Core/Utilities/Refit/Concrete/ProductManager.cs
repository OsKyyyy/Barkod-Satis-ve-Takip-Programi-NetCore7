using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.Product;
using Core.Utilities.Refit.Models.Response;
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
