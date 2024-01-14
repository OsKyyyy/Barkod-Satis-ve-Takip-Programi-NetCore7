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
        void Add(Pos pos);
        void AddMoney(Pos pos);
        void Delete(int id);
        void CancelSale(int basket);
        int QuantityIncrease(int id);
        int QuantityReduce(int id);
        List<ViewModel> List(int createUserId);
        ViewModel ListById(int id);
        List<ViewModel> ListByBasket(int basket, int createUserId);
    }
}
