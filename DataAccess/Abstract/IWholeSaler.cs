using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response.WholeSaler;
using Core.Utilities.Refit.Models.Response.HomePage;

namespace DataAccess.Abstract
{
    public interface IWholeSalerDal : IEntityRepository<WholeSaler>
    {
        void Add(WholeSaler wholeSaler);
        void Update(WholeSaler wholeSaler);
        List<ViewModel> List();
        void Delete(int id);
        ViewModel ListById(int id);
        TotalCurrentAccountViewModel GetTotalWholeSaler();
    }
}
