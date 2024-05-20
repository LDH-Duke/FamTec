using FamTec.Shared.DTO;

namespace FamTec.Server.Services.User
{
    public interface IUserService
    {
        /// <summary>
        /// 사용자 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<UsersDTO>> AddUserService(UsersDTO? model);

        /// <summary>
        /// 전체조회
        /// </summary>
        /// <returns></returns>
        public ValueTask<ResponseModel<UsersDTO>> GetAllUserService();


        /// <summary>
        /// 사업장 조회
        /// </summary>
        /// <param name="placeid"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<UsersDTO>> GetAllPlaceUser(int? placeid);


    }
}
