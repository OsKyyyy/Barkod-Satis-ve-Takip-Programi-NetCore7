﻿using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.HomePage;
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
        int Add(User user);
        User Update(User user);
        User Delete(int id);
        List<User> List();
        User ListByMail(string email);
        ViewModel ListById(int id);
        List<OperationClaim> GetClaims(User user);
        OperationClaim CheckExistsByName(string name);
        OperationClaim CheckExistsByNameAndId(int id, string name);
        OperationClaim AddOperationClaim(OperationClaim operationClaim);
        OperationClaim UpdateOperationClaim(OperationClaim operationClaim);
        void AddPageClaim(PageClaimAddDto pageClaimAddDto);
        void DeletePageClaim(int id);
        List<RoleListViewModel> RoleList();
        RoleListViewModel GetRoleById(int id);
        RoleListViewModel GetRoleByName(string name);
        void AddUserOperationClaim(UserOperationClaim userOperationClaim);
        void UpdateUserRole(UserRoleUpdateDto userRoleUpdateDto);
        void UpdateUserPassword(User user);
        void UpdateUserEmail(UserEmailUpdateDto userEmailUpdateDto);
        TotalCurrentAccountViewModel GetTotalUser();
    }
}
