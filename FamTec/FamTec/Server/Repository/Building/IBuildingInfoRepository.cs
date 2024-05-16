using FamTec.Shared.Model;

namespace FamTec.Server.Repository.Building
{
    public interface IBuildingInfoRepository
    {
        /// <summary>
        /// 건물추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<BuildingsTb?> AddAsync(BuildingsTb? model);

        /// <summary>
        /// 건물 전체조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<BuildingsTb>?> GetAllList();

        /// <summary>
        /// 해당사업장 코드에 해당하는 모든 건물 출력
        /// </summary>
        /// <returns></returns>
        ValueTask<List<BuildingsTb>?> GetBuildingList(string? placecode);

        /// <summary>
        /// 해당 건물코드에 해당하는 건물 출력
        /// </summary>
        /// <returns></returns>
        ValueTask<BuildingsTb?> GetBuildingInfo(string? buildingcode);

        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        ValueTask<bool?> EditBuildingInfo(BuildingsTb? model);

        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="buildingcd"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        ValueTask<bool?> DeleteBuildingInfo(BuildingsTb? model);
    }
}
