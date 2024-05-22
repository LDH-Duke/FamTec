﻿using FamTec.Server.Services.Admin.Account;
using FamTec.Server.Services.Admin.Department;
using FamTec.Server.Services.Admin.Place;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO.Admin;
using FamTec.Shared.Server.DTO.Login;
using FamTec.Shared.Server.DTO.Place;
using Microsoft.AspNetCore.Mvc;

/* 테스트 컨트롤러 -- 나중에 분리시켜야함. */
namespace FamTec.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private SessionInfo session; // 테스트 세션모델

        private IAdminAccountService AdminService;
        private IAdminPlaceService AdminPlaceService;
        private IDepartmentService DepartmentService;

        public AdminUserController(IAdminAccountService _adminservice, IAdminPlaceService _adminplaceservice, IDepartmentService _departmentservice)
        {
            this.AdminService = _adminservice;

            this.AdminPlaceService = _adminplaceservice;

            this.DepartmentService = _departmentservice;

            this.session = new SessionInfo();
        }


        // 로그인
        [HttpPost]
        [Route("Login")]
        public async ValueTask<IActionResult> Login([FromBody]LoginDTO dto)
        {

            ResponseModel<AccountDTO>? model = await AdminService.AdminLoginService(dto);
            return Ok(model);
        }

        // 부서추가
        [HttpPost]
        [Route("AddDepartment")]
        public async ValueTask<IActionResult> AddDepartment([FromBody]DepartmentDTO dto)
        {
            ResponseModel<DepartmentDTO>? model = await DepartmentService.AddDepartmentService(dto, session);
            return Ok(model);
        }

        [HttpPost]
        [Route("AddManager")]
        public async ValueTask<IActionResult> AddManager([FromBody]AccountDTO dto)
        {
            /*
            AccountDTO dto = new AccountDTO()
            {
                USERID = "TESTUSER4",
                NAME = "테스트관리자4",
                PASSWORD = "123451",
                EMAIL = "test@test.com",
                PHONE = "010-0000-0000",
                DEPARTMENT_INDEX = 4,
            };

            dto.placeDTO.Add(new PlacesDTO
            {
                PlaceIndex = 4
            });
            dto.placeDTO.Add(new PlacesDTO
            {
                PlaceIndex = 5
            });
            */
            ResponseModel<AccountDTO>? model = await AdminService.AdminRegisterService(dto, session);
            return Ok(model);
        }

        // 사업장 생성 완성 -- 수정해야함.
        [HttpPost]
        [Route("AddPlace")]
        public async ValueTask<IActionResult> AddPlace([FromBody]PlacesDTO dto)
        {
            ResponseModel<PlacesDTO>? model = await AdminPlaceService.AddPlaceService(dto, session);
            return Ok(model);
        }

        


    }
}
