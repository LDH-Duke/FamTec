using FamTec.Server.Services.Floor;
using FamTec.Server.Services.Room;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO.Building;
using FamTec.Shared.Server.DTO.Floor;
using FamTec.Shared.Server.DTO.Room;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamTec.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private IRoomService RoomService;
        private SessionInfo session;

        public RoomController(IRoomService _roomservice)
        {
            this.RoomService = _roomservice;
            this.session = new SessionInfo();
        }

        [HttpGet]
        [Route("GetRoomList")]
        public async ValueTask<IActionResult> GetRoomList()
        {
            ResponseModel<RoomManagementDTO>? model = await RoomService.GetRoomListService(session);
            return Ok(model);
        }

        [HttpGet]
        [Route("GetBuildingList")]
        public async ValueTask<IActionResult> GetBuildingList()
        {
            ResponseModel<BuildingsDTO>? model = await RoomService.GetBuildingList(session);
            return Ok(model);
        }

        [HttpGet]
        [Route("GetFloorList")]
        public async ValueTask<IActionResult> GetFloorList()
        {
            ResponseModel<FloorDTO>? model = await RoomService.GetFloorList(1);
            return Ok(model);
        }

  


    }
}
