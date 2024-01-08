using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;
using Core.Utilities.Refit.Models.Response.Customer;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IResult Add(CustomerAddDto customer);
        IResult Update(CustomerUpdateDto customer);
        IDataResult<List<ViewModel>> List();
        IDataResult<List<ViewModel>> ListActive();
        IResult Delete(int id);
        IDataResult<ViewModel> ListById(int id);
    }
}
