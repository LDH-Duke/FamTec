using FamTec.Shared.DTO;

namespace FamTec.Server.Services.Login
{
    public interface IAccountService
    {
        public ValueTask<ResponseModel<UsersDTO>> LoginService(string userid, string password, bool isadmin);
    }
}
