using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WholeSalerController : Controller
    {
        private IWholeSalerService _wholeSalerService;

        public WholeSalerController(IWholeSalerService wholeSalerService)
        {
            _wholeSalerService = wholeSalerService;
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add(WholeSalerAddDto wholeSalerAddDto)
        {
            var result = _wholeSalerService.Add(wholeSalerAddDto);

            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Route("Update")]
        [HttpPut]
        public ActionResult Update(WholeSalerUpdateDto wholeSalerUpdateDto)
        {
            var listById = _wholeSalerService.ListById(wholeSalerUpdateDto.Id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _wholeSalerService.Update(wholeSalerUpdateDto);

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
            var list = _wholeSalerService.List();
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
            var listById = _wholeSalerService.ListById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            var result = _wholeSalerService.Delete(id);

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
            var listById = _wholeSalerService.ListById(id);
            if (!listById.Status)
            {
                return BadRequest(listById);
            }

            return Ok(listById);
        }
    }
}
