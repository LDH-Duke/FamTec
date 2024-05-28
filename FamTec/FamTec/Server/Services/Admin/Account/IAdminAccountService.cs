using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO.Admin;
using FamTec.Shared.Server.DTO.Login;

namespace FamTec.Server.Services.Admin.Account
{
    public interface IAdminAccountService
    {
        /// <summary>
        /// 관리자 설정화면 서비스
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AccountDTO>> AdminLoginService(LoginDTO? dto);

        /// <summary>
        /// 관리자 아이디 생성 서비스
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<AccountDTO>> AdminRegisterService(AccountDTO? dto, SessionInfo session);

    }
}
