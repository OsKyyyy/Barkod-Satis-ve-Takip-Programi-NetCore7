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
using Core.Utilities.Refit.Models.Response.IncomeAndExpenses;

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
        public void UpdateType(IncomeAndExpensesType incomeAndExpensesType)
        {
            using (var context = new DataBaseContext())
            {

                var result = context.IncomeAndExpensesTypes.FirstOrDefault(c => c.Id == incomeAndExpensesType.Id);

                result.Name = incomeAndExpensesType.Name;
                result.Status = incomeAndExpensesType.Status;
                result.UpdateDate = incomeAndExpensesType.UpdateDate;
                result.UpdateUserId = incomeAndExpensesType.UpdateUserId;

                context.SaveChanges();
            }
        }

        public void DeleteType(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.IncomeAndExpensesTypes.FirstOrDefault(u => u.Id == id);

                result.Deleted = true;
                result.Status = false;

                context.SaveChanges();
            }
        }

        public List<ViewModel> ListType()
        {
            using (var context = new DataBaseContext())
            {
                var result = context.IncomeAndExpensesTypes.Join(context.Users,
                    c => c.UpdateUserId,
                    u => u.Id, (c, u) => new { c, u }).Where(x => x.c.Deleted == false).Select(l => new ViewModel
                    {
                        Id = l.c.Id,
                        Name = l.c.Name,
                        Status = l.c.Status,
                        UpdateDate = l.c.UpdateDate,
                        UpdateUserName = l.u.FirstName + " " + l.u.LastName,
                    }).ToList();

                return result;
            }
        }

        public ViewModel ListTypeById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.IncomeAndExpensesTypes.Join(context.Users,
                    c => c.UpdateUserId,
                    u => u.Id, (c, u) => new { c, u }).Where(x => x.c.Deleted == false).Select(l => new ViewModel
                    {
                        Id = l.c.Id,
                        Name = l.c.Name,
                        Status = l.c.Status,
                        UpdateDate = l.c.UpdateDate,
                        UpdateUserName = l.u.FirstName + " " + l.u.LastName,
                    }).FirstOrDefault(c => c.Id == id);

                return result;
            }
        }
    }
}
