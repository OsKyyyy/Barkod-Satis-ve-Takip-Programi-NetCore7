using Core.Utilities.Refit.Models.Request.WholeSaler;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response;
using Core.Utilities.Refit.Models.Response.WholeSaler;

namespace Core.Utilities.Refit.Abstract
{
    public interface IWholeSaler
    {
        [Post("/wholesaler/Add")]
        Task<Result> Add([Header("Authorization")] string token, [Body] AddRequestModel addRequestModel);

        [Put("/wholesaler/Update")]
        Task<Result> Update([Header("Authorization")] string token, [Body] UpdateRequestModel updateRequestModel);

        [Get("/wholesaler/List")]
        Task<DataResult<List<ViewModel>>> List([Header("Authorization")] string token);

        [Delete("/wholesaler/Delete")]
        Task<Result> Delete([Header("Authorization")] string token, int id);

        [Get("/wholesaler/ListById")]
        Task<DataResult<ViewModel>> ListById([Header("Authorization")] string token, [AliasAs("id")] int id);
    }
}
