using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace FamTec.Server.Services.User
{
    public interface IUserServices
    {
        public ValueTask<ResponseObject<UsersDTO>> GetUserService(string userid);
        public ValueTask<ResponseObject<UsersDTO>> GetAllUserListService();
        public ValueTask<ResponseObject<UsersDTO>> AddUserService(UsersDTO model, string userid);

    }
}


