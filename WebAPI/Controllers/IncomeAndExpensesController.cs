using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeAndExpensesController : Controller
    {
        private IIncomeAndExpensesService _incomeAndExpensesService;

        public IncomeAndExpensesController(IIncomeAndExpensesService incomeAndExpensesService)
        {
            _incomeAndExpensesService = incomeAndExpensesService;
        }

        [Route("AddType")]
        [HttpPost]
        public ActionResult AddType(IncomeAndExpensesTypeAddDto incomeAndExpensesTypeAddDto)
        {
            var result = _incomeAndExpensesService.AddType(incomeAndExpensesTypeAddDto);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("UpdateType")]
        [HttpPut]
        public ActionResult UpdateType(IncomeAndExpensesTypeUpdateDto incomeAndExpensesTypeUpdateDto)
        {
            var listById = _incomeAndExpensesService.ListTypeById(incomeAndExpensesTypeUpdateDto.Id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _incomeAndExpensesService.UpdateType(incomeAndExpensesTypeUpdateDto);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("DeleteType")]
        [HttpDelete]
        public ActionResult DeleteType(int id)
        {
            var listById = _incomeAndExpensesService.ListTypeById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _incomeAndExpensesService.DeleteType(id);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("ListType")]
        [HttpGet]
        public ActionResult ListType()
        {
            var list = _incomeAndExpensesService.ListType();
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }

        [Route("ListTypeById")]
        [HttpGet]
        public ActionResult ListTypeById(int id)
        {
            var listById = _incomeAndExpensesService.ListTypeById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            return Ok(listById);
        }
    }
}
