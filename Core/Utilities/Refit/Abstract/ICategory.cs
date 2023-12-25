using Core.Utilities.Refit.Models.Request.Category;
using Core.Utilities.Refit.Models.Response.Category;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response;

namespace Core.Utilities.Refit.Abstract
{
    public interface ICategory
    {
        [Post("/category/Add")]
        Task<Result> Add([Header("Authorization")] string token, [Body] AddRequestModel addRequestModel);

        [Put("/category/Update")]
        Task<Result> Update([Header("Authorization")] string token, [Body] UpdateRequestModel updateRequestModel);

        [Get("/category/List")]
        Task<DataResult<List<ViewModel>>> List([Header("Authorization")] string token);

        [Delete("/category/Delete")]
        Task<Result> Delete([Header("Authorization")] string token, int id);

        [Get("/category/ListById")]
        Task<DataResult<ViewModel>> ListById([Header("Authorization")] string token, [AliasAs("id")] int id);

        [Get("/category/ListByActive")]
        Task<DataResult<List<ViewModel>>> ListByActive([Header("Authorization")] string token);
    }
}
