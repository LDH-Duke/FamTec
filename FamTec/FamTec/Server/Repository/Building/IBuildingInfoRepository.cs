using FamTec.Shared.Model;

namespace FamTec.Server.Repository.Building
{
    public interface IBuildingInfoRepository
    {
        /// <summary>
        /// 입력
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<BuildingsTb> AddAsync(BuildingsTb model, string userid);

        /// <summary>
        /// 전체조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<BuildingsTb>> GetAllAsync();

        /// <summary>
        /// 사업장코드로 조회
        /// </summary>
        /// <param name="placecd"></param>
        /// <returns></returns>
        ValueTask<List<BuildingsTb>> GetByPlaceCDAsync(string placecd);

        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        ValueTask<bool> EditAsync(BuildingsTb model, string userid);

        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="buildingcd"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        ValueTask<bool> DeleteBuildingCDAsync(string buildingcd, string userid);
    }
}
