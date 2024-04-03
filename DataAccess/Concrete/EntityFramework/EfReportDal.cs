using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.Report;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfReportDal : EfEntityRepositoryBase<Sale, DataBaseContext>, IReportDal
    {
        public List<SalesReportViewModel> SalesReport()
        {
            using (var context = new DataBaseContext())
            {
                var data = context.Sales.GroupBy(s => s.CreateDate.Date).Select(s => new SalesReportViewModel
                    {
                        CreateDate = s.Key,
                        Amount = s.Sum(s => s.Amount),
                        Piece = s.Count()
                    }).OrderByDescending(d => d.CreateDate.Date)
                    .ToList();

                return data;
            }
        }

        public List<SalesDetailReportViewModel> SalesDetailReport(DateTime date)
        {
            using (var context = new DataBaseContext())
            {
                var data = context.Sales.GroupJoin(
                    context.Customers, 
                    s => s.CustomerId, 
                    c => c.Id,
                    (s, c) => new { s, c })
                    .Select(sale =>
                        new SalesDetailReportViewModel
                        {
                            Id = sale.s.Id,
                            CreateDate = sale.s.CreateDate,
                            CustomerId = sale.c.FirstOrDefault().Id,
                            CustomerName = sale.c.FirstOrDefault().Name,
                            Amount = sale.s.Amount,
                            PaymentType = sale.s.PaymentType,
                            Deleted = sale.s.Deleted
                        }
                    )
                    .Where(s => s.CreateDate.Date == date.Date)
                    .OrderByDescending(s => s.CreateDate)
                    .ToList();

                return data;
            }
        }

        public List<SalesProductsDetailReportViewModel> SalesProductsDetailReport(int id)
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
                             select new SalesProductsDetailReportViewModel
                             {
                                 Id = sp.Id,
                                 SaleId = sp.SaleId,
                                 ProductId = pr.Id,
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

        public SalesDetailReportViewModel SalesDetailReportById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var data = context.Sales.GroupJoin(
                    context.Customers,
                    s => s.CustomerId,
                    c => c.Id,
                    (s, c) => new { s, c })
                    .Select(sale =>
                        new SalesDetailReportViewModel
                        {
                            Id = sale.s.Id,
                            CreateDate = sale.s.CreateDate,
                            CustomerId = sale.c.FirstOrDefault().Id,
                            CustomerName = sale.c.FirstOrDefault().Name,
                            Amount = sale.s.Amount,
                            PaymentType = sale.s.PaymentType,
                            Deleted = sale.s.Deleted
                        }
                    )
                    .Where(s => s.Id == id)
                    .FirstOrDefault();

                return data;
            }
        }

        public void SalesDelete(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Sales.FirstOrDefault(u => u.Id == id);

                result.Deleted = true;

                context.SaveChanges();
            }
        }

        public void UpdateStock(string barcode, int quantity)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Products.FirstOrDefault(x => x.Barcode == barcode);

                result.Stock += quantity;

                context.SaveChanges();
            }
        }

    }
}
