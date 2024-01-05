using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response.WholeSalerMovement;

namespace DataAccess.Abstract
{
    public interface IWholeSalerMovementDal : IEntityRepository<WholeSalerMovement>
    {
        void Add(WholeSalerMovement wholeSalerMovement);
        void Update(WholeSalerMovement wholeSalerMovement);
        List<ViewModel> ListByWholeSalerId(int id);
        void Delete(int id);
        ViewModel ListById(int id);
    }
}
