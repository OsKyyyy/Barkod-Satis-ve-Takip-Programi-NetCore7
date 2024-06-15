using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Business.ValidationRules.FluentValidation.IncomeAndExpensesType;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.IncomeAndExpensesType;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Dtos;

namespace Business.Concrete
{
    [SecuredOperation("Admin")]
    public class IncomeAndExpensesTypeManager : IIncomeAndExpensesTypeService
    {
        private readonly CultureInfo _culture = new("en-US");

        IIncomeAndExpensesTypeDal _incomeAndExpensesTypeDal;

        public IncomeAndExpensesTypeManager(IIncomeAndExpensesTypeDal incomeAndExpensesDal)
        {
            _incomeAndExpensesTypeDal = incomeAndExpensesDal;
        }        

        [ValidationAspect(typeof(AddValidator))]
        public IResult Add(IncomeAndExpensesTypeAddDto incomeAndExpensesTypeAddDto)
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
            _incomeAndExpensesTypeDal.Add(incomeAndExpensesType);

            return new SuccessResult(Messages.IncomeAndExpensesTypeAdded);
        }

        [ValidationAspect(typeof(UpdateValidator))]
        public IResult Update(IncomeAndExpensesTypeUpdateDto incomeAndExpensesTypeUpdateDto)
        {
            var incomeAndExpensesType = new IncomeAndExpensesType
            {
                Id = incomeAndExpensesTypeUpdateDto.Id,
                Name = incomeAndExpensesTypeUpdateDto.Name,
                Status = incomeAndExpensesTypeUpdateDto.Status,
                UpdateUserId = incomeAndExpensesTypeUpdateDto.UpdateUserId,
                UpdateDate = DateTime.Now,
            };
            _incomeAndExpensesTypeDal.Update(incomeAndExpensesType);

            return new SuccessResult(Messages.IncomeAndExpensesTypeUpdated);
        }

        public IResult Delete(int id)
        {
            _incomeAndExpensesTypeDal.Delete(id);
            return new SuccessResult(Messages.IncomeAndExpensesTypeDeleted);
        }

        public IDataResult<List<ViewModel>> List()
        {
            var result = _incomeAndExpensesTypeDal.List();

            return new SuccessDataResult<List<ViewModel>>(result, Messages.IncomeAndExpensesTypeListed);
        }

        public IDataResult<ViewModel> ListById(int id)
        {
            var result = _incomeAndExpensesTypeDal.ListById(id);
            if (result == null)
            {
                return new ErrorDataResult<ViewModel>(Messages.IncomeAndExpensesTypeNotFound);
            }

            return new SuccessDataResult<ViewModel>(result, Messages.IncomeAndExpensesTypeInfoListed);
        }

        public IDataResult<List<ViewModel>> ListByActive()
        {
            var result = _incomeAndExpensesTypeDal.ListByActive();

            return new SuccessDataResult<List<ViewModel>>(result, Messages.IncomeAndExpensesTypeListed);
        }
    }
}
