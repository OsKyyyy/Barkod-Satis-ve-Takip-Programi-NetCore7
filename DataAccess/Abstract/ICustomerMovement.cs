using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response.CustomerMovement;

namespace DataAccess.Abstract
{
    public interface ICustomerMovementDal : IEntityRepository<CustomerMovement>
    {
        void Add(CustomerMovement customerMovement);
        void Update(CustomerMovement customerMovement);
        List<ViewModel> ListByCustomerId(int id);
        void Delete(int id);
        void DeleteBySaleId(int saleId);
        ViewModel ListById(int id);
    }
}
