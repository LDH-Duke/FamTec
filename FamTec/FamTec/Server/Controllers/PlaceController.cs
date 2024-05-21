using FamTec.Server.Repository.User;
using FamTec.Server.Services.Place;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace FamTec.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private IPlaceService PlaceService;

        public PlaceController(IPlaceService _placeservice)
        {
            this.PlaceService = _placeservice;
        }

        [HttpGet]
        [Route("GetAllPlace")]
        public async ValueTask<IActionResult> GetAllPlace()
        {
            ResponseModel<PlacesDTO>? model = await PlaceService.GetAllPlaceService();
            return Ok(model);
        }

        [HttpPost]
        [Route("AddPlace")]
        public async ValueTask<IActionResult> AddPlace([FromBody]PlacesDTO dto)
        {
            ResponseModel<PlacesDTO>? model = await PlaceService.AddPlaceService(dto);
            return Ok(model);
        }

        [HttpGet]
        [Route("GetPlace/{id?}")]
        public async ValueTask<IActionResult> GetPlaceId(int id)
        {
            ResponseModel<PlacesDTO>? model = await PlaceService.GetByPlaceService(id);
            return Ok(model);
        }

        [HttpGet]
        [Route("GetPlaceCD/{code?}")]
        public async ValueTask<IActionResult> GetPlaceCode(string code)
        {
            ResponseModel<PlacesDTO>? model = await PlaceService.GetByPlaceService(code);
            return Ok(model);
        }


    }
}
