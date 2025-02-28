﻿using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response.Product;
using Entities.Dtos;
using Core.Utilities.Refit.Models.Request.Product;

namespace Business.Abstract
{
    public interface IProductService
    {
        IResult Add(ProductAddDto product);
        IResult StockEntry(StockEntryRequestModel stockEntryRequestModel);
        IResult Update(ProductUpdateDto product);
        IResult UpdateStock(string barcode, int quantity);
        IResult UpdateAddStock(string barcode, int quantity);
        IDataResult<List<ViewModel>> List();
        IResult Delete(int id);
        IDataResult<ViewModel> ListById(int id);
        IDataResult<List<ViewModel>> ListByFavorite();
        IDataResult<List<ViewModel>> ListByCategory(int id);
        IDataResult<List<ViewModel>> ListByName(string name);
        IDataResult<ViewModel> ListToPos(string barcode);
        IDataResult<ViewModel> CheckExistsByBarcode(string barcode);
        IResult CheckExistsByBarcodeAndId(int id, string barcode);
        bool ListToSavePhoto(string barcode);
        IResult UpdateImage(UpdateImageRequestModel updateImageRequestModel);

    }
}
