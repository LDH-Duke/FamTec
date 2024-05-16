using FamTec.Shared.Model;

namespace FamTec.Server.Repository.SocketRoom
{
    public interface ISocketRoomsRepository
    {
        /// <summary>
        /// 소켓 그룹 추가
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<SocketRoomsTb?> AddAsync(SocketRoomsTb? model);

        /// <summary>
        /// 소켓 그룹 전체조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<SocketRoomsTb>?> GetAllList();

        /// <summary>
        /// 소켓 RoomCode에 해당하는 소켓 조회
        /// </summary>
        /// <param name="roomcode"></param>
        /// <returns></returns>
        ValueTask<SocketRoomsTb?> GetSocketRoomInfo(string roomcode);

        /// <summary>
        /// 소켓 그룹 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> EditRoomsInfo(SocketRoomsTb? model);

        /// <summary>
        /// 소켓 그룹 삭제
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool?> DeleteRoomsInfo(SocketRoomsTb? model);
    }
}
