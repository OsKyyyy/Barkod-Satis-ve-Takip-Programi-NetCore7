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
using Core.Utilities.Refit.Models.Response.WholeSalerMovement;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfWholeSalerMovementDal : EfEntityRepositoryBase<WholeSalerMovement, DataBaseContext>, IWholeSalerMovementDal
    {
        public void Add(WholeSalerMovement wholeSalerMovement)
        {
            using (var context = new DataBaseContext())
            {
                context.Entry(wholeSalerMovement).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(WholeSalerMovement wholeSalerMovement)
        {
            using (var context = new DataBaseContext())
            {

                var result = context.WholeSalerMovements.FirstOrDefault(c => c.Id == wholeSalerMovement.Id);

                result.ProcessType = wholeSalerMovement.ProcessType;
                result.Amount = wholeSalerMovement.Amount;
                result.InvoiceDate = wholeSalerMovement.InvoiceDate;
                result.Note = wholeSalerMovement.Note;
                result.Image = wholeSalerMovement.Image;
                result.Status = wholeSalerMovement.Status;
                result.UpdateDate = wholeSalerMovement.UpdateDate;
                result.UpdateUserId = wholeSalerMovement.UpdateUserId;

                context.SaveChanges();
            }
        }

        public List<ViewModel> ListByWholeSalerId(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.WholeSalerMovements.Join(context.Users,
                    w => w.UpdateUserId,
                    u => u.Id, (w, u) => new { w, u }).Where(x => x.w.Deleted == false).Where(x => x.w.WholeSalerId == id).Select(l => new ViewModel
                {
                    Id = l.w.Id,
                    WholeSalerId = l.w.WholeSalerId,
                    ProcessType = l.w.ProcessType,
                    Amount = l.w.Amount,
                    InvoiceDate = l.w.InvoiceDate,
                    Note = l.w.Note,
                    Image = l.w.Image,
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
                var result = context.WholeSalerMovements.FirstOrDefault(u => u.Id == id);

                result.Deleted = true;
                result.Status = false;

                context.SaveChanges();
            }
        }

        public ViewModel ListById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.WholeSalerMovements.Join(context.Users,
                    w => w.UpdateUserId,
                    u => u.Id, (w, u) => new { w, u }).Where(x => x.w.Deleted == false).Select(l => new ViewModel
                {
                    Id = l.w.Id,
                    WholeSalerId = l.w.WholeSalerId,
                    ProcessType = l.w.ProcessType,
                    Amount = l.w.Amount,
                    InvoiceDate = l.w.InvoiceDate,
                    Note = l.w.Note,
                    Image = l.w.Image,
                    Status = l.w.Status,
                    UpdateDate = l.w.UpdateDate,
                    UpdateUserName = l.u.FirstName + " " + l.u.LastName,
                }).FirstOrDefault(c => c.Id == id);

                return result;
            }
        }
    }
}
