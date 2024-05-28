using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO.Admin;
using FamTec.Shared.Server.DTO.Admin.Place;

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
        /// 관리자 사업장 조회
        /// </summary>
        /// <param name="adminid"></param>
        /// <returns></returns>
        ValueTask<List<AdminPlaceDTO>?> GetMyWorks(int? adminid);

        /// <summary>
        /// 사업장장 정보 조회
        /// </summary>
        /// <param name="placeid"></param>
        /// <returns></returns>
        ValueTask<AddPlaceDTO?> GetWorksInfo(int? placeid);

        /// <summary>
        /// 해당사업장 삭제
        /// </summary>
        /// <param name="placeid"></param>
        /// <returns></returns>
        ValueTask<bool?> DeleteMyWorks(List<int>? placeid, string delName);
    }
}
