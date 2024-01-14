using Core.Utilities.Refit.Models.Request.Sale;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response;

namespace Core.Utilities.Refit.Abstract
{
    public interface ISale
    {
        [Post("/sale/Add")]
        Task<Result> Add([Header("Authorization")] string token, [Body] AddRequestModel addRequestModel);
    }
}
