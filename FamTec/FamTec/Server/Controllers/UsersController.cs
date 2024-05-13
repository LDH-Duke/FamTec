using FamTec.Server.Repository.User;
using FamTec.Server.Services.User;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


        [HttpGet]
        [Route("SelectUser/{userid}")]
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


        [HttpGet]
        [Route("test")]
        public async ValueTask<IActionResult> Test()
        {
            UsersDTO temp = new UsersDTO();
            temp.USERID = "eeee";
            temp.PASSWORD = "123123";
            temp.NAME = "테스트";
            temp.PERM_BUILDING = 2;
            temp.PERM_EQUIPMENT = 2;
            temp.PERM_MATERIAL = 2;
            temp.PERM_ENERGY = 2;
            temp.PERM_OFFICE = 2;
            temp.PERM_COMP = 2;
            temp.PERM_CONST = 2;
            temp.PERM_CLAIM = 2;
            temp.PERM_SYS = 2;
            temp.PERM_EMPLOYEE = 2;
            temp.PERM_LAW_CK = 2;
            temp.PERM_LAW_EDU = 2;
            temp.ADMIN_YN = false;
            temp.ALARM_YN = false;
            temp.STATUS = true;


            string userid = "admin";

            var model = await UserServices.AddUserService(temp,userid);


            return Ok();
        }



        [HttpPost]
        [Route("InsertUser")]
        public async ValueTask<IActionResult> PostUser([FromBody]UsersDTO model)
        {
            string userid = "System";


            if (model == null)
                return BadRequest();
            

            
            return Ok();
        }

    }
}
