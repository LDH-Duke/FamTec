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
        /// 전체 사업장 리스트 조회
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllWorksList")]
        public async ValueTask<IActionResult> GetAllPlaceList()
        {
            ResponseModel<PlacesDTO>? model = await AdminPlaceService.GetAllWorksService();
            return Ok(model);
        }

        /// <summary>
        /// 매니저리스트 전체 반환
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllManagerList")]
        public async ValueTask<IActionResult> GetAllManagerList()
        {
            ResponseModel<ManagerListDTO> model = await AdminPlaceService.GetAllManagerListService();
            return Ok(model);
        }


        [HttpGet]
        [Route("MyWorks")]
        public async ValueTask<IActionResult> GetMyWorks()
        {
            ResponseModel<AdminPlaceDTO> model = await AdminPlaceService.GetMyWorksService(7);
            return Ok(model);
        }

        [HttpGet]
        [Route("DetailWorks")]
        public async ValueTask<IActionResult> DetailWorks()
        {
            ResponseModel<AddPlaceDTO>? model = await AdminPlaceService.GetPlaceService(7);
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
            ResponseModel<string>? model = await AdminPlaceService.AddPlaceService(dto, session);
            return Ok(model);
        }

        [HttpPost]
        [Route("DeleteWorks")]
        public async ValueTask<IActionResult> DeleteWorks([FromBody]List<int> placeidx)
        {
            ResponseModel<string>? model = await AdminPlaceService.DeletePlaceService(placeidx, session);
            return Ok(model);
        }

    }
}
