using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO;
using FamTec.Shared.Server.DTO.Login;
using FamTec.Shared.Server.DTO.User;

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

    }
}
