using Core.Utilities.Refit.Models.Request.CustomerMovement;
using Core.Utilities.Refit.Models.Response.CustomerMovement;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response;

namespace Core.Utilities.Refit.Abstract
{
    public interface ICustomerMovement
    {
        [Post("/customermovement/Add")]
        Task<Result> Add([Header("Authorization")] string token, [Body] AddRequestModel addRequestModel);

        [Put("/customermovement/Update")]
        Task<Result> Update([Header("Authorization")] string token, [Body] UpdateRequestModel updateRequestModel);

        [Delete("/customermovement/Delete")]
        Task<Result> Delete([Header("Authorization")] string token, int id);

        [Get("/customermovement/ListByCustomerId")]
        Task<DataResult<List<ViewModel>>> ListByCustomerId([Header("Authorization")] string token, [AliasAs("id")] int id);

        [Get("/customermovement/ListById")]
        Task<DataResult<ViewModel>> ListById([Header("Authorization")] string token, [AliasAs("id")] int id);
    }
}
