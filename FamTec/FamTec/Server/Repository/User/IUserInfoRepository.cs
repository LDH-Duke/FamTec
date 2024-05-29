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
        ValueTask<UserTb?> AddAsync(UserTb? model);

    

        /// <summary>
        /// USERID + PASSWORD에 해당하는 모델반환
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        ValueTask<UserTb?> GetUserInfo(string? userid, string? password);

        /// <summary>
        /// USERID 검사
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        ValueTask<UserTb?> UserIdCheck(string? userid);

    }
}
