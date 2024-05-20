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
        ValueTask<FloorTb?> AddAsync(FloorTb? model);

        /// <summary>
        /// 층 정보 전체 조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<FloorTb>?> GetAllList();

        /// <summary>
        /// 해당 건물인덱스에 해당하는 층정보 조회
        /// </summary>
        /// <param name="buildingtbid"></param>
        /// <returns></returns>
        ValueTask<List<FloorTb>?> GetFloorList(int? buildingtbid);

        /// <summary>
        /// 층 인덱스에 해당하는 층모델 정보 조회
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ValueTask<FloorTb?> GetFloorIndexInfo(int? id);

        /// <summary>
        /// 층 정보 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> EditFloorInfo(FloorTb? model);

        /// <summary>
        /// 층 정보 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> DeleteFloorInfo(FloorTb? model);

    }
}
