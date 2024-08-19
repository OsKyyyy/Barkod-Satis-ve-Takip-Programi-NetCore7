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
using Core.Utilities.Refit.Models.Response.Sale;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Dtos;

namespace Business.Concrete
{
    [SecuredOperation("pos_sale")]
    public class SaleManager : ISaleService
    {
        private readonly CultureInfo _culture = new("en-US");

        ISaleDal _saleDal;
        ISaleProductDal _saleProductDal;
        IPosDal _posDal;
        IProductService _productService;
        ICustomerMovementService _customerMovementService;

        public SaleManager(ISaleDal saleDal, IPosDal posDal, IProductService productService, ICustomerMovementService customerMovementService, ISaleProductDal saleProductDal)
        {
            _saleDal = saleDal;
            _posDal = posDal;
            _productService = productService;
            _customerMovementService = customerMovementService;
            _saleProductDal = saleProductDal;
        }

        [ValidationAspect(typeof(AddValidator))]
        public IDataResult<ViewModel> Add(SaleAddDto saleAddDto)
        {
            var sale = new Sale
            {
                CustomerId = saleAddDto.CustomerId,
                Amount = decimal.Parse(saleAddDto.Amount,_culture),
                PaymentType = saleAddDto.PaymentType,
                ComplateType = saleAddDto.ComplateType,
                CreateUserId = saleAddDto.CreateUserId,
                CreateDate = DateTime.Now,
                Deleted = false
            };
            var saleId = _saleDal.Add(sale);
            var viewModel = new ViewModel()
            {
                Id = saleId,
            };

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
                            ProductUnitPurchasePrice = item.ProductUnitPurchasePrice,
                            ProductQuantity = item.ProductQuantity,
                            Deleted = false
                        };

                        _saleProductDal.Add(saleProduct);

                        if (item.ProductName != "Muhtelif Tutar") // ÜRÜN STOĞUNU ADET KADAR GÜNCELLEME
                        {
                            _productService.UpdateStock(item.Barcode, item.ProductQuantity);
                        }                       
                    }

                    if (saleAddDto.CustomerId != null) // MÜŞTERİ SEÇİLMİŞ İSE MÜŞTERİYİ BORÇLANDIR
                    {
                        CustomerMovementAddDto customerMovementAddDto = new CustomerMovementAddDto();

                        customerMovementAddDto.CustomerId = (int)saleAddDto.CustomerId;
                        customerMovementAddDto.SaleId = saleId;
                        customerMovementAddDto.Amount = saleAddDto.Amount;
                        customerMovementAddDto.ProcessType = 1;
                        customerMovementAddDto.ProductInformation = customerMovementProducts;
                        customerMovementAddDto.Note = "Bu hareket barkod satış sisteminden yapılan sipariş ile eklenmiştir.";
                        customerMovementAddDto.Status = false;
                        customerMovementAddDto.CreateUserId = saleAddDto.CreateUserId;

                        if (saleAddDto.ComplateType == 2)
                        {
                            customerMovementAddDto.Amount = saleAddDto.PartialPaymentAmount;
                            customerMovementAddDto.Note = "Bu hareket Kısmi Ödeme yapılarak barkod satış sisteminden eklenmiştir. Toplam Tutar : " + saleAddDto.Amount;
                        }

                        _customerMovementService.Add(customerMovementAddDto);
                    }

                    _posDal.CancelSale(saleAddDto.Basket); // SATIŞI TAMAMLANAN SEPETİ BOŞALTMA
                }
                else // STOKDA OLMAYAN ÜRÜN VARSA SATIŞI İPTAL ET
                {
                    _saleDal.HardDelete(saleId);
                    return new ErrorDataResult<ViewModel>(Messages.ProductOutOfStock);
                }
            }
            else
            {
                return new ErrorDataResult<ViewModel>(viewModel, Messages.PosNotFound);
            }

            return new SuccessDataResult<ViewModel>(viewModel,Messages.SaleAdded);
        }

        public IResult Delete(int id)
        {
            _saleDal.Delete(id);
            return new SuccessResult(Messages.SalesDeleted);
        }

        public IDataResult<ViewModel> ListById(int id)
        {
            var result = _saleDal.ListById(id);
            if (result == null)
            {
                return new ErrorDataResult<ViewModel>(Messages.SaleDetailReportNotFound);
            }

            return new SuccessDataResult<ViewModel>(result, Messages.SaleDetailReportListed);
        }
    }
}