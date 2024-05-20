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
        /// 전체조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<UserTb>?> GetAllList();

        /// <summary>
        /// 매개변수 사업장에 해당하는 사용자리스트 조회
        /// </summary>
        /// <param name="placetbid"></param>
        /// <returns></returns>
        ValueTask<List<UserTb>?> GetAllUserList(int? placetbid);

        /// <summary>
        /// ID로 단일모델 조회
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        ValueTask<UserTb?> GetUserInfo(string? userid, int? placetbid);

        /// <summary>
        /// 사용자 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> EditUserInfo(UserTb? model);

        /// <summary>
        /// 사용자 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> DeleteUserInfo(UserTb? model);
    }
}
