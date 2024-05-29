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
        [Route("DetailWorks/{placeid?}")]
        public async ValueTask<IActionResult> DetailWorks(int placeid)
        {
            ResponseModel<AddPlaceDTO>? model = await AdminPlaceService.GetPlaceService(placeid);
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
            /*
            AddPlaceDTO dto = new AddPlaceDTO();
            dto.PlaceCd = "CD00004";
            dto.ContractNum = "CN00000004";
            dto.Name = "울산사업장";
            dto.Note = "비고값";
            dto.Address = "울산광역시 동구";
            dto.ContractDT = DateTime.Now;
            dto.PermMachine = 0;
            dto.PermLift = 0;
            dto.PermFire = 0;
            dto.PermConstruct = 0;
            dto.PermNetwork = 0;
            dto.PermBeauty = 0;
            dto.PermSecurity = 0;
            dto.PermMaterial = 0;
            dto.PermEnergy = 0;
            dto.Status = 1;

            dto.AdminList.Add(new AddManagerDTO
            {
                UserId = 3,
                UserName = "Admin",
                AdminID = 2,
                Type = "시스템관리자",
                DepartmentIdx = 3,
                DepartmentName = "에스텍시스템"
            });

            dto.AdminList.Add(new AddManagerDTO
            {
                UserId = 4,
                UserName = "TESTUSER",
                AdminID = 3,
                Type = "마스터",
                DepartmentIdx = 4,
                DepartmentName = "융합1팀"
            });
            */
            //ResponseModel<string>? model = await AdminPlaceService.AddPlaceService(dto, session);
            //return Ok(model);
            return Ok();
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
