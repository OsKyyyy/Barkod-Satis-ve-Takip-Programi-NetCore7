using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response.Customer;

namespace DataAccess.Abstract
{
    public interface ICustomerDal : IEntityRepository<Customer>
    {
        void Add(Customer customer);
        void Update(Customer customer);
        List<ViewModel> List();
        List<ViewModel> ListActive();
        void Delete(int id);
        ViewModel ListById(int id);
    }
}
