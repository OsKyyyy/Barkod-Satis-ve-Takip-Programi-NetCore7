using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response.CustomerMovement;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface ICustomerMovementService
    {
        IResult Add(CustomerMovementAddDto customerMovement);
        IResult Update(CustomerMovementUpdateDto customerMovement);
        IDataResult<List<ViewModel>> ListByCustomerId(int id);
        IResult Delete(int id);
        IDataResult<ViewModel> ListById(int id);
    }
}
