using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response.Report;
using SaleViewModel = Core.Utilities.Refit.Models.Response.Sale.ViewModel;
using CustomerMovementViewModel = Core.Utilities.Refit.Models.Response.CustomerMovement.ViewModel;

namespace DataAccess.Abstract
{
    public interface IReportDal : IEntityRepository<Sale>
    {
        List<SalesReportViewModel> SalesReport();
        List<SalesDetailReportViewModel> SalesDetailReport(DateTime date);
        List<SalesProductsDetailReportViewModel> SalesProductsDetailReport(int id);
        SalesDetailReportViewModel SalesDetailReportById(int id);
        void SalesDelete(int id);
        void UpdateStock(string barcode, int quantity);
        List<SaleViewModel> GetLastCustomerWithDebt();
        List<CustomerMovementViewModel> GetLastCustomerWithDebtPayment();
        CustomerTotalDebtViewModel GetCustomerTotalDebt();
        List<CustomerDebtViewModel> GetCustomerDebt();
        List<CustomerNonPayersViewModel> GetCustomerNonPayers();
        CustomerTotalDebtViewModel GetCustomerThisMonthDebt();
        CustomerTotalDebtViewModel GetCustomerPreviousMonthDebt();
        List<CustomerMonthlyDebtViewModel> GetCustomerMonthlyDebtOfOneYear();
    }
}
