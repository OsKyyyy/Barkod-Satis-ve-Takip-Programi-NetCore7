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
    }
}
