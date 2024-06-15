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
    public class EfIncomeAndExpensesDal : EfEntityRepositoryBase<IncomeAndExpenses, DataBaseContext>, IIncomeAndExpensesDal
    {
        public void Add(IncomeAndExpenses incomeAndExpenses)
        {
            using (var context = new DataBaseContext())
            {
                context.Entry(incomeAndExpenses).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(IncomeAndExpenses incomeAndExpenses)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.IncomeAndExpenses.FirstOrDefault(c => c.Id == incomeAndExpenses.Id);
             
                result.IncomeExpensesTypeId = incomeAndExpenses.IncomeExpensesTypeId;
                result.Type = incomeAndExpenses.Type;
                result.Description = incomeAndExpenses.Description;
                result.Amount = incomeAndExpenses.Amount;
                result.Status = incomeAndExpenses.Status;
                result.UpdateDate = incomeAndExpenses.UpdateDate;
                result.UpdateUserId = incomeAndExpenses.UpdateUserId;

                context.SaveChanges();
            }
        }

        public List<ViewModel> List()
        {
            using (var context = new DataBaseContext())
            {
                var result = context.IncomeAndExpenses
                    .Join(context.Users,
                    c => c.UpdateUserId,
                    u => u.Id, (c, u) => new { c, u })
                    .Join(context.IncomeAndExpensesTypes,
                    a => a.c.IncomeExpensesTypeId,
                    b => b.Id, (a, b) => new { a, b })
                    .Where(x => x.a.c.Deleted == false).Select(l => new ViewModel
                    {
                        Id = l.a.c.Id,
                        IncomeExpensesTypeId = l.b.Id,
                        IncomeExpensesTypeName = l.b.Name,
                        Type = l.a.c.Type,
                        Description = l.a.c.Description,
                        Amount = l.a.c.Amount,
                        Deleted = l.a.c.Deleted,
                        Status = l.a.c.Status,
                        UpdateDate = l.a.c.UpdateDate,
                        UpdateUserName = l.a.u.FirstName + " " + l.a.u.LastName,
                    }).ToList();

                return result;
            }
        }

        public void Delete(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.IncomeAndExpenses.FirstOrDefault(u => u.Id == id);

                result.Deleted = true;

                context.SaveChanges();
            }
        }

        public ViewModel ListById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.IncomeAndExpenses
                    .Join(context.Users,
                    c => c.UpdateUserId,
                    u => u.Id, (c, u) => new { c, u })
                    .Join(context.IncomeAndExpensesTypes,
                    a => a.c.IncomeExpensesTypeId,
                    b => b.Id, (a, b) => new { a, b })
                    .Where(x => x.a.c.Deleted == false).Select(l => new ViewModel
                    {
                        Id = l.a.c.Id,
                        IncomeExpensesTypeId = l.b.Id,
                        IncomeExpensesTypeName = l.b.Name,
                        Type = l.a.c.Type,
                        Description = l.a.c.Description,
                        Amount = l.a.c.Amount,
                        Deleted = l.a.c.Deleted,
                        Status = l.a.c.Status,
                        UpdateDate = l.a.c.UpdateDate,
                        UpdateUserName = l.a.u.FirstName + " " + l.a.u.LastName,
                    }).FirstOrDefault(x => x.Id == id);

                return result;
            }
        }
    }
}
