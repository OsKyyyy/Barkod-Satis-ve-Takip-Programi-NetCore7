using Core.Utilities.Refit.Models.Request.IncomeAndExpenses;
using Core.Utilities.Refit.Models.Response;
using Core.Utilities.Refit.Models.Response.IncomeAndExpenses;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Abstract
{
    public interface IIncomeAndExpenses
    {       
        [Post("/incomeAndExpenses/Add")]
        Task<Result> Add([Header("Authorization")] string token, [Body] AddRequestModel addRequestModel);

        [Put("/incomeAndExpenses/Update")]
        Task<Result> Update([Header("Authorization")] string token, [Body] UpdateRequestModel updateRequestModel);

        [Get("/incomeAndExpenses/List")]
        Task<DataResult<List<ViewModel>>> List([Header("Authorization")] string token);

        [Delete("/incomeAndExpenses/Delete")]
        Task<Result> Delete([Header("Authorization")] string token, int id);

        [Get("/incomeAndExpenses/ListById")]
        Task<DataResult<ViewModel>> ListById([Header("Authorization")] string token, [AliasAs("id")] int id);
    }
}
