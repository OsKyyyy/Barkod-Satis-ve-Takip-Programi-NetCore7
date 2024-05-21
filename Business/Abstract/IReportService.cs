using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response.Report;
using Entities.Dtos;
using Refit;
using Core.Utilities.Refit.Models.Response;
using SaleViewModel = Core.Utilities.Refit.Models.Response.Sale.ViewModel;
using CustomerMovementViewModel = Core.Utilities.Refit.Models.Response.CustomerMovement.ViewModel;

namespace Business.Abstract
{
    public interface IReportService
    {
        IDataResult<List<SalesReportViewModel>> SalesReport();
        IDataResult<List<SaleViewModel>> SalesDetailReport(DateTime date);
        IDataResult<List<CustomerMovementViewModel>> GetLastCustomerWithDebt();
        IDataResult<List<CustomerMovementViewModel>> GetLastCustomerWithDebtPayment();
        IDataResult<CustomerTotalDebtViewModel> GetCustomerTotalDebt();
        IDataResult<List<CustomerDebtViewModel>> GetCustomerDebt();
        IDataResult<List<CustomerNonPayersViewModel>> GetCustomerNonPayers();
        IDataResult<CustomerTotalDebtViewModel> GetCustomerThisMonthDebt();
        IDataResult<CustomerTotalDebtViewModel> GetCustomerPreviousMonthDebt();
        IDataResult<List<CustomerMonthlyDebtViewModel>> GetCustomerMonthlyDebtOfOneYear();
    }
}
