using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomePageController : Controller
    {
        private IHomePageService _homePageService;

        public HomePageController(IHomePageService homePageService)
        {
            _homePageService = homePageService;
        }

        [Route("GetStockQuantity")]
        [HttpGet]
        public ActionResult GetStockQuantity()
        {
            var list = _homePageService.GetStockQuantity();
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }

        [Route("GetStockValue")]
        [HttpGet]
        public ActionResult GetStockValue()
        {
            var list = _homePageService.GetStockValue();
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }

        [Route("GetSalesToday")]
        [HttpGet]
        public ActionResult GetSalesToday()
        {
            var list = _homePageService.GetSalesToday();
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }

        [Route("GetTotalUser")]
        [HttpGet]
        public ActionResult GetTotalUser()
        {
            var list = _homePageService.GetTotalUser();
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }

        [Route("GetTotalCustomer")]
        [HttpGet]
        public ActionResult GetTotalCustomer()
        {
            var list = _homePageService.GetTotalCustomer();
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }


        [Route("GetTotalWholeSaler")]
        [HttpGet]
        public ActionResult GetTotalWholeSaler()
        {
            var list = _homePageService.GetTotalWholeSaler();
            if (!list.Status)
            {
                return BadRequest(list);
            }

            return Ok(list);
        }
    }
}
