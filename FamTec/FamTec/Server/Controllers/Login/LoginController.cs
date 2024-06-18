using FamTec.Server.Services.Admin.Account;
using FamTec.Server.Services.User;
using FamTec.Server.Tokens;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO;
using FamTec.Shared.Server.DTO.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

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

        [AllowAnonymous]
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
                return StatusCode(404);
            }
        }


        [Authorize(Roles = "SystemManager,Master,Manager")]
        [HttpGet]
        [Route("SystemManager")]
        [Route("sign/SystemManager")]
        public async ValueTask<IActionResult> SystemManager()
        {
            JObject? model = TokenComm.TokenConvert(HttpContext.Request);
            return Ok(model);
        }

        [HttpPost]
        [Route("UserLogin")]
        public async ValueTask<IActionResult> UserLogin([FromBody] LoginDTO? dto)
        {
            try
            {
                if (dto is not null)
                {
                    ResponseUnit<string>? model = await UserService.UserLoginService(dto);

                    if (model is not null)
                    {
                        if (model.code == 200)
                        {
                            return Ok(model);
                        }
                        else
                        {
                            return Ok(model);
                        }
                    }
                    else
                    {
                        return Ok(model);
                    }
                }
                else
                {
                    return StatusCode(404);
                }
            }catch(Exception ex)
            {
                return StatusCode(500);
            }
        }
        

    }
}
