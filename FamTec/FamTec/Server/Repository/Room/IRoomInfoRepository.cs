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
        ValueTask<RoomTb?> AddAsync(RoomTb? model);

        /// <summary>
        /// 전체 공간리스트 조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<RoomTb>?> GetAllList();

        /// <summary>
        /// 인덱스에 해당하는 공간정보 반환
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        ValueTask<RoomTb?> GetRommInfo(int? id);

        /// <summary>
        /// 사업장의 공간이름에 해당하는 정보조회
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ValueTask<RoomTb?> GetRoomInfo(string? name, int? floortbid);

        

        /// <summary>
        /// 층인덱스에 해당하는 공간리스트 조회
        /// </summary>
        /// <param name="floorid"></param>
        /// <returns></returns>
        ValueTask<List<RoomTb>?> GetRoomsList(int? floortbid);


        /// <summary>
        /// 공간정보 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> EditRoomsInfo(RoomTb? model);

        /// <summary>
        /// 공간정보 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> DeleteRoomsInfo(RoomTb? model);

    }
}
