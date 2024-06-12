﻿using FamTec.Server.Databases;
using FamTec.Server.Services.Admin.Account;
using FamTec.Server.Services.Admin.Department;
using FamTec.Server.Services.Admin.Place;
using FamTec.Server.Services.User;
using FamTec.Server.Tokens;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server;
using FamTec.Shared.Server.DTO;
using FamTec.Shared.Server.DTO.Admin;
using FamTec.Shared.Server.DTO.Admin.Place;
using FamTec.Shared.Server.DTO.Login;
using FamTec.Shared.Server.DTO.Place;
using FamTec.Shared.Server.DTO.User;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace FamTec.Server.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private IAdminAccountService AdminService;
        private IUserService UserService;
        private IAdminPlaceService AdminPlaceService;
        private ITokenComm TokenComm;

        public AdminUserController(IAdminAccountService _adminservice,
            IUserService _userservice,
            IAdminPlaceService _adminplaceservice,
            ITokenComm _tokencomm)
        {
            this.AdminService = _adminservice;
            this.UserService = _userservice;
            this.AdminPlaceService = _adminplaceservice;
            this.TokenComm = _tokencomm;
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
            ResponseUnit<string>? model = await AdminService.AdminLoginService(dto);
            if(model is not null)
            {
                if(model.code == 200)
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
                return BadRequest(model);
            }
        }

        /// <summary>
        /// 관리자추가 [수정완료]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddManager")]
        public async ValueTask<IActionResult> AddManager([FromBody] AddManagerDTO dto)
        {
            AdminSettingModel? token = TokenComm.TokenConvert(HttpContext.Request);

            if (token is not null)
            {
                ResponseUnit<AdminTb>? model = await AdminService.AdminRegisterService(dto, token);

                if (model is not null)
                {
                    if (model.code == 200)
                    {
                        return Ok(new ResponseUnit<int?> { message = "데이터가 정상 처리되었습니다.", data = model.data!.Id, code = 200 });
                    }
                    else
                    {
                        return BadRequest(new ResponseUnit<int?> { message = "데이터가 처리되지 않았습니다.", data = null, code = 404 });
                    }
                }
                else
                {
                    return BadRequest(new ResponseUnit<int?> { message = "데이터가 처리되지 않았습니다.", data = null, code = 404 });
                }
            }
            else
            {
                return StatusCode(404);
            }
        }

        /// <summary>
        /// 차후확인
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetManagerInfo")]
        public async ValueTask<IActionResult> GetManagerInfo([FromQuery]int adminId)
        {
            ResponseUnit<DManagerDTO>? model = await AdminService.DetailAdminService(adminId);

            if(model is not null)
            {
                if(model.code == 200)
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
        /// 관리자추가시 사업장등록 [수정완료]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddManagerWorks")]
        public async ValueTask<IActionResult> AddManagerWorks([FromBody] AddManagerPlaceDTO dto)
        {
            ResponseUnit<bool>? model = await AdminPlaceService.AddManagerPlaceSerivce(dto);

            if(model is not null)
            {
                if(model.code == 200)
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
        /// 관리자 삭제 - 유저테이블 - 사업장테이블 모두 삭제
        /// </summary>
        /// <param name="adminidx"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("DeleteManager")]
        public async ValueTask<IActionResult> DeleteManager([FromBody] List<int> adminidx)
        {
            ResponseUnit<int>? model = await AdminService.DeleteAdminService(adminidx);

            if(model is not null)
            {
                if(model.code == 200)
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


        /* 사용자 검색 */
        [HttpPost]
        [Route("UserSearch")]
        public async ValueTask<IActionResult> UserSearch([FromBody] UsersDTO dto)
        {
            ResponseUnit<UsersDTO>? model = await UserService.UserIdCheck(dto.USERID);
            if (model is not null)
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

    }
}
