using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response.Product;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IProductService
    {
        IResult Add(ProductAddDto product);
        IDataResult<List<ViewModel>> List();
        IDataResult<List<ViewModel>> InActiveList();
        IResult Delete(int id);
        IDataResult<ViewModel> ListById(int id);
        IDataResult<ViewModel> CheckExistsByBarcode(string barcode);
    }
}
