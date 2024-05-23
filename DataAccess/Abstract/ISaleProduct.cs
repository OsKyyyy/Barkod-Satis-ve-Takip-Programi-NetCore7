using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.SaleProduct;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ISaleProductDal : IEntityRepository<SaleProduct>
    {
        void Add(SaleProduct saleProductAddDto);
        void Delete(int id);
        List<ViewModel> ListById(int id);
    }
}
