using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO.User;

namespace FamTec.Server.Services.User
{
    public interface IUserService
    {
        // 아이디 중복검사 서비스
        public ValueTask<ResponseModel<UsersDTO>?> UserIdCheck(string? userid);

    }
}
