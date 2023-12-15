using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IUserService
    {
        User GetByMail(string email, bool status = false);
        List<OperationClaim> GetClaims(User user);

        void Add(User user);
        IResult Delete(int id);
        IDataResult<User> Update(UserUpdateDto user);
        IDataResult<List<User>> List();
        IDataResult<List<User>> InActiveList();
        IResult UserExistsByUpdate(string email, int Id);
        IDataResult<User> ListByMail(string email);
        IDataResult<User> ListById(int id);

	}
}
