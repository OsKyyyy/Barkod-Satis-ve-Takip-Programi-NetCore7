using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response.WholeSalerMovement;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IWholeSalerMovementService
    {
        IResult Add(WholeSalerMovementAddDto wholeSalerMovement);
        IResult Update(WholeSalerMovementUpdateDto wholeSalerMovement);
        IDataResult<List<ViewModel>> ListByWholeSalerId(int id);
        IResult Delete(int id);
        IDataResult<ViewModel> ListById(int id);
    }
}
