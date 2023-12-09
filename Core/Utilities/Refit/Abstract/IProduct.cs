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
        [Post("/product/add")]
        Task<Result> Add([Header("Authorization")] string token, [Body] AddRequestModel createRequest);
    }
}
