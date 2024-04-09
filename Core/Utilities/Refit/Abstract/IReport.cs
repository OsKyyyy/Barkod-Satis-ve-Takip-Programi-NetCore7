using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response;
using Core.Utilities.Refit.Models.Response.Report;

namespace Core.Utilities.Refit.Abstract
{
    public interface IReport
    {       
        [Get("/report/SalesReport")]
        Task<DataResult<List<SalesReportViewModel>>> SalesReport([Header("Authorization")] string token);

        [Get("/report/SalesDetailReport")]
        Task<DataResult<List<SalesDetailReportViewModel>>> SalesDetailReport([Header("Authorization")] string token, DateTime date);

        [Get("/report/SalesProductsDetailReport")]
        Task<DataResult<List<SalesProductsDetailReportViewModel>>> SalesProductsDetailReport([Header("Authorization")] string token, int id);

        [Delete("/report/SalesDelete")]
        Task<Result> SalesDelete([Header("Authorization")] string token, int id);
        Task SalesDetailReport(string v, DateTime? dateTime);
    }
}
