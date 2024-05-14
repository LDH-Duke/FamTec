using FamTec.Shared.Model;

namespace FamTec.Server.Repository.User
{
    public interface IUserInfoRepository
    {
        /// <summary>
        /// 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<UsersTb> AddAsync(UsersTb model);

        /// <summary>
        /// 전체조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<UsersTb>> GetAllList();

        /// <summary>
        /// ID로 단일모델 조회
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        ValueTask<UsersTb> GetUserInfo(string userid);

        /// <summary>
        /// 사용자 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool> EditUserInfo(UsersTb model);

        /// <summary>
        /// 사용자 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool> DeleteUserInfo(UsersTb model);
    }
}
