using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response.Report;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IReportService
    {
        IDataResult<List<SalesReportViewModel>> SalesReport();
        IDataResult<List<SalesDetailReportViewModel>> SalesDetailReport(DateTime date);
    }
}
