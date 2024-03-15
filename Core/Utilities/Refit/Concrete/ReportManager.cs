using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Response;
using Core.Utilities.Refit.Models.Response.Report;
using Newtonsoft.Json;
using Refit;

namespace Core.Utilities.Refit.Concrete
{
    public class ReportManager : IReport
    {
        private IReport myAPI = RestService.For<IReport>("http://localhost:63067/api");

        public async Task<DataResult<List<SalesReportViewModel>>> SalesReport([Header("Authorization: Bearer")] string token)
        {
            DataResult<List<SalesReportViewModel>> dataResult = new DataResult<List<SalesReportViewModel>>();

            try
            {
                dataResult = await myAPI.SalesReport(token);

                return dataResult;
            }
            catch (ApiException exception)
            {
                dynamic response = JsonConvert.DeserializeObject(exception.Content);

                if (response != null && response.Status != null)
                {
                    dataResult.Message = response.Message;
                    dataResult.Status = response.Status;

                    return dataResult;
                }

                dataResult.Message = "Beklenmedik hata ile karşılaşıldı";
                dataResult.Status = false;

                return dataResult;
            }
        }
    }
}
