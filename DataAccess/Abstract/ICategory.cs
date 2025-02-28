﻿using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Refit.Models.Response.Category;

namespace DataAccess.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
        void Add(Category category);
        void Update(Category category);
        List<ViewModel> List();
        List<ViewModel> ListByActive();
        void Delete(int id);
        ViewModel ListById(int id);
    }
}
