using FamTec.Shared.Model;

namespace FamTec.Server.Repository.Interfaces
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
        /// 조회 - ID값
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        ValueTask<UsersTb> GetByUserId(string userid);

    }
}
