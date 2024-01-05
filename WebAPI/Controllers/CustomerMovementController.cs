using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerMovementController : Controller
    {
        private ICustomerMovementService _customerMovementService;

        public CustomerMovementController(ICustomerMovementService customerMovementService)
        {
            _customerMovementService = customerMovementService;
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add(CustomerMovementAddDto customerMovementAddDto)
        {
            var result = _customerMovementService.Add(customerMovementAddDto);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("Update")]
        [HttpPut]
        public ActionResult Update(CustomerMovementUpdateDto customerMovementUpdateDto)
        {
            var listById = _customerMovementService.ListById(customerMovementUpdateDto.Id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _customerMovementService.Update(customerMovementUpdateDto);

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
            var listById = _customerMovementService.ListById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _customerMovementService.Delete(id);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("ListByCustomerId")]
        [HttpGet]
        public ActionResult ListByCustomerId(int id)
        {
            var listById = _customerMovementService.ListByCustomerId(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            return Ok(listById);
        }

        [Route("ListById")]
        [HttpGet]
        public ActionResult ListById(int id)
        {
            var listById = _customerMovementService.ListById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            return Ok(listById);
        }
    }
}
