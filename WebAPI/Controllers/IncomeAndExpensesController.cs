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
    }
}
