using FamTec.Shared.Model;

namespace FamTec.Server.Repository.Floor
{
    public interface IFloorInfoRepository
    {
        /// <summary>
        /// 층 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<FloorsTb?> AddAsync(FloorsTb? model);

        /// <summary>
        /// 층 정보 전체 조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<FloorsTb>?> GetAllList();

        /// <summary>
        /// 해당 건물코드에 해당하는 층정보 조회
        /// </summary>
        /// <param name="buildingcd"></param>
        /// <returns></returns>
        ValueTask<List<FloorsTb>?> GetFloorList(string? buildingcd);

        /// <summary>
        /// 층 정보 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> EditFloorInfo(FloorsTb? model);

        /// <summary>
        /// 층 정보 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> DeleteFloorInfo(FloorsTb? model);

    }
}
