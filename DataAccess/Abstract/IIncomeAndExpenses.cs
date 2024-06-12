using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.IncomeAndExpenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IIncomeAndExpensesDal : IEntityRepository<IncomeAndExpensesType>
    {
        void AddType(IncomeAndExpensesType incomeAndExpensesType);
        void UpdateType(IncomeAndExpensesType incomeAndExpensesType);
        void DeleteType(int id);
        List<ViewModel> ListType();
        ViewModel ListTypeById(int id);
    }
}
