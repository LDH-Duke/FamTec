using FamTec.Server.Repository.User;
using FamTec.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamTec.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserInfoRepository UserInfoRepository;

        public UsersController(IUserInfoRepository _userinforepository)
        {
            UserInfoRepository = _userinforepository;
        }

        [HttpGet]
        [Route("SelectUserId/{userid}")]
        public async ValueTask<UsersTb> GetUserModel(string userid)
        {
            UsersTb? model = await UserInfoRepository.GetByUserId(userid);
            return model;
        }

    }
}
