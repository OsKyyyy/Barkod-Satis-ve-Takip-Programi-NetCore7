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
    public interface IIncomeAndExpensesDal : IEntityRepository<IncomeAndExpenses>
    {
        void Add(IncomeAndExpenses incomeAndExpenses);
        void Update(IncomeAndExpenses incomeAndExpenses);
        List<ViewModel> List();
        void Delete(int id);
        ViewModel ListById(int id);
    }
}
