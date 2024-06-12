using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;
using Core.Utilities.Refit.Models.Response.IncomeAndExpenses;

namespace Business.Abstract
{
    public interface IIncomeAndExpensesService
    {
        IResult AddType(IncomeAndExpensesTypeAddDto incomeAndExpensesTypeAddDto);
        IResult UpdateType(IncomeAndExpensesTypeUpdateDto incomeAndExpensesTypeUpdateDto);
        IResult DeleteType(int id);
        IDataResult<List<ViewModel>> ListType();
        IDataResult<ViewModel> ListTypeById(int id);
    }
}
