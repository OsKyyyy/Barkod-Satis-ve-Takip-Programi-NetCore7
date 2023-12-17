using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add(ProductAddDto productAddDto)
        {
            var checkExistsByBarcode= _productService.CheckExistsByBarcode(productAddDto.Barcode);
            if (!checkExistsByBarcode.Status)
            {
                return BadRequest(checkExistsByBarcode);
            }

            var result = _productService.Add(productAddDto);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("List")]
        [HttpGet]
        public ActionResult List()
        {
            var list = _productService.List();
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }
        
        [Route("Delete")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var listById = _productService.ListById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _productService.Delete(id);

            if (result.Status)
            {
                return Ok(result);
            }

            return Ok(result);
        }
    }
}
