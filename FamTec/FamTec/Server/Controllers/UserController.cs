using FamTec.Server.Services.User;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamTec.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService UserService;

        public UserController(IUserService _userservice)
        {
            this.UserService = _userservice;
        }

        [HttpGet]
        [Route("GetAllUser")]
        public async ValueTask<IActionResult> GetAllUser()
        {
            ResponseModel<UsersDTO>? model = await UserService.GetAllUserService();
            return Ok(model);
        }

        /* 57P - 사업장별 전체 조회 */
        [HttpGet]
        [Route("GetAllPlaceUser/{placeid?}")]
        public async ValueTask<IActionResult> GetPlaceUser(int placeid)
        {
            ResponseModel<UsersDTO>? model = await UserService.GetAllPlaceUser(placeid);
            return Ok(model);
        }

        /* 사업장별 전체에서 ROW 클릭했을때 */
        [HttpGet]
        [Route("GetUserSearch/{idx?}")]
        public async ValueTask<IActionResult> GetUserSearch(int idx)
        {
            ResponseModel<UsersDTO>? model = await UserService.GetUserService(idx);
            return Ok(model);
        }

        /*
        [HttpGet]
        [Route("GetUser/{userid?}/{placeid?}")]
        public async ValueTask<IActionResult> GetUserInfo(string userid, int placeid)
        {
            ResponseModel<UsersDTO>? model = await UserService.GetUserService(userid, placeid);
            return Ok(model);
        }
        */
        [HttpPost]
        [Route("AddUser")]
        public async ValueTask<IActionResult> AddUser([FromBody]UsersDTO dto)
        {
            ResponseModel<UsersDTO>? model = await UserService.AddUserService(dto);
            return Ok(model);
        }

        [HttpPost]
        [Route("EditUser")]
        public async ValueTask<IActionResult> EditUser([FromBody]UsersDTO dto)
        {
            ResponseModel<UsersDTO>? model = await UserService.EditUserService(dto);
            return Ok(model);
        }

        [HttpPost]
        [Route("DeleteUser")]
        public async ValueTask<IActionResult> DeleteUser([FromBody]UsersDTO dto)
        {
            ResponseModel<UsersDTO>? model = await UserService.DeleteUserService(dto);
            return Ok(model);
        }


    }
}
