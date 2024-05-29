using Microsoft.AspNetCore.Mvc;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO.User;
using FamTec.Server.Services.User;
using FamTec.Shared;

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

      


    }
}
