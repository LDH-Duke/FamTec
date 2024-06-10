using FamTec.Server.Services.Admin.Account;
using FamTec.Server.Services.User;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO;
using FamTec.Shared.Server.DTO.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FamTec.Server.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IAdminAccountService AdminAccountService;
        private IUserService UserService;

        public LoginController(IAdminAccountService _adminaccountservice, IUserService _userservice)
        {
            this.AdminAccountService = _adminaccountservice;
            this.UserService = _userservice;
        }

        [HttpPost]
        [Route("AdminSettingLogin")]
        public async ValueTask<IActionResult> AdminSettingLogin([FromBody] LoginDTO dto)
        {
            ResponseUnit<string>? model = await AdminAccountService.AdminLoginService(dto);
            return Ok(model);
            //AdminLoginService
        }


        [Authorize(Roles = "SystemManager")]
        [HttpGet]
        [Route("Test")]
        public async ValueTask<IActionResult> Test()
        {
            var temp = HttpContext.Items["Token"];
            HttpContext.Items.Clear();

            HttpContext.Session.SetString("Session", "123123");
            
            return Ok(temp);
        }

        [HttpPost]
        [Route("UserLogin")]
        public async ValueTask<IActionResult> UserLogin([FromBody] LoginDTO dto)
        {
            ResponseModel<string>? model = await UserService.UserLoginService(dto);
            return Ok(model);
        }
        

    }
}
