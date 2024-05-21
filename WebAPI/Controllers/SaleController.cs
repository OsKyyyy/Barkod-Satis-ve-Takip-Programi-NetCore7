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
        private ISaleProductService _saleProductService;
        private IProductService _productService;

        public SaleController(ISaleService saleService, ISaleProductService saleProductService, IProductService productService)
        {
            _saleService = saleService;
            _saleProductService = saleProductService;
            _productService = productService;
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

        [Route("SalesDelete")]
        [HttpDelete]
        public ActionResult SalesDelete(int id)
        {
            var listById = _saleService.ListById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _saleService.Delete(id);

            if (result.Status)
            {
                var salesProducts = _saleProductService.ListById(id);

                foreach (var item in salesProducts.Data)
                {
                    _productService.UpdateAddStock(item.ProductBarcode, item.ProductQuantity);
                }

                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
