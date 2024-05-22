using FamTec.Server.Repository.User;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop.Infrastructure;

namespace FamTec.Server.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserInfoRepository UserInfoRepository;

        ResponseOBJ<UsersDTO> Response;
        Func<string, UsersDTO, int, ResponseModel<UsersDTO>> FuncResponseOBJ;
        Func<string, List<UsersDTO>, int, ResponseModel<UsersDTO>> FuncResponseList;


        public UserService(IUserInfoRepository _userinforepository)
        {
            this.UserInfoRepository = _userinforepository;

            Response = new ResponseOBJ<UsersDTO>();
            FuncResponseOBJ = Response.RESPMessage;
            FuncResponseList = Response.RESPMessageList;
        }


     

    
    }
}
