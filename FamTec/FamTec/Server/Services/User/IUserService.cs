using FamTec.Shared.Client.DTO.Normal.Users;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO;
using FamTec.Shared.Server.DTO.Login;
using FamTec.Shared.Server.DTO.User;
using Newtonsoft.Json.Linq;

namespace FamTec.Server.Services.User
{
    public interface IUserService
    {
        // 아이디 중복검사 서비스
        public ValueTask<ResponseUnit<UsersDTO>?> UserIdCheck(string? userid);

        /// <summary>
        /// 일반페이지 로그인 서비스
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseUnit<string>?> UserLoginService(LoginDTO? dto);


        public ValueTask<ResponseUnit<string>?> LoginSelectPlaceService(HttpContext context, int? placeid);

        /// <summary>
        /// 해당사업장의 USERLIST 출력
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ValueTask<ResponseList<ListUser>> GetPlaceUserList(JObject? obj, int placeid);

    }
}
