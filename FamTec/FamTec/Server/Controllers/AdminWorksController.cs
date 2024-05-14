using FamTec.Client.Pages.Place;
using FamTec.Server.Services.Admin.Place;
using FamTec.Shared.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamTec.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminWorksController : ControllerBase
    {
        private readonly IAdminPlaceService AdminPlaceService;

        public AdminWorksController(IAdminPlaceService _adminplaceservice)
        {
            this.AdminPlaceService = _adminplaceservice;
        }

        [HttpGet]
        [Route("GetAllWorks")]
        public async ValueTask<IActionResult> GetAllWorks()
        {
            ResponseModel<AdminPlacesDTO> model = await AdminPlaceService.GetAllWorks();
            return Ok(model);
        }

        [HttpGet]
        [Route("GetUserIDWorks/{userid}")]
        public async ValueTask<IActionResult> GetUserIDWorks(string userid)
        {
            ResponseModel<AdminPlacesDTO> model = await AdminPlaceService.GetUserIDWorksList(userid);
            return Ok(model);
        }

        [HttpGet]
        [Route("GetPlaceCDWorks/{placecd}")]
        public async ValueTask<IActionResult> GetPlaceCDWorks(string placecd)
        {
            ResponseModel<AdminPlacesDTO> model = await AdminPlaceService.GetPlaceCDWorksList(placecd);
            return Ok(model);
        }

        [HttpPost]
        [Route("AddAdminWorks")]
        public async ValueTask<IActionResult> AddAdminWorks([FromBody] AdminPlacesDTO dto)
        {
            ResponseModel<AdminPlacesDTO> model = await AdminPlaceService.AddAdminWorksInfo(dto);
            return Ok(model);
        }


        [HttpPost]
        [Route("UpdateAdminWorks")]
        public async ValueTask<IActionResult> UpdateAdminWorks([FromBody] AdminPlacesDTO beforedto, AdminPlacesDTO afterdto)
        {
            ResponseModel<AdminPlacesDTO> model = await AdminPlaceService.UpdateAdminWorks(beforedto, afterdto);
            return Ok(model);
        }

        [HttpPost]
        [Route("DeleteAdminWorks")]
        public async ValueTask<IActionResult> DeleteAdminWorks([FromBody]AdminPlacesDTO dto)
        {

            ResponseModel<AdminPlacesDTO> model = await AdminPlaceService.DeleteAdminWorks(dto);
            return Ok(model);
        }

    }
}
