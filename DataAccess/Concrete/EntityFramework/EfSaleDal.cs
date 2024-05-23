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
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Response.Sale;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfSaleDal : EfEntityRepositoryBase<Sale, DataBaseContext>, ISaleDal
    {
        public int Add(Sale sale)
        {
            using (var context = new DataBaseContext())
            {
                context.Entry(sale).State = EntityState.Added;
                context.SaveChanges();
                return sale.Id;
            }
        }

        public void AddProducts(SaleProduct saleProduct)
        {
            using (var context = new DataBaseContext())
            {
                context.Entry(saleProduct).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void HardDelete(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Sales.FirstOrDefault(u => u.Id == id);

                context.Sales.Remove(result);
                context.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Sales.FirstOrDefault(u => u.Id == id);

                result.Deleted = true;

                context.SaveChanges();
            }
        }

        public ViewModel ListById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var data = context.Sales.GroupJoin(
                    context.Customers,
                    s => s.CustomerId,
                    c => c.Id,
                    (s, c) => new { s, c })
                    .Select(sale =>
                        new ViewModel
                        {
                            Id = sale.s.Id,
                            CreateDate = sale.s.CreateDate,
                            CustomerId = sale.c.FirstOrDefault().Id,
                            CustomerName = sale.c.FirstOrDefault().Name,
                            Amount = sale.s.Amount,
                            PaymentType = sale.s.PaymentType,
                            ComplateType = sale.s.ComplateType,
                            Deleted = sale.s.Deleted
                        }
                    )
                    .Where(s => s.Id == id)
                    .FirstOrDefault();

                return data;
            }
        }

    }
}
