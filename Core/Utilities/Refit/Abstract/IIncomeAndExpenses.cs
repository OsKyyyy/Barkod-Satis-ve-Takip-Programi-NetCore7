using Core.Utilities.Refit.Models.Request.IncomeAndExpenses;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response;

namespace Core.Utilities.Refit.Abstract
{
    public interface IIncomeAndExpenses
    {
        [Post("/incomeAndExpenses/AddType")]
        Task<Result> AddType([Header("Authorization")] string token, [Body] AddTypeRequestModel addRequestModel);
    }
}
