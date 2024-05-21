using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleProductController : Controller
    {
        private ISaleProductService _saleProductService;

        public SaleProductController(ISaleProductService saleProductService)
        {
            _saleProductService = saleProductService;            
        }

        [Route("ListById")]
        [HttpGet]
        public ActionResult ListById(int id)
        {
            var result = _saleProductService.ListById(id);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
