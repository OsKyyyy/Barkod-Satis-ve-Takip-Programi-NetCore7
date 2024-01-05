using Core.Utilities.Refit.Models.Request.WholeSalerMovement;
using Core.Utilities.Refit.Models.Response.WholeSalerMovement;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response;

namespace Core.Utilities.Refit.Abstract
{
    public interface IWholeSalerMovement
    {
        [Post("/wholesalermovement/Add")]
        Task<Result> Add([Header("Authorization")] string token, [Body] AddRequestModel addRequestModel);

        [Put("/wholesalermovement/Update")]
        Task<Result> Update([Header("Authorization")] string token, [Body] UpdateRequestModel updateRequestModel);

        [Delete("/wholesalermovement/Delete")]
        Task<Result> Delete([Header("Authorization")] string token, int id);

        [Get("/wholesalermovement/ListByWholeSalerId")]
        Task<DataResult<List<ViewModel>>> ListByWholeSalerId([Header("Authorization")] string token, [AliasAs("id")] int id);

        [Get("/wholesalermovement/ListById")]
        Task<DataResult<ViewModel>> ListById([Header("Authorization")] string token, [AliasAs("id")] int id);
    }
}
