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
using Core.Utilities.Refit.Models.Response.Category;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, DataBaseContext>, ICategoryDal
    {
        public Category Add(Category category)
        {
            using (var context = new DataBaseContext())
            {
                context.Entry(category).State = EntityState.Added;
                context.SaveChanges();

                return category;
            }
        }

        public Category Update(Category category)
        {
            using (var context = new DataBaseContext())
            {

                var result = context.Categories.FirstOrDefault(c => c.Id == category.Id);

                result.Name = category.Name;
                result.Status = category.Status;
                result.UpdateDate = category.UpdateDate;
                result.UpdateUserId = category.UpdateUserId;

                context.SaveChanges();

                return category;
            }
        }
      
        public Category Delete(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Categories.FirstOrDefault(u => u.Id == id);

                result.Deleted = true;
                result.Status = false;

                context.SaveChanges();

                return result;
            }
        }
        
        public List<ViewModel> List()
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Categories.Join(context.Users,
                    c => c.UpdateUserId,
                    u => u.Id, (c, u) => new { c, u }).Where(x => x.c.Deleted == false).Select(l => new ViewModel
                {
                        Id = l.c.Id,
                        Name = l.c.Name,
                        Status = l.c.Status,
                        UpdateDate = l.c.UpdateDate,
                        UpdateUserName = l.u.FirstName + " " + l.u.LastName,
                }).ToList();

                return result;
            }
        }

        public List<ViewModel> ListByActive()
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Categories.Join(context.Users,
                    c => c.UpdateUserId,
                    u => u.Id, (c, u) => new { c, u }).Where(x => x.c.Deleted == false).Where(x => x.c.Status == true).Select(l => new ViewModel
                {
                    Id = l.c.Id,
                    Name = l.c.Name,
                    Status = l.c.Status,
                    UpdateDate = l.c.UpdateDate,
                    UpdateUserName = l.u.FirstName + " " + l.u.LastName,
                }).ToList();

                return result;
            }
        }

        public ViewModel ListById(int id)
        {
            using (var context = new DataBaseContext())
            {
                var result = context.Categories.Join(context.Users,
                    c => c.UpdateUserId,
                    u => u.Id, (c, u) => new { c, u }).Where(x => x.c.Deleted == false).Select(l => new ViewModel
                {
                    Id = l.c.Id,
                    Name = l.c.Name,
                    Status = l.c.Status,
                    UpdateDate = l.c.UpdateDate,
                    UpdateUserName = l.u.FirstName + " " + l.u.LastName,
                }).FirstOrDefault(c => c.Id == id);

                return result;
            }
        }
    }
}
