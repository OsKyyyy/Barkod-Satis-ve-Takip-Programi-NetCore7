using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WholeSalerMovementController : Controller
    {
        private IWholeSalerMovementService _wholeSalerMovementService;

        public WholeSalerMovementController(IWholeSalerMovementService wholeSalerMovementService)
        {
            _wholeSalerMovementService = wholeSalerMovementService;
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add(WholeSalerMovementAddDto wholeSalerMovementAddDto)
        {
            var result = _wholeSalerMovementService.Add(wholeSalerMovementAddDto);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("Update")]
        [HttpPut]
        public ActionResult Update(WholeSalerMovementUpdateDto wholeSalerMovementUpdateDto)
        {
            var listById = _wholeSalerMovementService.ListById(wholeSalerMovementUpdateDto.Id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _wholeSalerMovementService.Update(wholeSalerMovementUpdateDto);

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
            var listById = _wholeSalerMovementService.ListById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _wholeSalerMovementService.Delete(id);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("ListByWholeSalerId")]
        [HttpGet]
        public ActionResult ListByWholeSalerId(int id)
        {
            var listById = _wholeSalerMovementService.ListByWholeSalerId(id);
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
            var listById = _wholeSalerMovementService.ListById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            return Ok(listById);
        }
    }
}
