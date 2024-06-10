using FamTec.Server.Services.Admin.Account;
using FamTec.Server.Services.User;
using FamTec.Server.Tokens;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO;
using FamTec.Shared.Server.DTO.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamTec.Server.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IAdminAccountService AdminAccountService;
        private IUserService UserService;
        private ITokenComm TokenComm;

        public LoginController(IAdminAccountService _adminaccountservice,
            IUserService _userservice,
            ITokenComm _tokencomm)
        {
            this.AdminAccountService = _adminaccountservice;
            this.UserService = _userservice;
            this.TokenComm = _tokencomm;
        }

        [HttpPost]
        [Route("AdminSettingLogin")]
        public async ValueTask<IActionResult> AdminSettingLogin([FromBody] LoginDTO? dto)
        {
            if (dto is not null)
            {
                ResponseUnit<string>? model = await AdminAccountService.AdminLoginService(dto);

                if (model is not null)
                {
                    if(model.code is 200)
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
            else
            {
                return BadRequest();
            }
        }


        [Authorize(Roles = "SystemManager,Master,Manager")]
        [HttpGet]
        [Route("SystemManager")]
        [Route("sign/SystemManager")]
        public async ValueTask<IActionResult> SystemManager()
        {
            string token = HttpContext.Items["Token"]!.ToString()!;
            AdminSettingModel? model = TokenComm.TokenConvert(token);
            HttpContext.Items.Clear();


            return Ok(model);
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
