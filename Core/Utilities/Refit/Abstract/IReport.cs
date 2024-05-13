using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response;
using Core.Utilities.Refit.Models.Response.Report;
using Core.Utilities.Refit.Models.Response.Sale;
using CustomerMovementViewModel = Core.Utilities.Refit.Models.Response.CustomerMovement.ViewModel;
using SaleViewModel = Core.Utilities.Refit.Models.Response.Sale.ViewModel;

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

        [Get("/report/GetLastCustomerWithDebt")]
        Task<DataResult<List<SaleViewModel>>> GetLastCustomerWithDebt([Header("Authorization")] string token);

        [Get("/report/GetLastCustomerWithDebtPayment")]
        Task<DataResult<List<CustomerMovementViewModel>>> GetLastCustomerWithDebtPayment([Header("Authorization")] string token);

        [Get("/report/GetCustomerTotalDebt")]
        Task<DataResult<CustomerTotalDebtViewModel>> GetCustomerTotalDebt([Header("Authorization")] string token);

        [Get("/report/GetCustomerDebt")]
        Task<DataResult<List<CustomerDebtViewModel>>> GetCustomerDebt([Header("Authorization")] string token);

        [Get("/report/GetCustomerNonPayers")]
        Task<DataResult<List<CustomerNonPayersViewModel>>> GetCustomerNonPayers([Header("Authorization")] string token);
    }
}
