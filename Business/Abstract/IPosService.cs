using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;
using Core.Utilities.Refit.Models.Response.Pos;

namespace Business.Abstract
{
    public interface IPosService
    {
        IResult Add(PosAddDto pos);
        IResult AddMoney(PosAddMoneyDto pos);
        IDataResult<List<ViewModel>> List(int createUserId);
        IDataResult<ViewModel> ListById(int id);
        IResult Delete(int id);
        IResult QuantityIncrease(int id);
        IResult QuantityReduce(int id);
    }
}
