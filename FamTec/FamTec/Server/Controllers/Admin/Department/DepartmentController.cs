using FamTec.Server.Services.Admin.Department;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO;
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

        /// <summary>
        /// 부서추가 [수정완료]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddDepartment")]
        public async ValueTask<IActionResult> AddDepartment([FromBody] AddDepartmentDTO dto)
        {
            ResponseUnit<AddDepartmentDTO>? model = await DepartmentService.AddDepartmentService(dto);

            if(model is not null)
            {
                if (model.code == 200)
                {
                    return Ok(model);
                }
                else
                {
                    return BadRequest(model);
                }
            }
            else
            {
                return BadRequest(model);
            }
        }

        /// <summary>
        /// 부서 전체조회 [수정완료]
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDepartmentList")]
        public async ValueTask<IActionResult> GetAllDepartment()
        {
            ResponseList<DepartmentDTO>? model = await DepartmentService.GetAllDepartmentService();
            if(model is not null)
            {
                if (model.code == 200)
                {
                    return Ok(model);
                }
                else
                {
                    return BadRequest(model);
                }
            }
            else
            {
                return BadRequest(model);
            }
        }

        /// <summary>
        /// 부서삭제 [수정완료]
        /// </summary>
        /// <param name="selList"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("DeleteDepartmentList")]
        public async ValueTask<IActionResult> DeleteDepartmentList([FromBody]List<int?> selList)
        {
            ResponseUnit<bool>? model = await DepartmentService.DeleteDepartmentService(selList);

            if(model is not null)
            {
                if (model.code == 200)
                    return Ok(model);
                else
                    return BadRequest(model);
            }
            else
            {
                return BadRequest(model);
            }
        }

        /// <summary>
        /// 부서수정 * 확인함
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateDepartment")]
        public async ValueTask<IActionResult> UpdateDepartment([FromBody]DepartmentDTO dto)
        {
            ResponseModel<DepartmentDTO>? model = await DepartmentService.UpdateDepartmentService(dto, session);
            return Ok(model);
        }


    }
}
