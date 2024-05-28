using FamTec.Server.Services.Admin.Department;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamTec.Server.Controllers.Admin.Department
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private SessionInfo session; // 테스트 세션모드

        private IDepartmentService DepartmentService;

        public DepartmentController(IDepartmentService _departmentservice)
        {
            this.DepartmentService = _departmentservice;

            session = new SessionInfo();
        }

        // 부서추가
        [HttpPost]
        [Route("AddDepartment")]
        public async ValueTask<IActionResult> AddDepartment([FromBody] DepartmentDTO dto)
        {
            ResponseModel<DepartmentDTO>? model = await DepartmentService.AddDepartmentService(dto, session);
            return Ok(model);
        }

        // 부서 전체조회
        [HttpGet]
        [Route("GetDepartmentList")]
        public async ValueTask<IActionResult> GetAllDepartment()
        {
            ResponseModel<DepartmentDTO>? model = await DepartmentService.GetAllDepartmentService();
            return Ok(model);
        }

        // 부서삭제
        [HttpPost]
        [Route("DeleteDepartmentList")]
        public async ValueTask<IActionResult> DeleteDepartmentList([FromBody]List<int> selList)
        {
            ResponseModel<string>? model = await DepartmentService.DeleteDepartmentService(selList, session);
            return Ok(model);
        }

        // 부서수정
        [HttpPost]
        [Route("UpdateDepartment")]
        public async ValueTask<IActionResult> UpdateDepartment([FromBody]DepartmentDTO dto)
        {
            ResponseModel<DepartmentDTO>? model = await DepartmentService.UpdateDepartmentService(dto, session);
            return Ok(model);
        }


    }
}
