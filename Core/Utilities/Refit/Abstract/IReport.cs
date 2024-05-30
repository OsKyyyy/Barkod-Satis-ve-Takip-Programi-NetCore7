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
using WholeSalerMovementViewModel = Core.Utilities.Refit.Models.Response.WholeSalerMovement.ViewModel;
using SaleViewModel = Core.Utilities.Refit.Models.Response.Sale.ViewModel;
using SaleProductViewModel = Core.Utilities.Refit.Models.Response.SaleProduct.ViewModel;
using Core.Utilities.Refit.Models.Response.SaleProduct;

namespace Core.Utilities.Refit.Abstract
{
    public interface IReport
    {       
        [Get("/report/SalesReport")]
        Task<DataResult<List<SalesReportViewModel>>> SalesReport([Header("Authorization")] string token);

        [Get("/report/SalesDetailReport")]
        Task<DataResult<List<SaleViewModel>>> SalesDetailReport([Header("Authorization")] string token, DateTime date);

        [Get("/saleProduct/ListById")]
        Task<DataResult<List<SaleProductViewModel>>> SaleProductListById([Header("Authorization")] string token, int id);

        [Get("/report/GetLastCustomerWithDebt")]
        Task<DataResult<List<CustomerMovementViewModel>>> GetLastCustomerWithDebt([Header("Authorization")] string token);

        [Get("/report/GetLastCustomerWithDebtPayment")]
        Task<DataResult<List<CustomerMovementViewModel>>> GetLastCustomerWithDebtPayment([Header("Authorization")] string token);

        [Get("/report/GetCustomerTotalDebt")]
        Task<DataResult<CustomerTotalDebtViewModel>> GetCustomerTotalDebt([Header("Authorization")] string token);

        [Get("/report/GetCustomerDebt")]
        Task<DataResult<List<CustomerDebtViewModel>>> GetCustomerDebt([Header("Authorization")] string token);

        [Get("/report/GetCustomerNonPayers")]
        Task<DataResult<List<CustomerNonPayersViewModel>>> GetCustomerNonPayers([Header("Authorization")] string token);

        [Get("/report/GetCustomerThisMonthDebt")]
        Task<DataResult<CustomerTotalDebtViewModel>> GetCustomerThisMonthDebt([Header("Authorization")] string token);

        [Get("/report/GetCustomerPreviousMonthDebt")]
        Task<DataResult<CustomerTotalDebtViewModel>> GetCustomerPreviousMonthDebt([Header("Authorization")] string token);

        [Get("/report/GetCustomerMonthlyDebtOfOneYear")]
        Task<DataResult<List<CustomerMonthlyDebtViewModel>>> GetCustomerMonthlyDebtOfOneYear([Header("Authorization")] string token);


        [Get("/report/GetLastWholeSalerWithDebt")]
        Task<DataResult<List<WholeSalerMovementViewModel>>> GetLastWholeSalerWithDebt([Header("Authorization")] string token);

        [Get("/report/GetLastWholeSalerWithDebtPayment")]
        Task<DataResult<List<WholeSalerMovementViewModel>>> GetLastWholeSalerWithDebtPayment([Header("Authorization")] string token);

        [Get("/report/GetWholeSalerTotalDebt")]
        Task<DataResult<WholeSalerTotalDebtViewModel>> GetWholeSalerTotalDebt([Header("Authorization")] string token);

        [Get("/report/GetWholeSalerDebt")]
        Task<DataResult<List<WholeSalerDebtViewModel>>> GetWholeSalerDebt([Header("Authorization")] string token);

        [Get("/report/GetWholeSalerNonPayers")]
        Task<DataResult<List<WholeSalerNonPayersViewModel>>> GetWholeSalerNonPayers([Header("Authorization")] string token);

        [Get("/report/GetWholeSalerThisMonthDebt")]
        Task<DataResult<WholeSalerTotalDebtViewModel>> GetWholeSalerThisMonthDebt([Header("Authorization")] string token);

        [Get("/report/GetWholeSalerPreviousMonthDebt")]
        Task<DataResult<WholeSalerTotalDebtViewModel>> GetWholeSalerPreviousMonthDebt([Header("Authorization")] string token);

        [Get("/report/GetWholeSalerMonthlyDebtOfOneYear")]
        Task<DataResult<List<WholeSalerMonthlyDebtViewModel>>> GetWholeSalerMonthlyDebtOfOneYear([Header("Authorization")] string token);
    }
}
