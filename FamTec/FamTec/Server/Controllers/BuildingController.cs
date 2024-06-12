﻿using FamTec.Server.Services.Building;
using FamTec.Server.Services.Floor;
using FamTec.Server.Services.Room;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO;
using FamTec.Shared.Server.DTO.Building;
using FamTec.Shared.Server.DTO.Floor;
using FamTec.Shared.Server.DTO.Room;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FamTec.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private IBuildingService BuildingService;
        private IFloorService FloorService;
        private IRoomService RoomService;


        private SessionInfo session;

        public BuildingController(IBuildingService _buildingservice,
            IFloorService _floorservice,
            IRoomService _roomservice)
        {
            this.BuildingService = _buildingservice;
            this.FloorService = _floorservice;

            this.RoomService = _roomservice;

            this.session = new SessionInfo();
        }

        /// <summary>
        /// 사업장에 해당하는 건물리스트 출력 -- 확인해야함
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("MyBuildings")]
        public async ValueTask<IActionResult> SelectMyBuilding()
        {
            int placeidx = 4;
            
            ResponseList<BuildingsDTO>? model = await BuildingService.GetBuilidngListService(placeidx);
            
            if(model is not null)
            {
                if (model.code == 200)
                {
                    return Ok(model);
                }
                else
                {
                    return BadRequest(model);
                }
            }
            else
            {
                return BadRequest(model);
            }
        }


        /// <summary>
        /// 사업장에 건물 추가
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Addbuilding")]
        public async ValueTask<IActionResult> InsertBuilding([FromBody]BuildingsDTO dto)
        {
            // 토큰내용
            int placeidx = 4;
            
            ResponseUnit<bool> model = await BuildingService.AddBuildingService(dto, placeidx);

            if(model is not null)
            {
                if(model.code == 200)
                {
                    return Ok(model);
                }
                else
                {
                    return BadRequest(model);
                }
            }
            else
            {
                return BadRequest(model);
            }
        }

  
        /*
        [HttpPost]
        [Route("DeleteBuilding")]
        public async ValueTask<IActionResult> DeleteMyBuilding([FromBody] List<int> idx)
        {
            ResponseModel<string>? model = await BuildingService.DeleteBuildingService(idx, session);
            return Ok(model);
        }
        */

        [HttpPost]
        [Route("AddFloor")]
        public async ValueTask<IActionResult> AddFloor([FromBody]FloorDTO dto)
        {
            ResponseModel<FloorDTO>? model = await FloorService.AddFloorService(dto, session);
            return Ok(model);
        }

        [HttpGet]
        [Route("GetFloorList/{buildingid?}")]
        public async ValueTask<IActionResult> GetFloorList(int buildingid)
        {
            ResponseModel<FloorDTO>? model = await FloorService.GetFloorListService(buildingid);
            return Ok(model);
        }

        [HttpPost]
        [Route("DeleteFloor")]
        public async ValueTask<IActionResult> DeleteFloor([FromBody] List<int> idx)
        {
            ResponseModel<string>? model = await FloorService.DeleteFloorService(idx, session);
            return Ok(model);
        }

        [HttpPost]
        [Route("AddRoom")]
        public async ValueTask<IActionResult> AddRoom([FromBody]RoomDTO dto)
        {
            ResponseModel<RoomDTO>? model = await RoomService.AddRoomService(dto, session);
            return Ok(model);
        }

    }
}
