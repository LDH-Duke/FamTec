using FamTec.Server.Repository.User;
using FamTec.Server.Services.User;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Infrastructure;
using System.Diagnostics.Eventing.Reader;
using System.Net;

namespace FamTec.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices UserServices;

        public UsersController(IUserServices _userservices)
        {
            this.UserServices = _userservices;
        }

        //https://localhost:8888/api/Users/SelectUser/test
        [HttpGet]
        [Route("SelectUser/{userid?}")]
        public async ValueTask<IActionResult> GetUser(string userid)
        {
            try
            {
                ResponseModel<UsersDTO>? model = await UserServices.GetUserInfo(userid);
                return Ok(model);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("SelectAllUser")]
        public async ValueTask<IActionResult> GetAllUserList()
        {
            try
            {
                ResponseModel<UsersDTO>? model = await UserServices.GetAllUserList();
                return Ok(model);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        [Route("AddUser")]
        public async ValueTask<IActionResult> AddUser([FromBody]UsersDTO dto)
        {
            ResponseModel<UsersDTO>? model = await UserServices.AddUserInfo(dto);
            return Ok(model);
        }


        [HttpPost]
        [Route("UpdateUser")]
        public async ValueTask<IActionResult> UpdateUser([FromBody]UsersDTO dto)
        {
           

            ResponseModel<UsersDTO>? model = await UserServices.UpdateUserInfo(dto);
            return Ok(model);
        }

        [HttpPost]
        [Route("DeleteUser")]
        public async ValueTask<IActionResult> DeleteUser([FromBody]UsersDTO dto)
        {
            ResponseModel<UsersDTO>? model = await UserServices.DeleteUserInfo(dto);
            return Ok(model);
        }

    }
}
