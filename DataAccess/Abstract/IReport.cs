using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response.Report;

namespace DataAccess.Abstract
{
    public interface IReportDal : IEntityRepository<Sale>
    {
        List<SalesReportViewModel> SalesReport();
        List<SalesDetailReportViewModel> SalesDetailReport(DateTime date);
        List<SalesProductsDetailReportViewModel> SalesProductsDetailReport(int id);
        SalesDetailReportViewModel SalesDetailReportById(int id);
        void SalesDelete(int id);
        void UpdateStock(string barcode, int quantity);
    }
}
