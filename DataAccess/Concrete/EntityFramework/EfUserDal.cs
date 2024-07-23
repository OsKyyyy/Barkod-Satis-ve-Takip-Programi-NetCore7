using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Utilities.Refit.Abstract;
using Entities.Dtos;
using Core.Utilities.Refit.Models.Response.User;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, DataBaseContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new DataBaseContext())
            {
                var result = 
                    from operationClaim in context.OperationClaims
                    join userOperationClaim in context.UserOperationClaims
                    on operationClaim.Id equals userOperationClaim.OperationClaimId
                    where userOperationClaim.UserId == user.Id
                    select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();
            }
        }

        public User Update(User user)
        {
            using (var context = new DataBaseContext())
            {
      
                var result = context.Users.FirstOrDefault(u => u.Id == user.Id);

                result.FirstName = user.FirstName;
                result.LastName = user.LastName;
                result.Phone = user.Phone;
                result.Email = user.Email;
                result.Status = user.Status;

                context.SaveChanges();

                return user;
            }
        }

        public User Delete(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Users.FirstOrDefault(u => u.Id == id);

                result.Deleted = true;
                result.Status = false;

                context.SaveChanges();
                
                return result;
            }
        }

        public List<User> List()
        {
	        using (var context = new DataBaseContext())
	        {
                var result = context.Users.Where(x => x.Deleted == false).Select(s => new User
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Phone = s.Phone,
                    Email = s. Email,
                    Status = s. Status,
                }).ToList();

                return result;
            }
        }

		public User ListByMail(string email)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Users.Where(x => x.Deleted == false).Select(s => new User
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Phone = s.Phone,
                    Email = s.Email,
                    Status = s.Status,
                }).FirstOrDefault(u => u.Email == email);

                return result;
            }
        }

        public User ListById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Users.Where(x => x.Deleted == false).Select(s => new User
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Phone = s.Phone,
                    Email = s.Email,
                    Status = s.Status,
                }).FirstOrDefault(u => u.Id == id);

                return result;
            }
        }

        public OperationClaim CheckExistsByName(string name)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.OperationClaims.Select(l => new OperationClaim
                {
                    Id = l.Id,
                    Name = l.Name,
                }).FirstOrDefault(p => p.Name == name);

                return result;
            }
        }

        public OperationClaim CheckExistsByNameAndId(int id, string name)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.OperationClaims.Select(l => new OperationClaim
                {
                    Id = l.Id,
                    Name = l.Name,
                }).FirstOrDefault(p => p.Name == name && p.Id != id);

                return result;
            }
        }

        public OperationClaim AddOperationClaim(OperationClaim operationClaim)
        {
            using (var context = new DataBaseContext())
            {
                context.Entry(operationClaim).State = EntityState.Added;
                context.SaveChanges();

                operationClaim.Id = operationClaim.Id;

                return operationClaim;
            }
        }

        public OperationClaim UpdateOperationClaim(OperationClaim operationClaim)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.OperationClaims.FirstOrDefault(c => c.Id == operationClaim.Id);

                result.Name = operationClaim.Name;               

                context.SaveChanges();

                return operationClaim;
            }
        }

        public async void AddPageClaim(PageClaimAddDto pageClaimAddDto)
        {
            using (var context = new DataBaseContext())
            {
                var pageClaims = pageClaimAddDto.SelectedItems.Select(item => new PageClaim
                {
                    OperationClaimId = pageClaimAddDto.Id,
                    PageName = item
                }).ToList();

                await context.PageClaims.AddRangeAsync(pageClaims);
                await context.SaveChangesAsync();
            }
        }

        public void DeletePageClaim(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.PageClaims.Where(u => u.OperationClaimId == id).ToList();

                foreach (var pageClaim in result)
                {
                    context.PageClaims.Remove(pageClaim);
                }

                context.SaveChanges();
            }
        }

        public List<RoleListViewModel> RoleList()
        {
            using (var context = new DataBaseContext())
            {
                var rolePageClaims = context.OperationClaims
                    .GroupJoin(context.PageClaims,
                        oc => oc.Id,
                        pc => pc.OperationClaimId,
                        (oc, pcs) => new { OperationClaim = oc, PageClaims = pcs })
                    .SelectMany(
                        oc_pc => oc_pc.PageClaims.DefaultIfEmpty(),
                        (oc_pc, pc) => new { oc_pc.OperationClaim, PageClaim = pc })
                    .GroupJoin(context.UserOperationClaims,
                        ocpc => ocpc.OperationClaim.Id,
                        uoc => uoc.OperationClaimId,
                        (ocpc, uocs) => new { ocpc.OperationClaim, ocpc.PageClaim, UserIds = uocs.Select(uoc => uoc.UserId).Distinct().ToList() })
                    .ToList();

                var result = rolePageClaims
                    .GroupBy(rpc => new { rpc.OperationClaim.Id, rpc.OperationClaim.Name })
                    .Select(g => new RoleListViewModel
                    {
                        OperationClaimId = g.Key.Id,
                        RoleName = g.Key.Name,
                        PageNames = g.Where(x => x.PageClaim != null).Select(x => x.PageClaim.PageName).ToList(),
                        UserCount = g.SelectMany(x => x.UserIds).Distinct().Count()
                    })
                    .ToList();

                return result;
            }
        }

        public RoleListViewModel GetRoleById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var rolePageClaims = context.OperationClaims
                     .Where(oc => oc.Id == id)
                     .GroupJoin(context.PageClaims,
                         oc => oc.Id,
                         pc => pc.OperationClaimId,
                         (oc, pcs) => new { OperationClaim = oc, PageClaims = pcs })
                     .SelectMany(
                         oc_pc => oc_pc.PageClaims.DefaultIfEmpty(),
                         (oc_pc, pc) => new { oc_pc.OperationClaim, PageClaim = pc })
                     .GroupJoin(context.UserOperationClaims,
                         ocpc => ocpc.OperationClaim.Id,
                         uoc => uoc.OperationClaimId,
                         (ocpc, uocs) => new { ocpc.OperationClaim, ocpc.PageClaim, UserIds = uocs.Select(uoc => uoc.UserId).Distinct().ToList() })
                     .ToList();

                var result = rolePageClaims
                    .GroupBy(rpc => new { rpc.OperationClaim.Id, rpc.OperationClaim.Name })
                    .Select(g => new RoleListViewModel
                    {
                        OperationClaimId = g.Key.Id,
                        RoleName = g.Key.Name,
                        PageNames = g.Where(x => x.PageClaim != null).Select(x => x.PageClaim.PageName).ToList(),
                        UserCount = g.SelectMany(x => x.UserIds).Distinct().Count(),
                        Users = g.SelectMany(x => x.UserIds).Distinct()
                                  .Join(context.Users,
                                        userId => userId,
                                        user => user.Id,
                                        (userId, user) => new ViewModel
                                        {
                                            Id = user.Id,
                                            FirstName = user.FirstName,
                                            LastName = user.LastName,
                                            Email = user.Email,
                                            Phone = user.Phone,
                                            Status = user.Status
                                        }).ToList()
                    })
                    .FirstOrDefault();

                return result;
            }
        }

        public RoleListViewModel GetRoleByName(string name)
        {
            using (var context = new DataBaseContext())
            {
                var rolePageClaims = context.OperationClaims
                     .Where(oc => oc.Name == name)
                     .GroupJoin(context.PageClaims,
                         oc => oc.Id,
                         pc => pc.OperationClaimId,
                         (oc, pcs) => new { OperationClaim = oc, PageClaims = pcs })
                     .SelectMany(
                         oc_pc => oc_pc.PageClaims.DefaultIfEmpty(),
                         (oc_pc, pc) => new { oc_pc.OperationClaim, PageClaim = pc })
                     .GroupJoin(context.UserOperationClaims,
                         ocpc => ocpc.OperationClaim.Id,
                         uoc => uoc.OperationClaimId,
                         (ocpc, uocs) => new { ocpc.OperationClaim, ocpc.PageClaim, UserIds = uocs.Select(uoc => uoc.UserId).Distinct().ToList() })
                     .ToList();

                var result = rolePageClaims
                    .GroupBy(rpc => new { rpc.OperationClaim.Id, rpc.OperationClaim.Name })
                    .Select(g => new RoleListViewModel
                    {
                        OperationClaimId = g.Key.Id,
                        RoleName = g.Key.Name,
                        PageNames = g.Where(x => x.PageClaim != null).Select(x => x.PageClaim.PageName).ToList(),
                        UserCount = g.SelectMany(x => x.UserIds).Distinct().Count()
                    })
                    .FirstOrDefault();

                return result;
            }
        }      
    }
}
