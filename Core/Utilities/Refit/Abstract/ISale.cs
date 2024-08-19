using Core.Utilities.Refit.Models.Request.Sale;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response;
using Core.Utilities.Refit.Models.Response.Sale;

namespace Core.Utilities.Refit.Abstract
{
    public interface ISale
    {
        [Post("/sale/Add")]
        Task<DataResult<ViewModel>> Add([Header("Authorization")] string token, [Body] AddRequestModel addRequestModel);

        [Delete("/sale/Delete")]
        Task<Result> Delete([Header("Authorization")] string token, int id);
    }
}
