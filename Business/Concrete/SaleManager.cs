using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Business.ValidationRules.FluentValidation.Sale;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Dtos;

namespace Business.Concrete
{
    [SecuredOperation("Admin")]
    public class SaleManager : ISaleService
    {
        private readonly CultureInfo _culture = new("en-US");

        ISaleDal _saleDal;
        IPosDal _posDal;
        IProductService _productService;
        ICustomerMovementService _customerMovementService;

        public SaleManager(ISaleDal saleDal, IPosDal posDal, IProductService productService, ICustomerMovementService customerMovementService)
        {
            _saleDal = saleDal;
            _posDal = posDal;
            _productService = productService;
            _customerMovementService = customerMovementService;
        }

        [ValidationAspect(typeof(AddValidator))]
        public IResult Add(SaleAddDto saleAddDto)
        {
            var sale = new Sale
            {
                CustomerId = saleAddDto.CustomerId,
                Amount = decimal.Parse(saleAddDto.Amount,_culture),
                PaymentType = saleAddDto.PaymentType,
                CreateUserId = saleAddDto.CreateUserId,
                CreateDate = DateTime.Now,
                Deleted = false
            };
            var saleId = _saleDal.Add(sale);

            var listByBasket = _posDal.ListByBasket(saleAddDto.Basket, saleAddDto.CreateUserId);

            if (listByBasket != null)
            {
                var stockError = 0;
                foreach (var item in listByBasket)
                {
                    if (item.ProductName != "Muhtelif Tutar")
                    {
                        var result = _productService.ListToPos(item.Barcode);
                        if (!result.Status || result.Data.Stock < item.ProductQuantity)
                        {
                            stockError = 1;
                        }
                    }
                }

                if (stockError != 1)
                {
                    var customerMovementProducts = "";

                    foreach (var item in listByBasket) // ÜRÜNLERİ SALEPRODUCTS TABLOSUNA KAYDETME
                    {
                        if (customerMovementProducts == "") // ÜRÜNLERİ TEK BİR STRING OLARAK BİRLEŞTİR
                        {
                            customerMovementProducts = item.ProductName;
                        }
                        else
                        {
                            customerMovementProducts += "," + item.ProductName;
                        }

                        var saleProduct = new SaleProduct
                        {
                            SaleId = saleId,
                            Barcode = item.Barcode,
                            ProductName = item.ProductName,
                            ProductUnitPrice = item.ProductUnitPrice,
                            ProductQuantity = item.ProductQuantity,
                            Deleted = false
                        };

                        _saleDal.AddProducts(saleProduct);

                        if (item.ProductName != "Muhtelif Tutar") // ÜRÜN STOĞUNU ADET KADAR GÜNCELLEME
                        {
                            _productService.UpdateStock(item.Barcode, item.ProductQuantity);
                        }                       
                    }

                    if (saleAddDto.CustomerId != null) // MÜŞTERİ SEÇİLMİŞ İSE MÜŞTERİYİ BORÇLANDIR
                    {
                        CustomerMovementAddDto customerMovementAddDto = new CustomerMovementAddDto();

                        customerMovementAddDto.CustomerId = (int)saleAddDto.CustomerId;
                        customerMovementAddDto.Amount = saleAddDto.Amount;
                        customerMovementAddDto.ProcessType = 1;
                        customerMovementAddDto.ProductInformation = customerMovementProducts;
                        customerMovementAddDto.Note = "Bu hareket barkod satış sisteminden yapılan sipariş ile eklenmiştir.";
                        customerMovementAddDto.Status = false;
                        customerMovementAddDto.CreateUserId = saleAddDto.CreateUserId;                        

                        _customerMovementService.Add(customerMovementAddDto);
                    }

                    _posDal.CancelSale(saleAddDto.Basket); // SATIŞI TAMAMLANAN SEPETİ BOŞALTMA
                }
                else // STOKDA OLMAYAN ÜRÜN VARSA SATIŞI İPTAL ET
                {
                    _saleDal.Delete(saleId);
                    return new ErrorResult(Messages.ProductOutOfStock);
                }
            }
            else
            {
                return new ErrorResult(Messages.PosNotFound);
            }

            return new SuccessResult(Messages.SaleAdded);
        }
    }
}
