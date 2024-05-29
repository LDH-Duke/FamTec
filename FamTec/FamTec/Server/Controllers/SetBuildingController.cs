using FamTec.Server.Services.Building;
using FamTec.Server.Services.Floor;
using FamTec.Server.Services.Room;
using FamTec.Shared;
using FamTec.Shared.DTO;
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
    public class SetBuildingController : ControllerBase
    {
        private IBuildingService BuildingService;
        private IFloorService FloorService;
        private IRoomService RoomService;


        private SessionInfo session;

        public SetBuildingController(IBuildingService _buildingservice, IFloorService _floorservice, IRoomService _roomservice)
        {
            this.BuildingService = _buildingservice;
            this.FloorService = _floorservice;

            this.RoomService = _roomservice;

            this.session = new SessionInfo();
        }

        /// <summary>
        /// 사업장에 건물 추가
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insertbuilding")]
        public async ValueTask<IActionResult> InsertBuilding([FromBody]BuildingsDTO dto)
        {
            /*
            BuildingsDTO? dto = new BuildingsDTO();
            dto.BuildingCode = "BD00003"; // 건물코드
            dto.Name = "경기우체국"; // 건물명
            dto.Address = "경기도 하남시"; // 주소
            dto.Tel = "053-0000-0000"; // 전화번호
            dto.Usage = "빌딩"; // 건물용도
            dto.ConstComp = "ㅇㅇ건설"; // 시공업체
            dto.CompletionDt = DateTime.Now.AddDays(-50); // 준공년월
            dto.BuildingStruct = "네모구조"; // 건물구조
            dto.RoofStruct = "세모구조"; // 지붕구조
            dto.GrossFloorArea = 87.55f; // 연면적
            dto.LandArea = 44.77f; // 대지면적
            dto.BuildingArea = 157.22f; // 건축면적
            dto.FloorNum = 13; // 층수
            dto.GroundFloorNum = 8; // 지상층수
            dto.BasementFloorNum = 5; // 지하층수
            dto.BuildingHeight = 188f; // 건물높이
            dto.GroundHeight = 150f; // 지상높이
            dto.BasementHeight = 30f; // 지하깊이
            dto.PackingNum = 130; // 주차대수
            dto.InnerPackingNum = 100; // 옥내대수
            dto.OuterPackingNum = 30; // 옥외대수
            dto.ElecCapacity = 66978f; // 전기용량
            dto.FaucetCapacity = 5646f; // 수전용량
            dto.GenerationCapacity = 65468f; // 발전용량
            dto.WaterCapacity = 65468f; // 급수용량
            dto.ElevWaterCapacity = 6854987f; // 고가수조
            dto.WaterTank = 553f; // 저수조
            dto.GasCapacity = 687.43f; // 가스용량
            dto.Boiler = 828f; // 보일러
            dto.WaterDispenser = 3355f; // 냉온수기
            dto.LiftNum = 5; // 승강대수
            dto.PeopleLiftNum = 5; // 인승용
            dto.CargoLiftNum = 0; // 화물용
            dto.CoolHeatCapacity = 33.44f; // 냉난용량
            dto.HeatCapacity = 344f; // 난방용량
            dto.CoolCapacity = 322f; // 냉방용량
            dto.LandScapeArea = 578.4f; // 조경면적
            dto.GroundArea = 232f; // 지상면적
            dto.RooftopArea = 22f; // 옥상면적
            dto.ToiletNum = 10; // 화장실개수
            dto.MenToiletNum = 5; // 남자화장실개수
            dto.WomenToiletNum = 5; // 여자화장실 개수
            dto.FireRating = "우수";
            dto.SepticTankCapacity = 33.4f; // 정화조용량

            session.selectPlace = 5;
            */
            ResponseModel<BuildingsDTO>? model = await BuildingService.AddBuildingService(dto, session);

            return Ok(model);
        }

        /// <summary>
        /// 사업장에 해당하는 건물리스트 출력
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("SelectMyBuilding")]
        public async ValueTask<IActionResult> SelectMyBuilding()
        {
            ResponseModel<BuildingsDTO>? model = await BuildingService.GetBuilidngListService(session);
            return Ok(model);
        }

        [HttpPost]
        [Route("DeleteBuilding")]
        public async ValueTask<IActionResult> DeleteMyBuilding([FromBody] List<int> idx)
        {
            ResponseModel<string>? model = await BuildingService.DeleteBuildingService(idx, session);
            return Ok(model);
        }

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
