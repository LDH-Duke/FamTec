using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace FamTec.Server.Services.User
{
    public interface IUserServices
    {
        /// <summary>
        /// 사용자 전체조회
        /// </summary>
        /// <returns></returns>
        public ValueTask<ResponseObject<UsersDTO>> GetAllUserListService();

        /// <summary>
        /// 해당 USER 정보 조회
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseObject<UsersDTO>> GetUserService(string userid);

        /// <summary>
        /// 사용자 추가
        /// </summary>
        /// <param name="userdto"></param>
        /// <returns></returns>
        public ValueTask<ResponseObject<UsersDTO>> AddUserService(UsersDTO dto);

        /// <summary>
        /// 사용자 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseObject<UsersDTO>> UpdateUserService(UsersDTO dto);

        /// <summary>
        /// 사용자 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseObject<UsersDTO>> DeleteUserService(UsersDTO dto);
    }
}


