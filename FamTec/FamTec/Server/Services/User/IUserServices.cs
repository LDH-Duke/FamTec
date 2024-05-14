using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace FamTec.Server.Services.User
{
    public interface IUserServices
    {
        /// <summary>
        /// 사용자 전체리스트 조회
        /// </summary>
        /// <returns></returns>
        public ValueTask<ResponseModel<UsersDTO>> GetAllUserList();

        /// <summary>
        /// 매개변수로 넘어온 USERID에 해당하는 사용자 정보 출력
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<UsersDTO>> GetUserInfo(string userid);

        /// <summary>
        /// 매개변수로 넘어온 사용자DTO 데이터베이스에 저장
        /// </summary>
        /// <param name="userdto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<UsersDTO>> AddUserInfo(UsersDTO dto);

        /// <summary>
        /// 매개변수로 넘어온 사용자DTO 데이터베이스에 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<UsersDTO>> UpdateUserInfo(UsersDTO dto);

        /// <summary>
        /// 매개변수로 넘어온 사용자DTO 데이터베이스에 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<UsersDTO>> DeleteUserInfo(UsersDTO dto);
    }
}


