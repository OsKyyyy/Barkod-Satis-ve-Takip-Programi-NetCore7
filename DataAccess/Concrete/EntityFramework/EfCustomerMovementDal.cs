using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using Core.Entities;
using Core.Utilities.Refit.Models.Response.CustomerMovement;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerMovementDal : EfEntityRepositoryBase<CustomerMovement, DataBaseContext>, ICustomerMovementDal
    {
        public void Add(CustomerMovement customerMovement)
        {
            using (var context = new DataBaseContext())
            {
                context.Entry(customerMovement).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(CustomerMovement customerMovement)
        {
            using (var context = new DataBaseContext())
            {

                var result = context.CustomerMovements.FirstOrDefault(c => c.Id == customerMovement.Id);

                result.ProcessType = customerMovement.ProcessType;
                result.Amount = customerMovement.Amount;
                result.ProductInformation = customerMovement.ProductInformation;
                result.Note = customerMovement.Note;
                result.Status = customerMovement.Status;
                result.UpdateDate = customerMovement.UpdateDate;
                result.UpdateUserId = customerMovement.UpdateUserId;

                context.SaveChanges();
            }
        }

        public List<ViewModel> ListByCustomerId(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.CustomerMovements.Join(context.Users,
                    w => w.UpdateUserId,
                    u => u.Id, (w, u) => new { w, u }).Where(x => x.w.Deleted == false).Where(x => x.w.CustomerId == id).Select(l => new ViewModel
                {
                    Id = l.w.Id,
                    CustomerId = l.w.CustomerId,
                    ProcessType = l.w.ProcessType,
                    Amount = l.w.Amount,
                    ProductInformation = l.w.ProductInformation,
                    Note = l.w.Note,
                    Status = l.w.Status,
                    UpdateDate = l.w.UpdateDate,
                    UpdateUserName = l.u.FirstName + " " + l.u.LastName,
                }).OrderByDescending(x => x.Id).ToList();

                return result;
            }
        }

        public void Delete(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.CustomerMovements.FirstOrDefault(u => u.Id == id);

                result.Deleted = true;
                result.Status = false;

                context.SaveChanges();
            }
        }

        public ViewModel ListById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.CustomerMovements.Join(context.Users,
                    w => w.UpdateUserId,
                    u => u.Id, (w, u) => new { w, u }).Where(x => x.w.Deleted == false).Select(l => new ViewModel
                {
                    Id = l.w.Id,
                    CustomerId = l.w.CustomerId,
                    ProcessType = l.w.ProcessType,
                    Amount = l.w.Amount,
                    ProductInformation = l.w.ProductInformation,
                    Note = l.w.Note,
                    Status = l.w.Status,
                    UpdateDate = l.w.UpdateDate,
                    UpdateUserName = l.u.FirstName + " " + l.u.LastName,
                }).FirstOrDefault(c => c.Id == id);

                return result;
            }
        }
    }
}
