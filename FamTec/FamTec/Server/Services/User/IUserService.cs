using FamTec.Shared.DTO;
using FamTec.Shared.Model;

namespace FamTec.Server.Services.User
{
    public interface IUserService
    {
        /// <summary>
        /// 사용자 추가
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<UsersDTO>?> AddUserService(UsersDTO? dto);

        /// <summary>
        /// 전체조회
        /// </summary>
        /// <returns></returns>
        public ValueTask<ResponseModel<UsersDTO>?> GetAllUserService();


        /// <summary>
        /// 사업장에 해당하는 사용자 리스트 조회
        /// </summary>
        /// <param name="placetbid"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<UsersDTO>?> GetAllPlaceUser(int? placetbid);

        /// <summary>
        /// idx로 해당 사용자모델 조회
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<UsersDTO>?> GetUserService(int? idx);

        /// <summary>
        /// 사업장-유저 단일모델 조회
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="placetbid"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<UsersDTO>?> GetUserService(string? userid, int? placetbid);

        /// <summary>
        /// 유저 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<UsersDTO>?> EditUserService(UsersDTO? dto);

        /// <summary>
        /// 유저 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<UsersDTO>?> DeleteUserService(UsersDTO? dto);

    }
}
