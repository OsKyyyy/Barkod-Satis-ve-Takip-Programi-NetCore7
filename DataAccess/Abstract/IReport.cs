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
using Core.Utilities.Refit.Models.Response.SaleProduct;

namespace DataAccess.Abstract
{
    public interface IReportDal : IEntityRepository<Sale>
    {
        List<SalesReportViewModel> SalesReport();
        List<SaleViewModel> SalesDetailReport(DateTime date);
        List<CustomerMovementViewModel> GetLastCustomerWithDebt();
        List<CustomerMovementViewModel> GetLastCustomerWithDebtPayment();
        CustomerTotalDebtViewModel GetCustomerTotalDebt();
        List<CustomerDebtViewModel> GetCustomerDebt();
        List<CustomerNonPayersViewModel> GetCustomerNonPayers();
        CustomerTotalDebtViewModel GetCustomerThisMonthDebt();
        CustomerTotalDebtViewModel GetCustomerPreviousMonthDebt();
        List<CustomerMonthlyDebtViewModel> GetCustomerMonthlyDebtOfOneYear();
    }
}
