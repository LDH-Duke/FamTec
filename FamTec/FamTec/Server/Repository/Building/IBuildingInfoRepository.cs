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
        ValueTask<BuildingTb?> AddAsync(BuildingTb? model);

        /// <summary>
        /// 건물 전체조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<BuildingTb>?> GetAllList();

        /// <summary>
        /// 해당사업장 코드에 해당하는 모든 건물 출력
        /// </summary>
        /// <returns></returns>
        ValueTask<List<BuildingTb>?> GetBuildingList(int? placeid);

        /// <summary>
        /// 해당 건물코드에 해당하는 건물 출력
        /// </summary>
        /// <returns></returns>
        ValueTask<BuildingTb?> GetBuildingInfo(string? buildingcode, int? placeid);

        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        ValueTask<bool?> EditBuildingInfo(BuildingTb? model);

        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="buildingcd"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        ValueTask<bool?> DeleteBuildingInfo(BuildingTb? model);
    }
}
