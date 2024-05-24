using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO.Room;

namespace FamTec.Server.Services.Room
{
    public interface IRoomService
    {
        // 공간추가
        public ValueTask<ResponseModel<RoomDTO>> AddRoomService(RoomDTO? dto, SessionInfo sessioninfo);

        public ValueTask<List<RoomManagementDTO>> GetRoomList(SessionInfo? sessioninfo);
    }
}
