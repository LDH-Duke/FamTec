using FamTec.Server.Services.Admin.Account;
using FamTec.Shared.DTO;
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

        public LoginController(IAdminAccountService _adminaccountservice)
        {
            this.AdminAccountService = _adminaccountservice;
        }

        [HttpPost]
        [Route("AdminSettingLogin")]
        public async ValueTask<IActionResult> AdminSettingLogin([FromBody] LoginDTO dto)
        {
            ResponseModel<string>? model = await AdminAccountService.AdminLoginService(dto);
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

            return Ok(HttpContext.Items["Token"].ToString());
        }
        

    }
}
