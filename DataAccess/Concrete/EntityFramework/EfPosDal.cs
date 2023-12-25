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
using Core.Utilities.Refit.Models.Response.Pos;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfPosDal : EfEntityRepositoryBase<Pos, DataBaseContext>, IPosDal
    {
        public Pos Add(Pos pos)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Pos.Where(p => p.CreateUserId == pos.CreateUserId).FirstOrDefault(c => c.Barcode == pos.Barcode);

                if (result == null)
                {
                    context.Entry(pos).State = EntityState.Added;
                    context.SaveChanges();
                }
                else
                {
                    result.ProductQuantity += pos.ProductQuantity;

                    context.SaveChanges();
                }

                return pos;
            }
        }

        public Pos AddMoney(Pos pos)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Pos.Where(p => p.CreateUserId == pos.CreateUserId).FirstOrDefault(c => c.ProductName == pos.ProductName);

                if (result == null)
                {
                    context.Entry(pos).State = EntityState.Added;
                    context.SaveChanges();
                }
                else
                {
                    result.ProductUnitPrice += pos.ProductUnitPrice;

                    context.SaveChanges();
                }

                return pos;
            }
        }

        public List<ViewModel> List(int createUserId)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Pos.Where(p => p.CreateUserId == createUserId).Select(s => new ViewModel
                {
                    Id = s.Id,
                    ProductName = s.ProductName,
                    ProductQuantity = s.ProductQuantity,
                    ProductUnitPrice = s.ProductUnitPrice
                }).ToList();

                return result;
            }
        }

        public ViewModel ListById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Pos.Select(s => new ViewModel
                {
                    Id = s.Id,
                    ProductName = s.ProductName,
                    ProductQuantity = s.ProductQuantity,
                    ProductUnitPrice = s.ProductUnitPrice
                }).FirstOrDefault(c => c.Id == id);

                return result;
            }
        }

        public Pos Delete(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Pos.FirstOrDefault(u => u.Id == id);

                context.Pos.Remove(result);
                context.SaveChanges();

                return result;
            }
        }

        public int QuantityIncrease(int id)
        {
            using (var context = new DataBaseContext())
            {

                var result = context.Pos.FirstOrDefault(p => p.Id == id);

                result.ProductQuantity += 1;
                
                context.SaveChanges();

                return id;
            }
        }

        public int QuantityReduce(int id)
        {
            using (var context = new DataBaseContext())
            {

                var result = context.Pos.FirstOrDefault(p => p.Id == id);

                if (result.ProductQuantity == 1)
                {
                    context.Pos.Remove(result);
                }
                else
                {
                    result.ProductQuantity -= 1;
                }

                context.SaveChanges();

                return id;
            }
        }
    }
}
