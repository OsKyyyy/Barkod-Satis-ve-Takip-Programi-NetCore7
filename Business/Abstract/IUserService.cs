using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        void Add(User user);
        IDataResult<List<User>> List();
		User GetByMail(string email);
        IDataResult<User> ListByMail(string email);
        IDataResult<User> ListById(int id);

	}
}
