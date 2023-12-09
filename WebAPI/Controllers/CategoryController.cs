using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add(CategoryAddDto categoryAddDto)
        {
            var result = _categoryService.Add(categoryAddDto);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("Update")]
        [HttpPut]
        public ActionResult Update(CategoryUpdateDto categoryUpdateDto)
        {
            var listById = _categoryService.ListById(categoryUpdateDto.Id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _categoryService.Update(categoryUpdateDto);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("List")]
        [HttpGet]
        public ActionResult List()
        {
            var list = _categoryService.List();
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }

        [Route("Delete")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var listById = _categoryService.ListById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _categoryService.Delete(id);

            if (result.Status)
            {
                return Ok(result);
            }

            return Ok(result);
        }

        [Route("ListById")]
        [HttpGet]
        public ActionResult ListById(int id)
        {
            var listById = _categoryService.ListById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            return Ok(listById);
        }
    }
}
