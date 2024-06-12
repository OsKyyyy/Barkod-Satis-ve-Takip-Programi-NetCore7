using System;
using System.Collections.Generic;
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
    [SecuredOperation("Admin")]
    public class IncomeAndExpensesManager : IIncomeAndExpensesService
    {
        IIncomeAndExpensesDal _incomeAndExpensesDal;

        public IncomeAndExpensesManager(IIncomeAndExpensesDal incomeAndExpensesDal)
        {
            _incomeAndExpensesDal = incomeAndExpensesDal;
        }

        [ValidationAspect(typeof(AddValidator))]
        public IResult AddType(IncomeAndExpensesTypeAddDto incomeAndExpensesTypeAddDto)
        {
            var incomeAndExpensesType = new IncomeAndExpensesType
            {
                Name = incomeAndExpensesTypeAddDto.Name,
                Status = incomeAndExpensesTypeAddDto.Status,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreateUserId = incomeAndExpensesTypeAddDto.CreateUserId,
                UpdateUserId = incomeAndExpensesTypeAddDto.CreateUserId,
                Deleted = false
            };
            _incomeAndExpensesDal.AddType(incomeAndExpensesType);

            return new SuccessResult(Messages.IncomeAndExpensesTypeAdded);
        }

        [ValidationAspect(typeof(UpdateValidator))]
        public IResult UpdateType(IncomeAndExpensesTypeUpdateDto incomeAndExpensesTypeUpdateDto)
        {
            var incomeAndExpensesType = new IncomeAndExpensesType
            {
                Id = incomeAndExpensesTypeUpdateDto.Id,
                Name = incomeAndExpensesTypeUpdateDto.Name,
                Status = incomeAndExpensesTypeUpdateDto.Status,
                UpdateUserId = incomeAndExpensesTypeUpdateDto.UpdateUserId,
                UpdateDate = DateTime.Now,
            };
            _incomeAndExpensesDal.UpdateType(incomeAndExpensesType);

            return new SuccessResult(Messages.IncomeAndExpensesTypeUpdated);
        }

        public IResult DeleteType(int id)
        {
            _incomeAndExpensesDal.DeleteType(id);
            return new SuccessResult(Messages.IncomeAndExpensesTypeDeleted);
        }

        public IDataResult<List<ViewModel>> ListType()
        {
            var result = _incomeAndExpensesDal.ListType();

            return new SuccessDataResult<List<ViewModel>>(result, Messages.IncomeAndExpensesTypeListed);
        }

        public IDataResult<ViewModel> ListTypeById(int id)
        {
            var result = _incomeAndExpensesDal.ListTypeById(id);
            if (result == null)
            {
                return new ErrorDataResult<ViewModel>(Messages.IncomeAndExpensesTypeNotFound);
            }

            return new SuccessDataResult<ViewModel>(result, Messages.IncomeAndExpensesTypeInfoListed);
        }
    }
}
