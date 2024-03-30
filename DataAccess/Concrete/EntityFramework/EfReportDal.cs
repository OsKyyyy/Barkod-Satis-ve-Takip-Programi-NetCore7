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
    }
}
