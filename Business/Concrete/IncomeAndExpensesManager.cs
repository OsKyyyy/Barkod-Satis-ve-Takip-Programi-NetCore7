using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Business.ValidationRules.FluentValidation.IncomeAndExpenses;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.IncomeAndExpenses;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Dtos;

namespace Business.Concrete
{    
    public class IncomeAndExpensesManager : IIncomeAndExpensesService
    {
        private readonly CultureInfo _culture = new("en-US");

        IIncomeAndExpensesDal _incomeAndExpensesDal;

        public IncomeAndExpensesManager(IIncomeAndExpensesDal incomeAndExpensesDal)
        {
            _incomeAndExpensesDal = incomeAndExpensesDal;
        }

        [SecuredOperation("incomeandexpenses_add")]
        [ValidationAspect(typeof(AddValidator))]
        public IResult Add(IncomeAndExpensesAddDto incomeAndExpensesAddDto)
        {
            var incomeAndExpenses = new IncomeAndExpenses
            {
                IncomeExpensesTypeId = incomeAndExpensesAddDto.IncomeExpensesTypeId,
                Type = incomeAndExpensesAddDto.Type,
                Description = incomeAndExpensesAddDto.Description,
                Amount = decimal.Parse(incomeAndExpensesAddDto.Amount, _culture),
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreateUserId = incomeAndExpensesAddDto.CreateUserId,
                UpdateUserId = incomeAndExpensesAddDto.CreateUserId,
                Deleted = false,
                Status = incomeAndExpensesAddDto.Status
            };
            _incomeAndExpensesDal.Add(incomeAndExpenses);

            return new SuccessResult(Messages.IncomeAndExpensesAdded);
        }

        [SecuredOperation("incomeandexpenses_add")]
        [ValidationAspect(typeof(UpdateValidator))]
        public IResult Update(IncomeAndExpensesUpdateDto incomeAndExpensesUpdateDto)
        {
            var incomeAndExpenses = new IncomeAndExpenses
            {
                Id = incomeAndExpensesUpdateDto.Id,
                IncomeExpensesTypeId = incomeAndExpensesUpdateDto.IncomeExpensesTypeId,
                Type = incomeAndExpensesUpdateDto.Type,
                Description = incomeAndExpensesUpdateDto.Description,
                Amount = decimal.Parse(incomeAndExpensesUpdateDto.Amount, _culture),                
                UpdateDate = DateTime.Now,
                UpdateUserId = incomeAndExpensesUpdateDto.UpdateUserId,
                Status = incomeAndExpensesUpdateDto.Status
            };
            _incomeAndExpensesDal.Update(incomeAndExpenses);

            return new SuccessResult(Messages.ProductUpdated);
        }

        [SecuredOperation("incomeandexpenses_list")]
        public IDataResult<List<ViewModel>> List()
        {
            var result = _incomeAndExpensesDal.List();

            return new SuccessDataResult<List<ViewModel>>(result, Messages.IncomeAndExpensesListed);
        }

        [SecuredOperation("incomeandexpenses_list")]
        public IResult Delete(int id)
        {
            _incomeAndExpensesDal.Delete(id);
            return new SuccessResult(Messages.IncomeAndExpensesDeleted);
        }

        [SecuredOperation("incomeandexpenses_list")]
        public IDataResult<ViewModel> ListById(int id)
        {
            var result = _incomeAndExpensesDal.ListById(id);
            if (result == null)
            {
                return new ErrorDataResult<ViewModel>(Messages.IncomeAndExpensesNotFound);
            }

            return new SuccessDataResult<ViewModel>(result, Messages.IncomeAndExpensesInfoListed);
        }
    }
}
