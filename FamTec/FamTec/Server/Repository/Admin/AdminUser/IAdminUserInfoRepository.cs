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
        ValueTask<AdminTb?> AddAsync(AdminTb? model);

        /// <summary>
        /// 전체조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<AdminTb>?> GetAllList();

        /// <summary>
        /// USERTABLE INDEX + DEPARTMENT INDEX = 모델조회
        /// </summary>
        /// <param name="usertbid"></param>
        /// <param name="departmentid"></param>
        /// <returns></returns>
        ValueTask<AdminTb?> GetAdminInfo(int? usertbid, int? departmentid);

        /// <summary>
        /// 매개변수의 관리자ID에 해당하는 관리자모델 리스트 조회
        /// </summary>
        /// <param name="adminid"></param>
        /// <returns></returns>
        ValueTask<AdminTb?> GetAdminUserList(int? usertbid);

        /// <summary>
        /// 매개변수의 부서ID에 해당하는 관리자모델 리스트 조회
        /// </summary>
        /// <param name="departmnetid"></param>
        /// <returns></returns>
        ValueTask<List<AdminTb>?> GetAdminDepartment(int? departmnetid);


        /// <summary>
        /// 관리자 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> EditAdminInfo(AdminTb? model);

        /// <summary>
        /// 관리자 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> DeleteAdminInfo(AdminTb? model);


    }
}
