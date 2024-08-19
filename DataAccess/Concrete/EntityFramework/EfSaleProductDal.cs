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
using Entities.Dtos;
using Core.Utilities.Refit.Models.Response.SaleProduct;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfSaleProductDal : EfEntityRepositoryBase<SaleProduct, DataBaseContext>, ISaleProductDal
    {
        public void Add(SaleProduct saleProduct)
        {
            using (var context = new DataBaseContext())
            {
                context.Entry(saleProduct).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ViewModel> ListById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = from sp in context.SaleProducts
                             join sa in context.Sales on sp.SaleId equals sa.Id into salesGroup
                             from sa in salesGroup.DefaultIfEmpty()
                             join cu in context.Customers on sa.CustomerId equals cu.Id into customersGroup
                             from cu in customersGroup.DefaultIfEmpty()
                             join pr in context.Products on sp.Barcode equals pr.Barcode into productsGroup
                             from pr in productsGroup.DefaultIfEmpty()
                             where sa != null && sa.Id == id
                             select new ViewModel
                             {
                                 Id = sp.Id,
                                 SaleId = sp.SaleId,
                                 ProductId = pr.Id == null ? 0: pr.Id,
                                 ProductImage = pr.Image,
                                 ProductBarcode = sp.Barcode,
                                 ProductName = sp.ProductName,
                                 ProductUnitPrice = sp.ProductUnitPrice,
                                 ProductQuantity = sp.ProductQuantity,
                                 Amount = sa.Amount,
                                 PaymentType = sa.PaymentType,
                                 CreateDate = sa.CreateDate,
                                 Deleted = sa.Deleted,
                                 CustomerId = cu.Id,
                                 CustomerName = cu.Name,
                                 CustomerAddress = cu.Address,
                                 CustomerPhone = cu.Phone,
                                 CustomerEmail = cu.Email
                             };

                return result.ToList();
            }
        }
    }
}
