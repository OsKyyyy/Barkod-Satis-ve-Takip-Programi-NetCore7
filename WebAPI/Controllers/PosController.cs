using Business.Abstract;
using Business.Constant;
using Core.Utilities.Results.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosController : Controller
    {
        private IPosService _posService;
        private IProductService _productService;

        public PosController(IPosService posService, IProductService productService)
        {
            _posService = posService;
            _productService = productService;
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add(PosAddDto posAddDto)
        {
            var ListToPos = _productService.ListToPos(posAddDto.Barcode);

            if (!ListToPos.Status)
            {
                return BadRequest(ListToPos);
            }

            PosAddDto newPosAddDto = new PosAddDto
            {
                Basket = posAddDto.Basket,
                Barcode = posAddDto.Barcode,
                ProductName = ListToPos.Data.Name,
                ProductUnitPrice = ListToPos.Data.SalePrice,
                ProductUnitPurchasePrice = ListToPos.Data.PurchasePrice,
                ProductQuantity = posAddDto.Quantity,
                CreateUserId = posAddDto.CreateUserId
            };

            var result = _posService.Add(newPosAddDto);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("AddMoney")]
        [HttpPost]
        public ActionResult AddMoney(PosAddMoneyDto posAddMoneyDto)
        {
            var result = _posService.AddMoney(posAddMoneyDto);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("List")]
        [HttpGet]
        public ActionResult List(int createUserId)
        {
            var list = _posService.List(createUserId);
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }

        [Route("QuantityIncrease")]
        [HttpPut]
        public ActionResult QuantityIncrease(int id, string barcode)
        {
            var listById = _posService.ListById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var listByBarcode = _productService.ListToPos(barcode);
            if (!listByBarcode.Status || listByBarcode.Data.Stock < listById.Data.ProductQuantity + 1)
            {
                return BadRequest(new ErrorResult(Messages.MaximumStockReached));

            }

            var result = _posService.QuantityIncrease(id);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("QuantityReduce")]
        [HttpPut]
        public ActionResult QuantityReduce(int id)
        {
            var listById = _posService.ListById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _posService.QuantityReduce(id);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("Delete")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var listById = _posService.ListById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _posService.Delete(id);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("CancelSale")]
        [HttpGet]
        public ActionResult CancelSale(int basket, int creteUserId)
        {
            var ListByBasket = _posService.ListByBasket(basket, creteUserId);
            if (!ListByBasket.Status)
            {
                return BadRequest(ListByBasket);
            }

            var result = _posService.CancelSale(basket);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}