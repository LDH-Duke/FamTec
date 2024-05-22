using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO.Admin;
using FamTec.Shared.Server.DTO.Login;

namespace FamTec.Server.Services.Admin.Account
{
    public interface IAdminAccountService
    {
        public ValueTask<ResponseModel<AccountDTO>> AdminLoginService(LoginDTO? dto);

        public ValueTask<ResponseModel<AccountDTO>> AdminRegisterService(AccountDTO? dto, SessionInfo session);

    }
}
