using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.Category;
using Core.Utilities.Refit.Models.Response;
using Core.Utilities.Refit.Models.Response.Category;
using Newtonsoft.Json;
using Refit;

namespace Core.Utilities.Refit.Concrete
{
    public class CategoryManager : ICategory
    {
        private ICategory myAPI = RestService.For<ICategory>("http://localhost:63067/api");

        public async Task<DataResult<AddModel>> Add([Header("Authorization")] string token, [Body] AddRequestModel createRequest)
        {
            DataResult<AddModel> dataResult = new DataResult<AddModel>();

            try
            {
                dataResult = await myAPI.Add(token, createRequest);

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
