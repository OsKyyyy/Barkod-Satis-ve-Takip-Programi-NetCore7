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

        [Route("GetCustomerThisMonthDebt")]
        [HttpGet]
        public ActionResult GetCustomerThisMonthDebt()
        {
            var list = _reportService.GetCustomerThisMonthDebt();
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }

        [Route("GetCustomerPreviousMonthDebt")]
        [HttpGet]
        public ActionResult GetCustomerPreviousMonthDebt()
        {
            var list = _reportService.GetCustomerPreviousMonthDebt();
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }

        [Route("GetCustomerMonthlyDebtOfOneYear")]
        [HttpGet]
        public ActionResult GetCustomerMonthlyDebtOfOneYear()
        {
            var list = _reportService.GetCustomerMonthlyDebtOfOneYear();
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }
    }
}
