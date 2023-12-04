using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Dtos;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IDataResult<Category> Add(CategoryAddDto categoryAddDto)
        {
            var category = new Category
            {
                Name = categoryAddDto.Name,
                Status = categoryAddDto.Status
            };
            _categoryDal.Add(category);

            return new SuccessDataResult<Category>(category, Messages.CategoryAdded);
        }
    }
}
