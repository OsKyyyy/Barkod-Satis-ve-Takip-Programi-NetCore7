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
using WholeSalerMovementViewModel = Core.Utilities.Refit.Models.Response.WholeSalerMovement.ViewModel;
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

        List<WholeSalerMovementViewModel> GetLastWholeSalerWithDebt();
        List<WholeSalerMovementViewModel> GetLastWholeSalerWithDebtPayment();
        WholeSalerTotalDebtViewModel GetWholeSalerTotalDebt();
        List<WholeSalerDebtViewModel> GetWholeSalerDebt();
        List<WholeSalerNonPayersViewModel> GetWholeSalerNonPayers();
        WholeSalerTotalDebtViewModel GetWholeSalerThisMonthDebt();
        WholeSalerTotalDebtViewModel GetWholeSalerPreviousMonthDebt();
        List<WholeSalerMonthlyDebtViewModel> GetWholeSalerMonthlyDebtOfOneYear();

        IncomeExpensesTotalViewModel MonthlyExternalIncome();
        IncomeExpensesTotalViewModel MonthlyExternalExpenses();
        IncomeExpensesTotalViewModel MonthlySalesIncome();
        IncomeExpensesTotalViewModel MonthlyWholeSalerExpenses();
        List<IncomeExpensesMonthlyTotalViewModel> YearlyExternalIncome();
        List<IncomeExpensesMonthlyTotalViewModel> YearlyExternalExpenses();
        List<IncomeExpensesMonthlyTotalViewModel> YearlySalesIncome();
        List<IncomeExpensesMonthlyTotalViewModel> YearlyWholeSalerExpenses();
    }
}
