using Core.Utilities.Refit.Models.Request.IncomeAndExpensesType;
using Core.Utilities.Refit.Models.Response;
using Core.Utilities.Refit.Models.Response.IncomeAndExpensesType;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Refit.Abstract
{
    public interface IIncomeAndExpensesType
    {       
        [Post("/incomeAndExpensesType/Add")]
        Task<Result> Add([Header("Authorization")] string token, [Body] AddRequestModel addRequestModel);

        [Put("/incomeAndExpensesType/Update")]
        Task<Result> Update([Header("Authorization")] string token, [Body] UpdateRequestModel updateTypeRequestModel);

        [Delete("/incomeAndExpensesType/Delete")]
        Task<Result> Delete([Header("Authorization")] string token, int id);

        [Get("/incomeAndExpensesType/List")]
        Task<DataResult<List<ViewModel>>> List([Header("Authorization")] string token);

        [Get("/incomeAndExpensesType/ListById")]
        Task<DataResult<ViewModel>> ListById([Header("Authorization")] string token, [AliasAs("id")] int id);

        [Get("/incomeAndExpensesType/ListByActive")]
        Task<DataResult<List<ViewModel>>> ListByActive([Header("Authorization")] string token);
    }
}
