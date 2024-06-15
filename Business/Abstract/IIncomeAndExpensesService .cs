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
        IResult Add(IncomeAndExpensesAddDto incomeAndExpensesAddDto);
        IResult Update(IncomeAndExpensesUpdateDto incomeAndExpensesUpdateDto);
        IDataResult<List<ViewModel>> List();
        IResult Delete(int id);
        IDataResult<ViewModel> ListById(int id);
    }
}
