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
using Core.Utilities.Refit.Models.Response.WholeSaler;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfWholeSalerDal : EfEntityRepositoryBase<WholeSaler, DataBaseContext>, IWholeSalerDal
    {
        public void Add(WholeSaler wholeSaler)
        {
            using (var context = new DataBaseContext())
            {
                context.Entry(wholeSaler).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(WholeSaler wholeSaler)
        {
            using (var context = new DataBaseContext())
            {

                var result = context.WholeSalers.FirstOrDefault(c => c.Id == wholeSaler.Id);

                result.Name = wholeSaler.Name;
                result.AuthorizedPerson = wholeSaler.AuthorizedPerson;
                result.Phone = wholeSaler.Phone;
                result.Email = wholeSaler.Email;
                result.Address = wholeSaler.Address;
                result.Status = wholeSaler.Status;
                result.UpdateDate = wholeSaler.UpdateDate;
                result.UpdateUserId = wholeSaler.UpdateUserId;

                context.SaveChanges();
            }
        }

        public List<ViewModel> List()
        {
            using (var context = new DataBaseContext())
            {
                var result = context.WholeSalers.Join(context.Users,
                    w => w.UpdateUserId,
                    u => u.Id, (w, u) => new { w, u }).Where(x => x.w.Deleted == false).Select(l => new ViewModel
                {
                    Id = l.w.Id,
                    Name = l.w.Name,
                    AuthorizedPerson = l.w.AuthorizedPerson,
                    Phone = l.w.Phone,
                    Email = l.w.Email,
                    Address = l.w.Address,
                    Status = l.w.Status,
                    UpdateDate = l.w.UpdateDate,
                    UpdateUserName = l.u.FirstName + " " + l.u.LastName,
                }).ToList();

                return result;
            }
        }

        public void Delete(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.WholeSalers.FirstOrDefault(u => u.Id == id);

                result.Deleted = true;
                result.Status = false;

                context.SaveChanges();
            }
        }

        public ViewModel ListById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.WholeSalers.Join(context.Users,
                    w => w.UpdateUserId,
                    u => u.Id, (w, u) => new { w, u }).Where(x => x.w.Deleted == false).Select(l => new ViewModel
                {
                    Id = l.w.Id,
                    Name = l.w.Name,
                    AuthorizedPerson = l.w.AuthorizedPerson,
                    Phone = l.w.Phone,
                    Email = l.w.Email,
                    Address = l.w.Address,
                    Status = l.w.Status,
                    UpdateDate = l.w.UpdateDate,
                    UpdateUserName = l.u.FirstName + " " + l.u.LastName,
                }).FirstOrDefault(c => c.Id == id);

                return result;
            }
        }

    }
}
