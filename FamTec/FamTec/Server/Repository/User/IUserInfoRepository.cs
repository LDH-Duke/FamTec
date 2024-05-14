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
        ValueTask<List<UsersTb>> GetAllAsync();

        /// <summary>
        /// ID로 단일모델 조회
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        ValueTask<UsersTb> GetByUserInfo(string userid);

        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        ValueTask<bool> EditAsync(UsersTb model);

        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="tguserid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        ValueTask<bool> DeleteUserIdAsync(UsersTb model);
    }
}
