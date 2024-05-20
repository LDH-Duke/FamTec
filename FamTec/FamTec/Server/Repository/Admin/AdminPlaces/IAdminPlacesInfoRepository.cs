using FamTec.Shared.DTO;
using FamTec.Shared.Model;

namespace FamTec.Server.Repository.Admin.AdminPlaces
{
    public interface IAdminPlacesInfoRepository
    {
        /// <summary>
        /// 관리자 사업장 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<AdminPlaceTb?> AddAsync(AdminPlaceTb? model);

        /// <summary>
        /// 전체 사업장 조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<AdminPlaceTb>?> GetAllList();

        /// <summary>
        /// 관리자 USERID에 해당하는 전체 사업장 출력
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        ValueTask<List<AdminPlaceTb>?> GetAllUserList(int? admintbid);
        
        /// <summary>
        /// 관리자 PLACECODE에 해당하는 전체 사업장 출력
        /// </summary>
        /// <param name="placecd"></param>
        /// <returns></returns>
        ValueTask<List<AdminPlaceTb>?> GetAllPlaceList(int? placeid);

        /// <summary>
        /// 테이블 정보에 해당하는 단일 사업장모델 반환
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<AdminPlaceTb?> GetPlaceInfo(int? admintbid, int? placeid);

        /// <summary>
        /// 해당하는 관리자 사업장 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> DeleteAdminPlacesInfo(AdminPlaceTb? model);

        /// <summary>
        /// 해당하는 관리자 사업장 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> EditAdminPlacesInfo(AdminPlaceTb? model); // 수정

    }
}
