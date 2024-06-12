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

        [Post("/product/StockEntry")]
        Task<Result> StockEntry([Header("Authorization")] string token, [Body] StockEntryRequestModel stockEntryRequestModel);

        [Put("/product/Update")]
        Task<Result> Update([Header("Authorization")] string token, [Body] UpdateRequestModel updateRequestModel);

        [Get("/product/List")]
        Task<DataResult<List<ViewModel>>> List([Header("Authorization")] string token);

        [Delete("/product/Delete")]
        Task<Result> Delete([Header("Authorization")] string token, int id);

        [Get("/product/ListById")]
        Task<DataResult<ViewModel>> ListById([Header("Authorization")] string token, [AliasAs("id")] int id);

        [Get("/product/ListByFavorite")]
        Task<DataResult<List<ViewModel>>> ListByFavorite([Header("Authorization")] string token);

        [Get("/product/ListByBarcode")]
        Task<DataResult<ViewModel>> ListByBarcode([Header("Authorization")] string token, string barcode);

        [Get("/product/ListByName")]
        Task<DataResult<List<ViewModel>>> ListByName([Header("Authorization")] string token, string name);

        [Get("/product/ListToSavePhoto")]
        Task<bool> ListToSavePhoto([Header("Authorization")] string token, string barcode);

        [Put("/product/UpdateImage")]
        Task<Result> UpdateImage([Header("Authorization")] string token, [Body] UpdateImageRequestModel updateImageRequestModel);
    }
}
