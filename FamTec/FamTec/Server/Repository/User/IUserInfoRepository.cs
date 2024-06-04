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
        /// 유저 INDEX로 유저테이블 검색
        /// </summary>
        /// <param name="useridx"></param>
        /// <returns></returns>
        ValueTask<UserTb?> GetUserIndexInfo(int? useridx);

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

        /// <summary>
        /// 유저 테이블 내용 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> DeleteUserInfo(UserTb? model);

    }
}
