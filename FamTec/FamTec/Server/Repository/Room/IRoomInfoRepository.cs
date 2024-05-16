using FamTec.Shared.Model;

namespace FamTec.Server.Repository.Room
{
    public interface IRoomInfoRepository
    {
        /// <summary>
        /// 공간 정보 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<RoomsTb?> AddAsync(RoomsTb? model);

        /// <summary>
        /// 전체 공간리스트 조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<RoomsTb>?> GetAllList();

        /// <summary>
        /// 해당ID에 해당하는 공간정보 반환
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ValueTask<RoomsTb?> GetRommInfo(int? id);

        /// <summary>
        /// 층정보에 해당하는 공간리스트 조회
        /// </summary>
        /// <param name="floorid"></param>
        /// <returns></returns>
        ValueTask<List<RoomsTb>?> GetRoomsList(int? floorid);

        /// <summary>
        /// 공간정보 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> EditRoomsInfo(RoomsTb? model);

        /// <summary>
        /// 공간정보 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> DeleteRoomsInfo(RoomsTb? model);

    }
}
