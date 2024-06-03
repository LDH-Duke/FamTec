using FamTec.Server.Databases;
using FamTec.Server.Services.Admin.Account;
using FamTec.Server.Services.Admin.Department;
using FamTec.Server.Services.Admin.Place;
using FamTec.Server.Services.User;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO.Admin;
using FamTec.Shared.Server.DTO.Admin.Place;
using FamTec.Shared.Server.DTO.Login;
using FamTec.Shared.Server.DTO.Place;
using FamTec.Shared.Server.DTO.User;
using Microsoft.AspNetCore.Mvc;

/* 테스트 컨트롤러 -- 나중에 분리시켜야함. */
namespace FamTec.Server.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private SessionInfo session; // 테스트 세션모델

        private IAdminAccountService AdminService;
        private IUserService UserService;

        public AdminUserController(IAdminAccountService _adminservice, IUserService _userservice)
        {
            AdminService = _adminservice;


            UserService = _userservice;

            session = new SessionInfo();
        }


        /// <summary>
        /// 로그인 * (확인함)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async ValueTask<IActionResult> Login([FromBody] LoginDTO dto)
        {
            ResponseModel<string>? model = await AdminService.AdminLoginService(dto);
            return Ok(model);
        }

      
        [HttpPost]
        [Route("AddManager")]
        public async ValueTask<IActionResult> AddManager([FromBody] AddManagerDTO dto)
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
            ResponseModel<AddManagerDTO>? model = await AdminService.AdminRegisterService(dto, session);
            return Ok(model);
        }

        /* 사용자 검색 */
        [HttpPost]
        [Route("UserSearch")]
        public async ValueTask<IActionResult> UserSearch([FromBody] UsersDTO dto)
        {
            ResponseModel<UsersDTO>? model = await UserService.UserIdCheck(dto.USERID);
            return Ok(model);
        }


        // ===========================


    







    }
}
