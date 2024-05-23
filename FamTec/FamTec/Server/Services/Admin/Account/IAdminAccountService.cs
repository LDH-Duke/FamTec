using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO.Admin;
using FamTec.Shared.Server.DTO.Login;

namespace FamTec.Server.Services.Admin.Account
{
    public interface IAdminAccountService
    {
        // 관리자 로그인 서비스
        public ValueTask<ResponseModel<AccountDTO>> AdminLoginService(LoginDTO? dto);

        // 관리자 회원가입 서비스
        public ValueTask<ResponseModel<AccountDTO>> AdminRegisterService(AccountDTO? dto, SessionInfo session);

    }
}
