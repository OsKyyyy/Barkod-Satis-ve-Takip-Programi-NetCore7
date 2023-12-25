using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.Pos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IPosDal : IEntityRepository<Pos>
    {
        Pos Add(Pos pos);
        Pos AddMoney(Pos pos);
        Pos Delete(int id);
        int QuantityIncrease(int id);
        int QuantityReduce(int id);
        List<ViewModel> List(int createUserId);
        ViewModel ListById(int id);
    }
}
