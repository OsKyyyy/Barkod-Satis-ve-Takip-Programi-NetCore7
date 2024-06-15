using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.IncomeAndExpensesType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IIncomeAndExpensesTypeDal : IEntityRepository<IncomeAndExpensesType>
    {
        void Add(IncomeAndExpensesType incomeAndExpensesType);
        void Update(IncomeAndExpensesType incomeAndExpensesType);
        void Delete(int id);
        List<ViewModel> List();
        ViewModel ListById(int id);
        List<ViewModel> ListByActive();
    }
}
