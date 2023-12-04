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
        [Post("/category/add")]
        Task<DataResult<AddModel>> Add([Header("Authorization")] string token, [Body] AddRequestModel createRequest);
    }
}
