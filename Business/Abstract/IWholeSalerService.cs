using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response.WholeSaler;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IWholeSalerService
    {
        IResult Add(WholeSalerAddDto wholeSaler);
        IResult Update(WholeSalerUpdateDto category);
        IDataResult<List<ViewModel>> List();
        IResult Delete(int id);
        IDataResult<ViewModel> ListById(int id);
    }
}
