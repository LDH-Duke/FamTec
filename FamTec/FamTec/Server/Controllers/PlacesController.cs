using FamTec.Server.Repository.Place;
using FamTec.Server.Services.Place;
using FamTec.Shared.Client.DTO;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamTec.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly IPlaceServices PlaceServices;

        public PlacesController(IPlaceServices _placeservices)
        {

            this.PlaceServices = _placeservices;
        }

        [HttpPost]
        [Route("InsertPlace")]
        public async ValueTask<IActionResult> AddPlace([FromBody]PlacesDTO dto)
        {
            ResponseModel<PlacesDTO>? model = await PlaceServices.AddPlaceService(dto);
            return Ok(dto);
        }


        [HttpGet]
        [Route("SelectAllPlace")]
        public async ValueTask<IActionResult> GetAllPlace()
        {
            ResponseModel<PlacesDTO>? model = await PlaceServices.GetAllUserListService();
            return Ok(model);
        }


        [HttpPost]
        [Route("UpdatePlace")]
        public async ValueTask<IActionResult> UpdatePlace([FromBody]PlacesDTO dto)
        {
            ResponseModel<PlacesDTO>? model = await PlaceServices.UpdatePlaceService(dto);
            return Ok(model);
        }


        [HttpPost]
        [Route("DeletePlace")]
        public async ValueTask<IActionResult> DeletePlace([FromBody]PlacesDTO dto)
        {
            ResponseModel<PlacesDTO>? model = await PlaceServices.DeletePlaceService(dto);
            return Ok(model);
        }


    }
}
