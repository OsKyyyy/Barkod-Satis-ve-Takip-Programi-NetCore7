using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.IncomeAndExpenses;
using Core.Utilities.Refit.Models.Response;
using Newtonsoft.Json;
using Refit;

namespace Core.Utilities.Refit.Concrete
{
    public class IncomeAndExpensesManager : IIncomeAndExpenses
    {
        private IIncomeAndExpenses myAPI = RestService.For<IIncomeAndExpenses>("http://localhost:63067/api");

        public async Task<Result> AddType([Header("Authorization")] string token, [Body] AddTypeRequestModel addTypeRequestModel)
        {
            Result dataResult = new Result();

            try
            {
                dataResult = await myAPI.AddType(token, addTypeRequestModel);

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
