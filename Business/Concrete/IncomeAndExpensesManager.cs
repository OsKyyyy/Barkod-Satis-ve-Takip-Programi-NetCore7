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
    }
}
