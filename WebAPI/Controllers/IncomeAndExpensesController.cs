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

        [Route("Add")]
        [HttpPost]
        public ActionResult Add(IncomeAndExpensesAddDto incomeAndExpensesAddDto)
        {
            var result = _incomeAndExpensesService.Add(incomeAndExpensesAddDto);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("Update")]
        [HttpPut]
        public ActionResult Update(IncomeAndExpensesUpdateDto incomeAndExpensesUpdateDto)
        {
            var listById = _incomeAndExpensesService.ListById(incomeAndExpensesUpdateDto.Id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _incomeAndExpensesService.Update(incomeAndExpensesUpdateDto);

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
            var list = _incomeAndExpensesService.List();
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
            var listById = _incomeAndExpensesService.ListById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _incomeAndExpensesService.Delete(id);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("ListById")]
        [HttpGet]
        public ActionResult ListById(int id)
        {
            var listById = _incomeAndExpensesService.ListById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            return Ok(listById);
        }
    }
}
