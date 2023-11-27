using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public User GetInfoByMail(string email)
        {
            using (var context = new DataBaseContext())
            {
                var result = 
                    from users in context.Users
                    where users.Email == email
                    select new User
                    {
                        Id = users.Id, 
                        FirstName = users.FirstName, 
                        LastName = users.LastName,
                        Email = users.Email,
                        Phone = users.Phone,
                        Status = users.Status,
                    };

                return result.FirstOrDefault();
            }
        }
    }
}
