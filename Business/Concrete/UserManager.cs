using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

        [SecuredOperation("Admin")]
		public IDataResult<List<User>> List()
        {
	        var userToCheck = _userDal.List();

	        return new SuccessDataResult<List<User>>(userToCheck, Messages.UsersListed);
        }

        [SecuredOperation("Admin")]
		public IDataResult<User> ListByMail(string email)
        {
	        var userToCheck = _userDal.ListByMail(email);
	        if (userToCheck == null)
	        {
		        return new ErrorDataResult<User>(Messages.UserNotFound);
	        }

	        return new SuccessDataResult<User>(userToCheck, Messages.UserInfoListed);
        }

        [SecuredOperation("Admin")]
        public IDataResult<User> ListById(int id)
        {
            var userToCheck = _userDal.ListById(id);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.UserInfoListed);
        }
    }
}
