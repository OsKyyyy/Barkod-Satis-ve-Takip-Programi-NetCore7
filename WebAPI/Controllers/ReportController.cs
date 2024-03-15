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
    }
}
