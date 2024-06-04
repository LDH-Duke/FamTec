using FamTec.Server.Services.Admin.Place;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO.Admin;
using FamTec.Shared.Server.DTO.Admin.Place;
using FamTec.Shared.Server.DTO.Place;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace FamTec.Server.Controllers.Admin.AdminPlaces
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminPlaceController : ControllerBase
    {
        private SessionInfo session; // 테스트 세션

        private IAdminPlaceService AdminPlaceService;


        public AdminPlaceController(IAdminPlaceService _adminplaceservice)
        {
            this.AdminPlaceService = _adminplaceservice;

            this.session = new SessionInfo();
        }
        
        /// <summary>
        /// 전체 사업장 리스트 조회 * (확인함)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllWorksList")]
        public async ValueTask<IActionResult> GetAllPlaceList()
        {
            ResponseModel<AllPlaceDTO>? model = await AdminPlaceService.GetAllWorksService();
            return Ok(model);
        }

        /// <summary>
        /// 매니저리스트 전체 반환 * (확인함)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllManagerList")]
        public async ValueTask<IActionResult> GetAllManagerList()
        {
            ResponseModel<ManagerListDTO> model = await AdminPlaceService.GetAllManagerListService();
            return Ok(model);
        }

        /// <summary>
        /// 선택된 매니저가 관리하는 사업장 LIST반환 * (확인함)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("MyWorks/{id?}")]
        public async ValueTask<IActionResult> GetMyWorks(int id)
        {
            ResponseModel<AdminPlaceDTO> model = await AdminPlaceService.GetMyWorksService(id);
            return Ok(model);
        }

        /// <summary>
        /// 해당사업장을 관리하는 관리자 LIST 반환 * (확인함)
        /// </summary>
        /// <param name="placeid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DetailWorks")]
        public async ValueTask<IActionResult> DetailWorks([FromQuery]int placeid)
        {
            ResponseModel<PlaceDetailDTO>? model = await AdminPlaceService.GetPlaceService(placeid);
            return Ok(model);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("UpdateWorks")]
        public async ValueTask<IActionResult> UpdateWorks()
        {
            AddPlaceDTO dto = new AddPlaceDTO();
            return Ok();
        }
        
        /// <summary>
        /// 사업장 생성시 관리자할당 * (확인함)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddWorks")]
        public async ValueTask<IActionResult> AddWorks([FromBody]AddPlaceDTO dto)
        {
            ResponseModel<int?> model = await AdminPlaceService.AddPlaceService(dto);
            return Ok(model);
        }

        [HttpPost]
        [Route("AddPlaceManager")]
        public async ValueTask<IActionResult> AddPlaceManager([FromBody]AddPlaceManagerDTO<ManagerListDTO> placemanager)
        {
            ResponseModel<string> model = await AdminPlaceService.AddPlaceManagerService(placemanager);
            return Ok(model);

        }

        /// <summary>
        /// 사업장 자체를 삭제 * 확인함
        /// </summary>
        /// <param name="placeidx"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteWorks")]
        public async ValueTask<IActionResult> DeleteWorks([FromBody]List<int> placeidx)
        {
            ResponseModel<string>? model = await AdminPlaceService.DeletePlaceService(placeidx, session);
            return Ok(model);
        }

    }
}
