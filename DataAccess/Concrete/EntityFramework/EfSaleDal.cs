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

        public void Delete(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Sales.FirstOrDefault(u => u.Id == id);

                context.Sales.Remove(result);
                context.SaveChanges();
            }
        }
    }
}
