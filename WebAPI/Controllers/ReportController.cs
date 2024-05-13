using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [Route("SalesReport")]
        [HttpGet]
        public ActionResult SalesReport()
        {
            var list = _reportService.SalesReport();
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }

        [Route("SalesDetailReport")]
        [HttpGet]
        public ActionResult SalesDetailReport(DateTime date)
        {
            var list = _reportService.SalesDetailReport(date);
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }


        [Route("SalesProductsDetailReport")]
        [HttpGet]
        public ActionResult SalesProductsDetailReport(int id)
        {
            var list = _reportService.SalesProductsDetailReport(id);
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }

        [Route("SalesDelete")]
        [HttpDelete]
        public ActionResult SalesDelete(int id)
        {
            var listById = _reportService.SalesDetailReportById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _reportService.SalesDelete(id);

            if (result.Status)
            {
                var salesProducts = _reportService.SalesProductsDetailReport(id);

                foreach (var item in salesProducts.Data)
                {
                    _reportService.UpdateStock(item.ProductBarcode, item.ProductQuantity);
                }

                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("GetLastCustomerWithDebt")]
        [HttpGet]
        public ActionResult GetLastCustomerWithDebt()
        {
            var list = _reportService.GetLastCustomerWithDebt();
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }

        [Route("GetLastCustomerWithDebtPayment")]
        [HttpGet]
        public ActionResult GetLastCustomerWithDebtPayment()
        {
            var list = _reportService.GetLastCustomerWithDebtPayment();
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }

        [Route("GetCustomerTotalDebt")]
        [HttpGet]
        public ActionResult GetCustomerTotalDebt()
        {
            var list = _reportService.GetCustomerTotalDebt();
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }

        [Route("GetCustomerDebt")]
        [HttpGet]
        public ActionResult GetCustomerDebt()
        {
            var list = _reportService.GetCustomerDebt();
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }

        [Route("GetCustomerNonPayers")]
        [HttpGet]
        public ActionResult GetCustomerNonPayers()
        {
            var list = _reportService.GetCustomerNonPayers();
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }
    }
}
