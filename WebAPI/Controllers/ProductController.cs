using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Abstract;
using Core.Utilities.Refit.Models.Request.Product;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly CultureInfo _culture = new("en-US");

        private IProductService _productService;
        private IWholeSalerMovementService _wholeSalerMovementService;

        public ProductController(IProductService productService, IWholeSalerMovementService wholeSalerMovementService)
        {
            _productService = productService;
            _wholeSalerMovementService = wholeSalerMovementService;
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add(ProductAddDto productAddDto)
        {
            var checkExistsByBarcode= _productService.CheckExistsByBarcode(productAddDto.Barcode);
            if (checkExistsByBarcode.Status)
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

        [Route("StockEntry")]
        [HttpPost]
        public ActionResult StockEntry(StockEntryRequestModel stockEntryRequestModel)
        {
            var checkExistsByBarcode = _productService.CheckExistsByBarcode(stockEntryRequestModel.Barcode);
            if (!checkExistsByBarcode.Status)
            {
                return BadRequest(checkExistsByBarcode);
            }

            var stockEntry = _productService.StockEntry(stockEntryRequestModel);
            if (!stockEntry.Status)
            {
                return BadRequest(stockEntry);
            }

            //var amount = Convert.ToDecimal(stockEntryRequestModel.TotalCost,_culture) - Convert.ToDecimal(stockEntryRequestModel.PaymentAmount,_culture);
            //byte paymentType = 1;

            //if (amount < 1)
            //{
            //    amount = -amount;
            //    paymentType = 2;
            //}

            //WholeSalerMovementAddDto wholeSalerMovementAddDto = new WholeSalerMovementAddDto
            //{
            //    WholeSalerId = stockEntryRequestModel.WholeSalerId,
            //    Image = stockEntryRequestModel.Image,
            //    Amount = Convert.ToString(amount,_culture),
            //    ProcessType = paymentType,
            //    InvoiceDate = DateTime.Now,
            //    CreateUserId = stockEntryRequestModel.UpdateUserId
            //};

            WholeSalerMovementAddDto wholeSalerMovementAddDtoTotalCost = new WholeSalerMovementAddDto
            {
                WholeSalerId = stockEntryRequestModel.WholeSalerId,
                Image = stockEntryRequestModel.Image,
                Amount = stockEntryRequestModel.TotalCost,
                ProcessType = 1,
                InvoiceDate = DateTime.Now,
                CreateUserId = stockEntryRequestModel.UpdateUserId
            };

            var resultTotalCost = _wholeSalerMovementService.Add(wholeSalerMovementAddDtoTotalCost);

            if (resultTotalCost.Status)
            {
                WholeSalerMovementAddDto wholeSalerMovementAddDtoPaymentAmount = new WholeSalerMovementAddDto
                {
                    WholeSalerId = stockEntryRequestModel.WholeSalerId,
                    Image = stockEntryRequestModel.Image,
                    Amount = stockEntryRequestModel.PaymentAmount,
                    ProcessType = 2,
                    InvoiceDate = DateTime.Now,
                    CreateUserId = stockEntryRequestModel.UpdateUserId
                };

                var resultPaymentAmount = _wholeSalerMovementService.Add(wholeSalerMovementAddDtoPaymentAmount);

                if (resultPaymentAmount.Status)
                {
                    return Ok(resultTotalCost);
                }

                return BadRequest(resultTotalCost);
            }

            return BadRequest(resultTotalCost);
        }
        
        [Route("Update")]
        [HttpPut]
        public ActionResult Update(ProductUpdateDto productUpdateDto)
        {
            var checkExistByBarcodeAndId = _productService.CheckExistsByBarcodeAndId(productUpdateDto.Id, productUpdateDto.Barcode);
            if (!checkExistByBarcodeAndId.Status)
            {
                return BadRequest(checkExistByBarcodeAndId);
            }

            var result = _productService.Update(productUpdateDto);

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

            return BadRequest(result);
        }

        [Route("ListById")]
        [HttpGet]
        public ActionResult ListById(int id)
        {
            var listById = _productService.ListById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            return Ok(listById);
        }

        [Route("ListByFavorite")]
        [HttpGet]
        public ActionResult ListByFavorite()
        {
            var list = _productService.ListByFavorite();
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }

        [Route("ListByBarcode")]
        [HttpGet]
        public ActionResult ListByBarcode(string barcode)
        {
            var list = _productService.ListToPos(barcode);
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }

        [Route("ListByName")]
        [HttpGet]
        public ActionResult ListByName(string name)
        {
            var list = _productService.ListByName(name);
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }

        [Route("ListToSavePhoto")]
        [HttpGet]
        public ActionResult ListToSavePhoto(string barcode)
        {
            var list = _productService.ListToSavePhoto(barcode);
            return Ok(list);
        }

        [Route("UpdateImage")]
        [HttpPut]
        public ActionResult UpdateImage(UpdateImageRequestModel updateImageRequestModel)
        {
            var result = _productService.UpdateImage(updateImageRequestModel);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
