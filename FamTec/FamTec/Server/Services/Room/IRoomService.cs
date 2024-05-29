using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO.Building;
using FamTec.Shared.Server.DTO.Floor;
using FamTec.Shared.Server.DTO.Room;

namespace FamTec.Server.Services.Room
{
    public interface IRoomService
    {
        // 공간추가
        public ValueTask<ResponseModel<RoomDTO>> AddRoomService(RoomDTO? dto, SessionInfo sessioninfo);

        /// <summary>
        /// 해당 사업장의 공간List 조회
        /// </summary>
        /// <param name="sessioninfo"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<RoomManagementDTO>> GetRoomListService(SessionInfo sessioninfo);

        /// <summary>
        /// 해당 사업장의 건물 List 조회
        /// </summary>
        /// <param name="sessionsinfo"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<BuildingsDTO>> GetBuildingList(SessionInfo sessionsinfo);

        /// <summary>
        /// 건물에 속해있는 층 List 조회
        /// </summary>
        /// <param name="buildingidx"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<FloorDTO>> GetFloorList(int? buildingidx);


    }
}
