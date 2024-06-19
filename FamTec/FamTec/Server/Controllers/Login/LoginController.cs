using FamTec.Client.Pages.Normal.Voc.VocDetail;
using FamTec.Server.Services.Admin.Account;
using FamTec.Server.Services.Admin.Place;
using FamTec.Server.Services.User;
using FamTec.Server.Tokens;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO;
using FamTec.Shared.Server.DTO.Admin;
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
        private IAdminPlaceService AdminPlaceService;

        private IUserService UserService;

        public LoginController(IAdminAccountService _adminaccountservice,
            IAdminPlaceService _adminplaceservice,
            IUserService _userservice)
        {
            this.AdminAccountService = _adminaccountservice;
            this.AdminPlaceService = _adminplaceservice;
            this.UserService = _userservice;
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

        /// <summary>
        /// 로그인 API - 모든사람 접근가능
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async ValueTask<IActionResult> Login([FromBody] LoginDTO? dto)
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
                            return Ok(model); // 유저
                        }
                        else if (model.code == 201)
                        {
                            return Ok(model); // 관리자
                        }
                        else
                        {
                            return BadRequest();
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
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// 관리자 들만 접근가능 할당된 사업장 LIST 반환
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "SystemManager,Master,Manager")]
        [HttpGet]
        [Route("sign/AdminPlaceList")]
        public async ValueTask<IActionResult> AdminPlaceList()
        {
            int? adminidx = Int32.Parse(HttpContext.Items["AdminIdx"]!.ToString()!);
            
            if (adminidx is not null)
            {
                if(adminidx is not null)
                {
                    ResponseList<AdminPlaceDTO> model = await AdminPlaceService.GetMyWorksService(adminidx);
                    if(model is not null)
                    {
                        if(model.code == 200)
                        {
                            return Ok(model);
                        }
                        else if(model.code == 204)
                        {
                            return Ok(model);
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// 관리자들만 접근가능
        /// </summary>
        /// <param name="placeid"></param>
        /// <returns></returns>
        [Authorize(Roles = "SystemManager,Master,Manager")]
        [HttpGet]
        [Route("sign/SelectPlace")]
        public async ValueTask<IActionResult> SelectPlace([FromQuery]int placeid)
        {
            ResponseUnit<string>? model = await UserService.LoginSelectPlaceService(HttpContext, placeid);

            if(model is not null)
            {
                if(model.code == 200)
                {
                    return Ok(model);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

     

    }
}
