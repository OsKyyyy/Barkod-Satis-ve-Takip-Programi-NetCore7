using Core.Utilities.Refit.Models.Request.Product;
using Core.Utilities.Refit.Models.Response.Product;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response;

namespace Core.Utilities.Refit.Abstract
{
    public interface IProduct
    {
        [Post("/product/Add")]
        Task<Result> Add([Header("Authorization")] string token, [Body] AddRequestModel addRequestModel);

        [Put("/product/Update")]
        Task<Result> Update([Header("Authorization")] string token, [Body] UpdateRequestModel updateRequestModel);

        [Get("/product/List")]
        Task<DataResult<List<ViewModel>>> List([Header("Authorization")] string token);

        [Delete("/product/Delete")]
        Task<DataResult<ViewModel>> Delete([Header("Authorization")] string token, int id);

        [Get("/product/ListById")]
        Task<DataResult<ViewModel>> ListById([Header("Authorization")] string token, [AliasAs("id")] int id);
    }
}
