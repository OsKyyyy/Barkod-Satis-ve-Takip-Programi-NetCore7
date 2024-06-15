using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;
using Core.Utilities.Refit.Models.Response.IncomeAndExpensesType;

namespace Business.Abstract
{
    public interface IIncomeAndExpensesTypeService
    {
        IResult Add(IncomeAndExpensesTypeAddDto incomeAndExpensesTypeAddDto);
        IResult Update(IncomeAndExpensesTypeUpdateDto incomeAndExpensesTypeUpdateDto);
        IResult Delete(int id);
        IDataResult<List<ViewModel>> List();
        IDataResult<ViewModel> ListById(int id);
        IDataResult<List<ViewModel>> ListByActive();
    }
}
