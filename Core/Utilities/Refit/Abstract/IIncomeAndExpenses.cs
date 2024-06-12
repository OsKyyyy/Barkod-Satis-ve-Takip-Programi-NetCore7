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
        [Post("/incomeAndExpenses/AddType")]
        Task<Result> AddType([Header("Authorization")] string token, [Body] AddTypeRequestModel addRequestModel);

        [Put("/incomeAndExpenses/UpdateType")]
        Task<Result> UpdateType([Header("Authorization")] string token, [Body] UpdateTypeRequestModel updateTypeRequestModel);

        [Delete("/incomeAndExpenses/DeleteType")]
        Task<Result> DeleteType([Header("Authorization")] string token, int id);

        [Get("/incomeAndExpenses/ListType")]
        Task<DataResult<List<ViewModel>>> ListType([Header("Authorization")] string token);

        [Get("/incomeAndExpenses/ListTypeById")]
        Task<DataResult<ViewModel>> ListTypeById([Header("Authorization")] string token, [AliasAs("id")] int id);
    }
}
