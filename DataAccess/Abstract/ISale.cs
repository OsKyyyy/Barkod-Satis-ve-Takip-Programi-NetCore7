using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ISaleDal : IEntityRepository<Sale>
    {
        int Add(Sale sale);
        void AddProducts(SaleProduct saleProduct);
        void HardDelete(int id);
        void Delete(int id);
        ViewModel ListById(int id);
    }
}
