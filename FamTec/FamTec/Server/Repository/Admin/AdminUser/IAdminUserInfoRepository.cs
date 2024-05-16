using FamTec.Shared.Model;

namespace FamTec.Server.Repository.Admin.AdminUser
{
    public interface IAdminUserInfoRepository
    {
        /// <summary>
        /// 관리자 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<AdminsTb?> AddAsync(AdminsTb? model);

        /// <summary>
        /// 전체조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<AdminsTb>?> GetAllList();

        /// <summary>
        /// 관리자ID로 단일모델 조회
        /// </summary>
        /// <param name="adminid"></param>
        /// <returns></returns>
        ValueTask<AdminsTb?> GetAdminInfo(string? adminid);

        /// <summary>
        /// 관리자 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> EditAdminInfo(AdminsTb? model);

        /// <summary>
        /// 관리자 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> DeleteAdminInfo(AdminsTb? model);


    }
}
