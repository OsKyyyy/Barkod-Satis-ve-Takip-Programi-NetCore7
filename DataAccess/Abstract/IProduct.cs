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
        List<ViewModel> List();
        List<ViewModel> InActiveList();
        Product Delete(int id);
        ViewModel ListById(int id);
        ViewModel CheckExistsByBarcode(string barcode);
    }
}
