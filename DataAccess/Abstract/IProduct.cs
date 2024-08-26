using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response.Product;
using Core.Utilities.Refit.Models.Request.Product;
using Core.Utilities.Refit.Models.Response.HomePage;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        void Add(Product product);
        bool StockEntry(StockEntryRequestModel stockEntryRequestModel);
        void Update(Product product);
        void UpdateStock(string barcode, int quantity);
        void UpdateAddStock(string barcode, int quantity);
        List<ViewModel> List();
        void Delete(int id);
        ViewModel ListById(int id);
        ViewModel ListToPos(string barcode);
        List<ViewModel> ListByName(string name);
        List<ViewModel> ListByFavorite();
        List<ViewModel> ListByCategory(int id);
        ViewModel CheckExistsByBarcode(string barcode);
        bool ListToSavePhoto(string barcode);
        bool CheckExistsByBarcodeAndId(int id, string barcode);
        void UpdateImage(UpdateImageRequestModel updateImageRequestModel);
        StockQuantityViewModel GetStockQuantity();
        StockValueViewModel GetStockValue();
    }
}
