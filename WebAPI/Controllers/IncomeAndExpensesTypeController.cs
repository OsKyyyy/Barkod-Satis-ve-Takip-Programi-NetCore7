using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeAndExpensesTypeController : Controller
    {
        private IIncomeAndExpensesTypeService _incomeAndExpensesTypeService;

        public IncomeAndExpensesTypeController(IIncomeAndExpensesTypeService incomeAndExpensesTypeService)
        {
            _incomeAndExpensesTypeService = incomeAndExpensesTypeService;
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add(IncomeAndExpensesTypeAddDto incomeAndExpensesTypeAddDto)
        {
            var result = _incomeAndExpensesTypeService.Add(incomeAndExpensesTypeAddDto);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("Update")]
        [HttpPut]
        public ActionResult Update(IncomeAndExpensesTypeUpdateDto incomeAndExpensesTypeUpdateDto)
        {
            var listById = _incomeAndExpensesTypeService.ListById(incomeAndExpensesTypeUpdateDto.Id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _incomeAndExpensesTypeService.Update(incomeAndExpensesTypeUpdateDto);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("Delete")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var listById = _incomeAndExpensesTypeService.ListById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _incomeAndExpensesTypeService.Delete(id);

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
            var list = _incomeAndExpensesTypeService.List();
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }

        [Route("ListById")]
        [HttpGet]
        public ActionResult ListById(int id)
        {
            var listById = _incomeAndExpensesTypeService.ListById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            return Ok(listById);
        }

        [Route("ListByActive")]
        [HttpGet]
        public ActionResult ListByActive()
        {
            var listById = _incomeAndExpensesTypeService.ListByActive();
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            return Ok(listById);
        }
    }
}
