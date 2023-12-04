using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Runtime.ConstrainedExecution;
using Core.Entities;

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
    }
}
