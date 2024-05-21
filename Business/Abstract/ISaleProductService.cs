using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;
using Core.Utilities.Refit.Models.Response.SaleProduct;

namespace Business.Abstract
{
    public interface ISaleProductService
    {
        IResult Add(SaleProductAddDto saleProductAddDto);
        IDataResult<List<ViewModel>> ListById(int id);
    }
}
