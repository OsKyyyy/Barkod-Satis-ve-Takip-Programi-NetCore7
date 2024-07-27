using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Business.ValidationRules.FluentValidation.Category;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.Category;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
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

        [SecuredOperation("category_add")]
        [ValidationAspect(typeof(AddValidator))]
        public IResult Add(CategoryAddDto categoryAddDto)
        {
            var category = new Category
            {
                Name = categoryAddDto.Name,
                Status = categoryAddDto.Status,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreateUserId = categoryAddDto.CreateUserId,
                UpdateUserId = categoryAddDto.CreateUserId,
                Deleted = false
            };
            _categoryDal.Add(category);

            return new SuccessResult(Messages.CategoryAdded);
        }

        [SecuredOperation("category_add")]
        [ValidationAspect(typeof(UpdateValidator))]
        public IResult Update(CategoryUpdateDto categoryUpdateDto)
        {
            var category = new Category
            {
                Id = categoryUpdateDto.Id,
                Name = categoryUpdateDto.Name,
                Status = categoryUpdateDto.Status,
                UpdateUserId = categoryUpdateDto.UpdateUserId,
                UpdateDate = DateTime.Now,
            };
            _categoryDal.Update(category);

            return new SuccessResult(Messages.CategoryUpdated);
        }

        [SecuredOperation("category_list")]
        public IResult Delete(int id)
        {
            _categoryDal.Delete(id);
            return new SuccessResult(Messages.CategoryDeleted);
        }

        [SecuredOperation("category_list")]
        public IDataResult<List<ViewModel>> List()
        {
            var result = _categoryDal.List();

            return new SuccessDataResult<List<ViewModel>>(result, Messages.CategoriesListed);
        }

        [SecuredOperation("category_list")]
        public IDataResult<List<ViewModel>> ListByActive()
        {
            var result = _categoryDal.ListByActive();

            return new SuccessDataResult<List<ViewModel>>(result, Messages.CategoriesListed);
        }

        [SecuredOperation("category_list")]
        public IDataResult<ViewModel> ListById(int id)
        {
            var result = _categoryDal.ListById(id);
            if (result == null)
            {
                return new ErrorDataResult<ViewModel>(Messages.CategoryNotFound);
            }

            return new SuccessDataResult<ViewModel>(result, Messages.CategoryInfoListed);
        }
    }
}
