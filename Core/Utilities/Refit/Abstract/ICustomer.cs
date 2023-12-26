using Core.Utilities.Refit.Models.Request.Customer;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response;
using Core.Utilities.Refit.Models.Response.Customer;

namespace Core.Utilities.Refit.Abstract
{
    public interface ICustomer
    {
        [Post("/customer/Add")]
        Task<Result> Add([Header("Authorization")] string token, [Body] AddRequestModel addRequestModel);

        [Put("/customer/Update")]
        Task<Result> Update([Header("Authorization")] string token, [Body] UpdateRequestModel updateRequestModel);

        [Get("/customer/List")]
        Task<DataResult<List<ViewModel>>> List([Header("Authorization")] string token);

        [Delete("/customer/Delete")]
        Task<Result> Delete([Header("Authorization")] string token, int id);

        [Get("/customer/ListById")]
        Task<DataResult<ViewModel>> ListById([Header("Authorization")] string token, [AliasAs("id")] int id);
    }
}
