using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : Controller
    {
        private ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add(SaleAddDto saleAddDto)
        {
            var result = _saleService.Add(saleAddDto);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
