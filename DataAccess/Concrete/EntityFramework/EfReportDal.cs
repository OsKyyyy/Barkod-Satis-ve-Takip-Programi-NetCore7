using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.Report;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfReportDal : EfEntityRepositoryBase<Sale, DataBaseContext>, IReportDal
    {
        public List<SalesReportViewModel> SalesReport()
        {
            using (var context = new DataBaseContext())
            {
                var data = context.Sales.GroupBy(d => d.CreateDate.Date).Select(g => new
                    {
                        CreateDate = g.Key,
                        Amount = g.Sum(s => s.Amount),
                        Piece = g.Count()
                    })
                    .ToList();

            }

            return null;
        }
    }
}
