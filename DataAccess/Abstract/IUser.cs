using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.User;
using Entities.Dtos;
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
        OperationClaim CheckExistsByName(string name);
        OperationClaim CheckExistsByNameAndId(int id, string name);
        OperationClaim AddOperationClaim(OperationClaim operationClaim);
        OperationClaim UpdateOperationClaim(OperationClaim operationClaim);
        void AddPageClaim(PageClaimAddDto pageClaimAddDto);
        void DeletePageClaim(int id);
        List<RoleListViewModel> RoleList();
        RoleListViewModel GetRoleByName(string name);
    }
}
