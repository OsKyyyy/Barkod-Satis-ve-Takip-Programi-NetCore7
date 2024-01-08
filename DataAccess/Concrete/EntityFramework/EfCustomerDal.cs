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
using Core.Utilities.Refit.Models.Response.Customer;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, DataBaseContext>, ICustomerDal
    {
        public void Add(Category category)
        {
            using (var context = new DataBaseContext())
            {
                context.Entry(category).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(Customer customer)
        {
            using (var context = new DataBaseContext())
            {

                var result = context.Customers.FirstOrDefault(c => c.Id == customer.Id);

                result.Name = customer.Name;
                result.Address = customer.Address;
                result.Phone = customer.Phone;
                result.Email = customer.Email;
                result.Status = customer.Status;
                result.UpdateDate = customer.UpdateDate;
                result.UpdateUserId = customer.UpdateUserId;

                context.SaveChanges();
            }
        }

        public List<ViewModel> List()
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Customers.Join(context.Users,
                    c => c.UpdateUserId,
                    u => u.Id, (c, u) => new { c, u }).Where(x => x.c.Deleted == false).Select(l => new ViewModel
                {
                    Id = l.c.Id,
                    Name = l.c.Name,
                    Address = l.c.Address,
                    Phone = l.c.Phone,
                    Email = l.c.Email,
                    Status = l.c.Status,
                    UpdateDate = l.c.UpdateDate,
                    UpdateUserName = l.u.FirstName + " " + l.u.LastName,
                }).ToList();

                return result;
            }
        }

        public List<ViewModel> ListActive()
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Customers.Join(context.Users,
                    c => c.UpdateUserId,
                    u => u.Id, (c, u) => new { c, u }).Where(x => x.c.Status == true).Where(x => x.c.Deleted == false).Select(l => new ViewModel
                {
                    Id = l.c.Id,
                    Name = l.c.Name,
                    Address = l.c.Address,
                    Phone = l.c.Phone,
                    Email = l.c.Email,
                    Status = l.c.Status,
                    UpdateDate = l.c.UpdateDate,
                    UpdateUserName = l.u.FirstName + " " + l.u.LastName,
                }).ToList();

                return result;
            }
        }

        public void Delete(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Customers.FirstOrDefault(u => u.Id == id);

                result.Deleted = true;
                result.Status = false;

                context.SaveChanges();
            }
        }

        public ViewModel ListById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Customers.Join(context.Users,
                    c => c.UpdateUserId,
                    u => u.Id, (c, u) => new { c, u }).Where(x => x.c.Deleted == false).Select(l => new ViewModel
                {
                    Id = l.c.Id,
                    Name = l.c.Name,
                    Address = l.c.Address,
                    Phone = l.c.Phone,
                    Email = l.c.Email,
                    Status = l.c.Status,
                    UpdateDate = l.c.UpdateDate,
                    UpdateUserName = l.u.FirstName + " " + l.u.LastName,
                }).FirstOrDefault(c => c.Id == id);

                return result;
            }
        }
    }
}
