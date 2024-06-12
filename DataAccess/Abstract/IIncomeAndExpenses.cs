using Core.DataAccess;
using Core.Entities.Concrete;
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
    }
}
