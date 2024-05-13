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
        IDataResult<List<SalesDetailReportViewModel>> SalesDetailReport(DateTime date);
        IDataResult<List<SalesProductsDetailReportViewModel>> SalesProductsDetailReport(int id);
        IDataResult<SalesDetailReportViewModel> SalesDetailReportById(int id);
        IResult SalesDelete(int id);
        IResult UpdateStock(string barcode, int quantity);
        IDataResult<List<SaleViewModel>> GetLastCustomerWithDebt();
        IDataResult<List<CustomerMovementViewModel>> GetLastCustomerWithDebtPayment();
        IDataResult<CustomerTotalDebtViewModel> GetCustomerTotalDebt();
        IDataResult<List<CustomerDebtViewModel>> GetCustomerDebt();
        IDataResult<List<CustomerNonPayersViewModel>> GetCustomerNonPayers();
    }
}
