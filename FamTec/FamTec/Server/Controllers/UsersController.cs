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
                ResponseObject<UsersDTO>? model = await UserServices.GetUserService(userid);
                if(model is not null)
                {
                    model.Message = "정상처리";
                    return Ok(model);
                }
                else
                {
                    return Ok(model);
                }
                
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
                ResponseObject<UsersDTO>? model = await UserServices.GetAllUserListService();
                switch(model.StatusCode)
                {
                    case 200:
                        model.Message = "정상처리";
                        return Ok(model);
                        
                    case 500:
                        return BadRequest();
                    
                    default:
                        return StatusCode(501, model);
                }
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
            ResponseObject<UsersDTO>? model = await UserServices.AddUserService(dto);
            return Ok(model);
        }


        [HttpPost]
        [Route("UpdateUser")]
        public async ValueTask<IActionResult> UpdateUser([FromBody]UsersDTO dto)
        {
           

            ResponseObject<UsersDTO>? model = await UserServices.UpdateUserService(dto);
            return Ok(model);
        }

        [HttpPost]
        [Route("DeleteUser")]
        public async ValueTask<IActionResult> DeleteUser([FromBody]UsersDTO dto)
        {
            ResponseObject<UsersDTO>? model = await UserServices.DeleteUserService(dto);
            return Ok(model);
        }
        

    }
}
