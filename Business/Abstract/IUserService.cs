using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;
using Core.Utilities.Refit.Models.Response.User;

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
        IResult UserExistsByUpdate(string email, int Id);
        IDataResult<User> ListByMail(string email);
        IDataResult<User> ListById(int id);
        IDataResult<OperationClaim> CheckExistsByName(string name);
        IDataResult<OperationClaim> CheckExistsByNameAndId(int id, string name);
        IDataResult<OperationClaim> AddOperationClaim(string name);
        IDataResult<OperationClaim> UpdateOperationClaim(int id, string name);
        IResult AddPageClaim(List<string> selectedItems, int id);
        IResult DeletePageClaim(int id);        
        IDataResult<List<RoleListViewModel>> RoleList();
        IDataResult<RoleListViewModel> GetRoleByName(string name);

    }
}
