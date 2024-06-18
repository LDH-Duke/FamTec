using Microsoft.AspNetCore.Mvc;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO.User;
using FamTec.Server.Services.User;
using FamTec.Shared;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using FamTec.Server.Tokens;
using Newtonsoft.Json.Linq;
using FamTec.Shared.Server.DTO;
using FamTec.Shared.Client.DTO.Normal.Users;

namespace FamTec.Server.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService UserService;
        private ITokenComm TokenComm;


        public UserController(IUserService _userservice, ITokenComm _tokencomm)
        {
            UserService = _userservice;
            TokenComm = _tokencomm;
        }

        [HttpGet]
        [Route("GetPlaceUsers")]
        public async ValueTask<IActionResult> GetUserList([FromQuery] int placeid)
        {
            JObject? jobj = TokenComm.TokenConvert(HttpContext.Request);
            ResponseList<ListUser> model = await UserService.GetPlaceUserList(jobj, placeid);

            if (model.code == 200)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest();
            }
        }





    }
}
