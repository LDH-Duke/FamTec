using FamTec.Server.Services.Admin.User;
using FamTec.Shared.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamTec.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUsersController : ControllerBase
    {
        private readonly IAdminUserService AdminUserSerivce;

        public AdminUsersController(IAdminUserService _adminuserservice)
        {
            this.AdminUserSerivce = _adminuserservice;
        }

        [HttpGet]
        [Route("SelectAllAdmin")]
        public async ValueTask<IActionResult> GetAllAdminList()
        {
            ResponseModel<AdminsDTO>? model = await AdminUserSerivce.GetAllAdminList();
            return Ok(model);
        }

        [HttpGet]
        [Route("SelectAdmin/{adminid}")]
        public async ValueTask<IActionResult> GetAdmin(string adminid)
        {
            ResponseModel<AdminsDTO>? model = await AdminUserSerivce.GetAdminInfo(adminid);
            return Ok(model);
        }

        [HttpPost]
        [Route("AddAdmin")]
        public async ValueTask<IActionResult> AddAdmin([FromBody]AdminsDTO dto)
        {
            ResponseModel<AdminsDTO>? model = await AdminUserSerivce.AddAdminInfo(dto);
            return Ok(model);
        }

        [HttpPost]
        [Route("UpdateAdmin")]
        public async ValueTask<IActionResult> UpdateAdmin([FromBody]AdminsDTO dto)
        {
            ResponseModel<AdminsDTO>? model = await AdminUserSerivce.UpdateAdminInfo(dto);
            return Ok(model);
        }

        [HttpPost]
        [Route("DeleteAdmin")]
        public async ValueTask<IActionResult> DeleteAdmin([FromBody]AdminsDTO dto)
        {
            ResponseModel<AdminsDTO>? model = await AdminUserSerivce.DeleteAdminInfo(dto);
            return Ok(model);
        }

    }
}
