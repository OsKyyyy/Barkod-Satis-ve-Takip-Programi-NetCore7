using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Business.ValidationRules.FluentValidation.User;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Security.Hashing;
using Entities.Dtos;
using DataAccess.Concrete.EntityFramework;
using Core.Utilities.Refit.Models.Response.User;

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

        [SecuredOperation("Admin")]
        public void Add(User user)
        {
            _userDal.Add(user);
        }

        [SecuredOperation("Admin")]
        public IResult Delete(int id)
        {
            _userDal.Delete(id);
            return new SuccessResult(Messages.UserDeleted);
        }

        public User GetByMail(string email, bool status = false)
        {
            if (status)
            {
                return _userDal.Get(u => u.Email == email && u.Status == true);
            }
            return _userDal.Get(u => u.Email == email);
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(UpdateValidator))]
        public IDataResult<User> Update(UserUpdateDto userUpdateDto)
        {
            var user = new User
            {
                Id = userUpdateDto.Id,
                Email = userUpdateDto.Email,
                FirstName = userUpdateDto.FirstName,
                LastName = userUpdateDto.LastName,
                Phone = userUpdateDto.Phone,
                Status = userUpdateDto.Status
            };
            _userDal.Update(user);

            return new SuccessDataResult<User>(user, Messages.UserUpdated);
        }

        [SecuredOperation("Admin")]
        public IDataResult<List<User>> List()
        {
	        var result = _userDal.List();

	        return new SuccessDataResult<List<User>>(result, Messages.UsersListed);
        }

        [SecuredOperation("Admin")]
        public IDataResult<User> ListByMail(string email)
        {
	        var result = _userDal.ListByMail(email);
	        if (result == null)
	        {
		        return new ErrorDataResult<User>(Messages.UserNotFound);
	        }

	        return new SuccessDataResult<User>(result, Messages.UserInfoListed);
        }

        [SecuredOperation("Admin")]
        public IDataResult<User> ListById(int id)
        {
            var result = _userDal.ListById(id);
            if (result == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            return new SuccessDataResult<User>(result, Messages.UserInfoListed);
        }

        [SecuredOperation("Admin")]
        public IResult UserExistsByUpdate(string email, int Id)
        {
            if (_userDal.Get(u => u.Email == email && u.Id != Id) != null)
            {
                 return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult(Messages.UserInfoListed);
        }

        public IDataResult<OperationClaim> CheckExistsByName(string name)
        {
            var result = _userDal.CheckExistsByName(name);
            if (result == null)
            {
                return new SuccessDataResult<OperationClaim>(Messages.RoleNotFound);
            }

            return new ErrorDataResult<OperationClaim>(result, Messages.RoleAlreadyExists);
        }

        public IDataResult<OperationClaim> CheckExistsByNameAndId(int id, string name)
        {
            var result = _userDal.CheckExistsByNameAndId(id,name);
            if (result == null)
            {
                return new SuccessDataResult<OperationClaim>(Messages.RoleNotFound);
            }

            return new ErrorDataResult<OperationClaim>(result, Messages.RoleAlreadyExists);
        }

        public IDataResult<OperationClaim> AddOperationClaim(string name)
        {
            var operationClaim = new OperationClaim
            {
                Name = name         
            };
            var result = _userDal.AddOperationClaim(operationClaim);

            return new SuccessDataResult<OperationClaim>(result,Messages.OperationClaimAdded);
        }

        public IDataResult<OperationClaim> UpdateOperationClaim(int id, string name)
        {
            var operationClaim = new OperationClaim
            {
                Id = id,
                Name = name
            };
            var result = _userDal.UpdateOperationClaim(operationClaim);

            return new SuccessDataResult<OperationClaim>(result, Messages.OperationClaimAdded);
        }

        public IResult AddPageClaim(List<string> selectedItems, int id)
        {
            var pageClaim = new PageClaimAddDto
            {
                Id = id,
                SelectedItems = selectedItems
            };
            _userDal.AddPageClaim(pageClaim);

            return new SuccessResult(Messages.PageClaimAdded);
        }
        
        public IResult DeletePageClaim(int id)
        {
            _userDal.DeletePageClaim(id);
            return new SuccessResult(Messages.PageClaimsDeleted);
        }

        public IDataResult<List<RoleListViewModel>> RoleList()
        {
            var result = _userDal.RoleList();

            return new SuccessDataResult<List<RoleListViewModel>>(result, Messages.RoleListed);
        }

        public IDataResult<RoleListViewModel> GetRoleById(int id)
        {
            var result = _userDal.GetRoleById(id);

            return new SuccessDataResult<RoleListViewModel>(result, Messages.RoleListed);
        }

        public IDataResult<RoleListViewModel> GetRoleByName(string name)
        {
            var result = _userDal.GetRoleByName(name);

            return new SuccessDataResult<RoleListViewModel>(result, Messages.RoleListed);
        }       
    }
}
