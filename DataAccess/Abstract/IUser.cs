using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        User Update(User user);
        User Delete(int id);
        List<User> List();
        User ListByMail(string email);
        User ListById(int id);
        List<OperationClaim> GetClaims(User user);
    }
}
