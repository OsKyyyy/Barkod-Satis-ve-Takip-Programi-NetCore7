using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response.Product;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        void Add(Product product);
        void Update(Product product);
        List<ViewModel> List();
        Product Delete(int id);
        ViewModel ListById(int id);
        ViewModel ListToPos(string barcode);
        ViewModel CheckExistsByBarcode(string barcode);
    }
}
