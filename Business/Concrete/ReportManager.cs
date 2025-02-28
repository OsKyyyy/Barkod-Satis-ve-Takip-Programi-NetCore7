﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.Report;
using SaleViewModel = Core.Utilities.Refit.Models.Response.Sale.ViewModel;
using CustomerMovementViewModel = Core.Utilities.Refit.Models.Response.CustomerMovement.ViewModel;
using WholeSalerMovementViewModel = Core.Utilities.Refit.Models.Response.WholeSalerMovement.ViewModel;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Dtos;
using Core.Utilities.Refit.Models.Response.SaleProduct;

namespace Business.Concrete
{    
    public class ReportManager : IReportService
    {
        IReportDal _reportDal;

        public ReportManager(IReportDal reportDal)
        {
            _reportDal = reportDal;
        }

        [SecuredOperation("report_salesreport")]
        public IDataResult<List<SalesReportViewModel>> SalesReport()
        {
            var result = _reportDal.SalesReport();

            return new SuccessDataResult<List<SalesReportViewModel>>(result, Messages.SaleReportListed);
        }

        [SecuredOperation("report_salesdetailreport")]
        public IDataResult<List<SaleViewModel>> SalesDetailReport(DateTime date)
        {
            var result = _reportDal.SalesDetailReport(date);

            return new SuccessDataResult<List<SaleViewModel>>(result, Messages.SaleDetailReportListed);
        }

        #region Müşteri Raporları
        [SecuredOperation("report_customershoppinglist")]
        public IDataResult<List<CustomerMovementViewModel>> GetLastCustomerWithDebt()
        {
            var result = _reportDal.GetLastCustomerWithDebt();

            return new SuccessDataResult<List<CustomerMovementViewModel>>(result, Messages.GetLastCustomerWithDebtListed);
        }

        [SecuredOperation("report_customerrefundlist")]
        public IDataResult<List<CustomerMovementViewModel>> GetLastCustomerWithDebtPayment()
        {
            var result = _reportDal.GetLastCustomerWithDebtPayment();

            return new SuccessDataResult<List<CustomerMovementViewModel>>(result, Messages.GetLastCustomerWithDebtPaymentListed);
        }

        [SecuredOperation("report_customertotaldebtlist")]
        public IDataResult<CustomerTotalDebtViewModel> GetCustomerTotalDebt()
        {
            var result = _reportDal.GetCustomerTotalDebt();

            return new SuccessDataResult<CustomerTotalDebtViewModel>(result, Messages.GetCustomerTotalDebtListed);
        }

        [SecuredOperation("report_customertotaldebtlist")]
        public IDataResult<List<CustomerDebtViewModel>> GetCustomerDebt()
        {
            var result = _reportDal.GetCustomerDebt();

            return new SuccessDataResult<List<CustomerDebtViewModel>>(result, Messages.GetCustomerDebtListed);
        }
        
        [SecuredOperation("report_customernonpayerslist")] 
        public IDataResult<List<CustomerNonPayersViewModel>> GetCustomerNonPayers()
        {
            var result = _reportDal.GetCustomerNonPayers();

            return new SuccessDataResult<List<CustomerNonPayersViewModel>>(result, Messages.GetCustomerNonPayersListed);
        }

        public IDataResult<CustomerTotalDebtViewModel> GetCustomerThisMonthDebt()
        {
            var result = _reportDal.GetCustomerThisMonthDebt();

            return new SuccessDataResult<CustomerTotalDebtViewModel>(result, Messages.GetCustomerThisMonthDebtListed);
        }

        public IDataResult<CustomerTotalDebtViewModel> GetCustomerPreviousMonthDebt()
        {
            var result = _reportDal.GetCustomerPreviousMonthDebt();

            return new SuccessDataResult<CustomerTotalDebtViewModel>(result, Messages.GetCustomerPreviousMonthDebtListed);
        }

        public IDataResult<List<CustomerMonthlyDebtViewModel>> GetCustomerMonthlyDebtOfOneYear()
        {
            var result = _reportDal.GetCustomerMonthlyDebtOfOneYear();

            return new SuccessDataResult<List<CustomerMonthlyDebtViewModel>>(result, Messages.GetCustomerMonthlyDebtListed);
        }
        #endregion

        #region Toptancı Raporları
        [SecuredOperation("report_wholesalershoppinglist")]
        public IDataResult<List<WholeSalerMovementViewModel>> GetLastWholeSalerWithDebt()
        {
            var result = _reportDal.GetLastWholeSalerWithDebt();

            return new SuccessDataResult<List<WholeSalerMovementViewModel>>(result, Messages.GetLastWholeSalerWithDebtListed);
        }

        [SecuredOperation("report_wholesalerrefundlist")]
        public IDataResult<List<WholeSalerMovementViewModel>> GetLastWholeSalerWithDebtPayment()
        {
            var result = _reportDal.GetLastWholeSalerWithDebtPayment();

            return new SuccessDataResult<List<WholeSalerMovementViewModel>>(result, Messages.GetLastWholeSalerWithDebtPaymentListed);
        }

        [SecuredOperation("report_wholesalertotaldebtlist")]
        public IDataResult<WholeSalerTotalDebtViewModel> GetWholeSalerTotalDebt()
        {
            var result = _reportDal.GetWholeSalerTotalDebt();

            return new SuccessDataResult<WholeSalerTotalDebtViewModel>(result, Messages.GetWholeSalerTotalDebtListed);
        }

        [SecuredOperation("report_wholesalertotaldebtlist")]
        public IDataResult<List<WholeSalerDebtViewModel>> GetWholeSalerDebt()
        {
            var result = _reportDal.GetWholeSalerDebt();

            return new SuccessDataResult<List<WholeSalerDebtViewModel>>(result, Messages.GetWholeSalerDebtListed);
        }

        [SecuredOperation("report_wholesalernonpayerslist")]
        public IDataResult<List<WholeSalerNonPayersViewModel>> GetWholeSalerNonPayers()
        {
            var result = _reportDal.GetWholeSalerNonPayers();

            return new SuccessDataResult<List<WholeSalerNonPayersViewModel>>(result, Messages.GetWholeSalerNonPayersListed);
        }

        public IDataResult<WholeSalerTotalDebtViewModel> GetWholeSalerThisMonthDebt()
        {
            var result = _reportDal.GetWholeSalerThisMonthDebt();

            return new SuccessDataResult<WholeSalerTotalDebtViewModel>(result, Messages.GetWholeSalerThisMonthDebtListed);
        }

        public IDataResult<WholeSalerTotalDebtViewModel> GetWholeSalerPreviousMonthDebt()
        {
            var result = _reportDal.GetWholeSalerPreviousMonthDebt();

            return new SuccessDataResult<WholeSalerTotalDebtViewModel>(result, Messages.GetWholeSalerPreviousMonthDebtListed);
        }

        public IDataResult<List<WholeSalerMonthlyDebtViewModel>> GetWholeSalerMonthlyDebtOfOneYear()
        {
            var result = _reportDal.GetWholeSalerMonthlyDebtOfOneYear();

            return new SuccessDataResult<List<WholeSalerMonthlyDebtViewModel>>(result, Messages.GetWholeSalerMonthlyDebtListed);
        }
        #endregion

        #region Gelir-Gider Raporları
        public IDataResult<IncomeExpensesTotalViewModel> MonthlyExternalIncome()
        {
            var result = _reportDal.MonthlyExternalIncome();

            return new SuccessDataResult<IncomeExpensesTotalViewModel>(result, Messages.MonthlyExternalIncomeListed);
        }

        public IDataResult<IncomeExpensesTotalViewModel> MonthlyExternalExpenses()
        {
            var result = _reportDal.MonthlyExternalExpenses();

            return new SuccessDataResult<IncomeExpensesTotalViewModel>(result, Messages.MonthlyExternalExpensesListed);
        }

        public IDataResult<IncomeExpensesTotalViewModel> MonthlySalesIncome()
        {
            var result = _reportDal.MonthlySalesIncome();

            return new SuccessDataResult<IncomeExpensesTotalViewModel>(result, Messages.MonthlySalesIncomeListed);
        }
        
        public IDataResult<IncomeExpensesTotalViewModel> MonthlyWholeSalerExpenses()
        {
            var result = _reportDal.MonthlyWholeSalerExpenses();

            return new SuccessDataResult<IncomeExpensesTotalViewModel>(result, Messages.MonthlyWholeSalerExpensesListed);
        }

        public IDataResult<List<IncomeExpensesMonthlyTotalViewModel>> YearlyExternalIncome()
        {
            var result = _reportDal.YearlyExternalIncome();

            return new SuccessDataResult<List<IncomeExpensesMonthlyTotalViewModel>>(result, Messages.YearlyExternalIncomeListed);
        }
        
        public IDataResult<List<IncomeExpensesMonthlyTotalViewModel>> YearlyExternalExpenses()
        {
            var result = _reportDal.YearlyExternalExpenses();

            return new SuccessDataResult<List<IncomeExpensesMonthlyTotalViewModel>>(result, Messages.YearlyExternalExpensesListed);
        }

        public IDataResult<List<IncomeExpensesMonthlyTotalViewModel>> YearlySalesIncome()
        {
            var result = _reportDal.YearlySalesIncome();

            return new SuccessDataResult<List<IncomeExpensesMonthlyTotalViewModel>>(result, Messages.YearlySalesIncomeListed);
        }
        
        public IDataResult<List<IncomeExpensesMonthlyTotalViewModel>> YearlyWholeSalerExpenses()
        {
            var result = _reportDal.YearlyWholeSalerExpenses();

            return new SuccessDataResult<List<IncomeExpensesMonthlyTotalViewModel>>(result, Messages.YearlyWholeSalerExpensesListed);
        }
        #endregion
    }
}
