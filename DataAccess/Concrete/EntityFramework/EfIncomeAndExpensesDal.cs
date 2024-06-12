using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using Core.Entities;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfIncomeAndExpensesDal : EfEntityRepositoryBase<IncomeAndExpensesType, DataBaseContext>, IIncomeAndExpensesDal
    {
        public void AddType(IncomeAndExpensesType incomeAndExpensesType)
        {
            using (var context = new DataBaseContext())
            {
                context.Entry(incomeAndExpensesType).State = EntityState.Added;
                context.SaveChanges();
            }
        }      
    }
}
