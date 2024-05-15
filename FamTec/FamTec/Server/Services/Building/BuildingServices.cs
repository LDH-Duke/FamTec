using FamTec.Client.Pages.Place;
using FamTec.Server.Repository.Building;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using Microsoft.JSInterop.Infrastructure;
using System.Net;

namespace FamTec.Server.Services.Building
{
    public class BuildingServices : IBuildingServices
    {
        private readonly IBuildingInfoRepository BuildingInfoRepository;

        ResponseOBJ<BuildingsDTO> Response;
        Func<string, BuildingsDTO, int, ResponseModel<BuildingsDTO>> FuncResponseOBJ;
        Func<string, List<BuildingsDTO>, int, ResponseModel<BuildingsDTO>> FuncResponseList;


        public BuildingServices(IBuildingInfoRepository _buildinginforepository)
        {
            this.BuildingInfoRepository = _buildinginforepository;

            Response = new ResponseOBJ<BuildingsDTO>();
            FuncResponseOBJ = Response.RESPMessage;
            FuncResponseList = Response.RESPMessageList;
        }

        /// <summary>
        /// 건물 전체조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<ResponseModel<BuildingsDTO>> GetAllBuildings()
        {
            List<BuildingsTb>? result = await BuildingInfoRepository.GetAllList();

            if(result is [_, ..])
            {
                return FuncResponseList("전체데이터 조회 성공", result.Select(e => new BuildingsDTO()
                {
                    BuildingCode = e.BuildingCd, // 건물코드
                    Name = e.Name, // 건물이름
                    Address = e.Address, // 주소
                    Tel = e.Tel, // 전화번호
                    Usage = e.Usage, // 건물용도
                    ConstComp = e.ConstComp, // 시공업체
                    CompletionData = e.CompletionDate, // 준공년월
                    BuildingStruct = e.BuildingStruct, // 건물구조
                    RoofStruct = e.RoofStruct, // 지붕구조
                    GrossFloorArea = e.GrossFloorArea, // 연면적
                    LandArea = e.LandArea, // 대지면적
                    BuildingArea = e.BuildingArea, // 건축면적
                    FloorNum = e.FloorNum, // 건물층수
                    GroundFloorNum = e.GroundFloorNum, // 지상층수
                    BasementFloorNum = e.BasementFloorNum, // 지하층수
                    BuildingHeight = e.BuildingHeight, // 건물높이
                    GroundHeight = e.GroundHeight, // 건물 지상높이
                    BasementHeight = e.BasementHeight, // 건물 지상깊이
                    PackingNum = e.PackingNum, // 주차대수
                    InnerPackingNum = e.InnerPackingNum, // 옥내대수
                    OuterPackingNum = e.OuterPackingNum, // 옥외대수
                    ElecCapacity = e.ElecCapacity, // 전기용량
                    FaucetCapacity = e.FaucetCapacity, // 수전용량
                    GenerationCapacity = e.GenerationCapacity, // 발전용량
                    WaterCapacity = e.WaterCapacity, // 급수용량
                    ElevWaterTank = e.ElevWaterTank, // 고가수조
                    WaterTank = e.WaterTank, // 저수조
                    GasCapacity =e.GasCapacity, // 가스용량
                    Boiler = e.Boiler, // 보일러
                    WaterDispenser = e.WaterDispenser, // 냉온수기
                    LiftNum = e.LiftNum, // 승강대수
                    PeopleLiftNum = e.PeopleLiftNum, // 인승용
                    CargoLiftNum = e.CargoLiftNum, // 화물용
                    CoolHeatCapacity = e.CoolHeatCapacity, // 냉난방용량
                    HeatCapacity = e.HeatCapacity, // 난방용량
                    CoolCapacity = e.CoolCapacity, // 냉방용량
                    LandScapeArea = e.LandscapeArea, // 조경면적
                    GroundArea = e.GroundArea, // 지상면적
                    RooftopArea = e.RooftopArea, // 옥상면적
                    ToiletNum = e.ToiletNum, // 화장실 개수
                    MenToiletNum = e.MenToiletNum, // 남자화장실 개수
                    WomenToiletNum = e.WomenToiletNum, // 여자화장실 개수
                    FireRating = e.FireRating, // 소방등급
                    SepticTankCapacity = e.SepticTankCapacity, // 정화조 용량
                    PlaceCD = e.BuildingCd, // 사업장코드
                }).ToList(), 200);
            }
            else
            {
                return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
            }

        }

        /// <summary>
        /// 매개변수로 넘어온 사업장Code에 해당하는 건물리스트 출력
        /// </summary>
        /// <param name="placecd"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<BuildingsDTO>> GetBuildingList(string placecd)
        {
            List<BuildingsTb>? result = await BuildingInfoRepository.GetBuildingList(placecd);
            
            if(result is [_, ..])
            {
                return FuncResponseList("전체데이터 조회 성공", result.Select(e => new BuildingsDTO()
                {
                    BuildingCode = e.BuildingCd, // 건물코드
                    Name = e.Name, // 건물이름
                    Address = e.Address, // 주소
                    Tel = e.Tel, // 전화번호
                    Usage = e.Usage, // 건물용도
                    ConstComp = e.ConstComp, // 시공업체
                    CompletionData = e.CompletionDate, // 준공년월
                    BuildingStruct = e.BuildingStruct, // 건물구조
                    RoofStruct = e.RoofStruct, // 지붕구조
                    GrossFloorArea = e.GrossFloorArea, // 연면적
                    LandArea = e.LandArea, // 대지면적
                    BuildingArea = e.BuildingArea, // 건축면적
                    FloorNum = e.FloorNum, // 건물층수
                    GroundFloorNum = e.GroundFloorNum, // 지상층수
                    BasementFloorNum = e.BasementFloorNum, // 지하층수
                    BuildingHeight = e.BuildingHeight, // 건물높이
                    GroundHeight = e.GroundHeight, // 건물 지상높이
                    BasementHeight = e.BasementHeight, // 건물 지상깊이
                    PackingNum = e.PackingNum, // 주차대수
                    InnerPackingNum = e.InnerPackingNum, // 옥내대수
                    OuterPackingNum = e.OuterPackingNum, // 옥외대수
                    ElecCapacity = e.ElecCapacity, // 전기용량
                    FaucetCapacity = e.FaucetCapacity, // 수전용량
                    GenerationCapacity = e.GenerationCapacity, // 발전용량
                    WaterCapacity = e.WaterCapacity, // 급수용량
                    ElevWaterTank = e.ElevWaterTank, // 고가수조
                    WaterTank = e.WaterTank, // 저수조
                    GasCapacity = e.GasCapacity, // 가스용량
                    Boiler = e.Boiler, // 보일러
                    WaterDispenser = e.WaterDispenser, // 냉온수기
                    LiftNum = e.LiftNum, // 승강대수
                    PeopleLiftNum = e.PeopleLiftNum, // 인승용
                    CargoLiftNum = e.CargoLiftNum, // 화물용
                    CoolHeatCapacity = e.CoolHeatCapacity, // 냉난방용량
                    HeatCapacity = e.HeatCapacity, // 난방용량
                    CoolCapacity = e.CoolCapacity, // 냉방용량
                    LandScapeArea = e.LandscapeArea, // 조경면적
                    GroundArea = e.GroundArea, // 지상면적
                    RooftopArea = e.RooftopArea, // 옥상면적
                    ToiletNum = e.ToiletNum, // 화장실 개수
                    MenToiletNum = e.MenToiletNum, // 남자화장실 개수
                    WomenToiletNum = e.WomenToiletNum, // 여자화장실 개수
                    FireRating = e.FireRating, // 소방등급
                    SepticTankCapacity = e.SepticTankCapacity, // 정화조 용량
                    PlaceCD = e.PlacecodeCd, // 사업장코드
                }).ToList(), 200);
            }
            else
            {
                return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
            }
        }


        /// <summary>
        /// 매개변수로 넘어온 건물Code에 해당하는 건물정보 조회
        /// </summary>
        /// <param name="buildingcd"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<BuildingsDTO>> GetBuildingInfo(string buildingcd)
        {
            if(!String.IsNullOrWhiteSpace(buildingcd))
            {
                BuildingsTb? model = await BuildingInfoRepository.GetBuildingInfo(buildingcd);

                if(model is not null)
                {
                    return FuncResponseOBJ("데이터 것맥 성공.", new BuildingsDTO()
                    {
                        BuildingCode = model.BuildingCd, // 건물코드
                        Name = model.Name, // 건물이름
                        Address = model.Address, // 주소
                        Tel = model.Tel, // 전화번호
                        Usage = model.Usage, // 건물용도
                        ConstComp = model.ConstComp, // 시공업체
                        CompletionData = model.CompletionDate, // 준공년월
                        BuildingStruct = model.BuildingStruct, // 건물구조
                        RoofStruct = model.RoofStruct, // 지붕구조
                        GrossFloorArea = model.GrossFloorArea, // 연면적
                        LandArea = model.LandArea, // 대지면적
                        BuildingArea = model.BuildingArea, // 건축면적
                        FloorNum = model.FloorNum, // 건물층수
                        GroundFloorNum = model.GroundFloorNum, // 지상층수
                        BasementFloorNum = model.BasementFloorNum, // 지하층수
                        BuildingHeight = model.BuildingHeight, // 건물높이
                        GroundHeight = model.GroundHeight, // 건물 지상높이
                        BasementHeight = model.BasementHeight, // 건물 지상깊이
                        PackingNum = model.PackingNum, // 주차대수
                        InnerPackingNum = model.InnerPackingNum, // 옥내대수
                        OuterPackingNum = model.OuterPackingNum, // 옥외대수
                        ElecCapacity = model.ElecCapacity, // 전기용량
                        FaucetCapacity = model.FaucetCapacity, // 수전용량
                        GenerationCapacity = model.GenerationCapacity, // 발전용량
                        WaterCapacity = model.WaterCapacity, // 급수용량
                        ElevWaterTank = model.ElevWaterTank, // 고가수조
                        WaterTank = model.WaterTank, // 저수조
                        GasCapacity = model.GasCapacity, // 가스용량
                        Boiler = model.Boiler, // 보일러
                        WaterDispenser = model.WaterDispenser, // 냉온수기
                        LiftNum = model.LiftNum, // 승강대수
                        PeopleLiftNum = model.PeopleLiftNum, // 인승용
                        CargoLiftNum = model.CargoLiftNum, // 화물용
                        CoolHeatCapacity = model.CoolHeatCapacity, // 냉난방용량
                        HeatCapacity = model.HeatCapacity, // 난방용량
                        CoolCapacity = model.CoolCapacity, // 냉방용량
                        LandScapeArea = model.LandscapeArea, // 조경면적
                        GroundArea = model.GroundArea, // 지상면적
                        RooftopArea = model.RooftopArea, // 옥상면적
                        ToiletNum = model.ToiletNum, // 화장실 개수
                        MenToiletNum = model.MenToiletNum, // 남자화장실 개수
                        WomenToiletNum = model.WomenToiletNum, // 여자화장실 개수
                        FireRating = model.FireRating, // 소방등급
                        SepticTankCapacity = model.SepticTankCapacity, // 정화조 용량
                        PlaceCD = model.BuildingCd, // 사업장코드
                    }, 200);
                }
                else
                {
                    return FuncResponseOBJ("해당 데이터가 존재하지 않습니다.", null, 404);
                }
            }
            else
            {
                return FuncResponseOBJ("데이터가 비어있습니다.", null, 200);
            }
        }

        /// <summary>
        /// 매개변수로 넘어온 건물DTO 데이터베이스에 저장
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<BuildingsDTO>> AddBuildingInfo(BuildingsDTO dto)
        {
            if(dto is not null && !String.IsNullOrWhiteSpace(dto.BuildingCode))
            {
                BuildingsTb? model = await BuildingInfoRepository.GetBuildingInfo(dto.BuildingCode);
                
                if(model == null) // 해당 건물코드로 건물이 없음
                {
                    BuildingsTb buildingtb = new BuildingsTb()
                    {
                        BuildingCd = dto.BuildingCode, // 건물코드
                        Name = dto.Name, // 건물명
                        Address = dto.Address, // 건물주소
                        Tel = dto.Tel, // 전화번호
                        Usage = dto.Usage, // 건물용도
                        ConstComp = dto.ConstComp, // 시공업체
                        CompletionDate = dto.CompletionData, // 준공년월
                        BuildingStruct = dto.BuildingStruct, // 건물구조
                        RoofStruct = dto.RoofStruct, //지붕구조
                        GrossFloorArea = dto.GrossFloorArea, // 연면적
                        LandArea = dto.LandArea, // 대지면적
                        BuildingArea = dto.BuildingArea, // 건축면적
                        FloorNum = dto.FloorNum, // 건물층수
                        GroundFloorNum = dto.GroundFloorNum, // 지상층수
                        BasementFloorNum = dto.BasementFloorNum, // 지하층수
                        BuildingHeight = dto.BuildingHeight, // 건물높이
                        GroundHeight = dto.GroundHeight, // 건물 지상높이
                        BasementHeight = dto.BasementHeight, // 건물 지하깊이
                        PackingNum = dto.PackingNum, // 주차대수
                        InnerPackingNum = dto.InnerPackingNum, // 옥내대수
                        OuterPackingNum = dto.OuterPackingNum, // 옥외대수
                        ElecCapacity = dto.ElecCapacity, // 전기용량
                        FaucetCapacity = dto.FaucetCapacity, // 수전용량
                        GenerationCapacity = dto.GenerationCapacity, // 발전용량
                        WaterCapacity = dto.WaterCapacity, // 급수용량
                        ElevWaterTank = dto.ElevWaterTank, // 고가수조
                        WaterTank = dto.WaterTank, // 저수조
                        GasCapacity = dto.GasCapacity, // 가스용량
                        Boiler = dto.Boiler, // 보일러
                        WaterDispenser = dto.WaterDispenser, // 냉온수기
                        LiftNum = dto.LiftNum, // 승강대수
                        PeopleLiftNum = dto.PeopleLiftNum, // 인승용
                        CargoLiftNum = dto.CargoLiftNum, // 화물용
                        CoolHeatCapacity = dto.CoolHeatCapacity, // 냉 난방 용량
                        HeatCapacity = dto.HeatCapacity, // 난방용량
                        CoolCapacity = dto.CoolCapacity, // 냉방용량
                        LandscapeArea = dto.LandScapeArea, // 조경면적
                        GroundArea = dto.GroundArea, // 지상면적
                        RooftopArea = dto.RooftopArea, // 옥상면적
                        ToiletNum = dto.ToiletNum, // 화장실개수
                        MenToiletNum = dto.MenToiletNum, // 남자화장실 개수
                        WomenToiletNum = dto.WomenToiletNum, // 여자화장실 개수
                        FireRating = dto.FireRating, // 소방등급
                        SepticTankCapacity = dto.SepticTankCapacity, // 정화조용량
                        
                        CreateDt = DateTime.Now, // 생성일
                        CreateUser = "토큰USER",

                        PlacecodeCd = dto.PlaceCD, // 건물코드 - 외래키 참조
                    };

                    var result = await BuildingInfoRepository.AddAsync(buildingtb);

                    if(result == null)
                    {
                        // ADD에 실패하였을때
                        return FuncResponseOBJ("데이터 추가에 실패하였습니다.", null, 404);
                    }
                    else
                    {
                        return FuncResponseOBJ("건물 추가에 성공하였습니다.", new BuildingsDTO()
                        {
                            BuildingCode = model.BuildingCd, // 건물코드
                            Name = model.Name, // 건물이름
                            Address = model.Address, // 주소
                            Tel = model.Tel, // 전화번호
                            Usage = model.Usage, // 건물용도
                            ConstComp = model.ConstComp, // 시공업체
                            CompletionData = model.CompletionDate, // 준공년월
                            BuildingStruct = model.BuildingStruct, // 건물구조
                            RoofStruct = model.RoofStruct, // 지붕구조
                            GrossFloorArea = model.GrossFloorArea, // 연면적
                            LandArea = model.LandArea, // 대지면적
                            BuildingArea = model.BuildingArea, // 건축면적
                            FloorNum = model.FloorNum, // 건물층수
                            GroundFloorNum = model.GroundFloorNum, // 지상층수
                            BasementFloorNum = model.BasementFloorNum, // 지하층수
                            BuildingHeight = model.BuildingHeight, // 건물높이
                            GroundHeight = model.GroundHeight, // 건물 지상높이
                            BasementHeight = model.BasementHeight, // 건물 지상깊이
                            PackingNum = model.PackingNum, // 주차대수
                            InnerPackingNum = model.InnerPackingNum, // 옥내대수
                            OuterPackingNum = model.OuterPackingNum, // 옥외대수
                            ElecCapacity = model.ElecCapacity, // 전기용량
                            FaucetCapacity = model.FaucetCapacity, // 수전용량
                            GenerationCapacity = model.GenerationCapacity, // 발전용량
                            WaterCapacity = model.WaterCapacity, // 급수용량
                            ElevWaterTank = model.ElevWaterTank, // 고가수조
                            WaterTank = model.WaterTank, // 저수조
                            GasCapacity = model.GasCapacity, // 가스용량
                            Boiler = model.Boiler, // 보일러
                            WaterDispenser = model.WaterDispenser, // 냉온수기
                            LiftNum = model.LiftNum, // 승강대수
                            PeopleLiftNum = model.PeopleLiftNum, // 인승용
                            CargoLiftNum = model.CargoLiftNum, // 화물용
                            CoolHeatCapacity = model.CoolHeatCapacity, // 냉난방용량
                            HeatCapacity = model.HeatCapacity, // 난방용량
                            CoolCapacity = model.CoolCapacity, // 냉방용량
                            LandScapeArea = model.LandscapeArea, // 조경면적
                            GroundArea = model.GroundArea, // 지상면적
                            RooftopArea = model.RooftopArea, // 옥상면적
                            ToiletNum = model.ToiletNum, // 화장실 개수
                            MenToiletNum = model.MenToiletNum, // 남자화장실 개수
                            WomenToiletNum = model.WomenToiletNum, // 여자화장실 개수
                            FireRating = model.FireRating, // 소방등급
                            SepticTankCapacity = model.SepticTankCapacity, // 정화조 용량
                            PlaceCD = model.BuildingCd, // 사업장코드
                        }, 200);
                    }
                }
                else
                {
                    return FuncResponseOBJ("이미 해당 코드의 건물이 존재합니다.", null, 200);
                }
            }
            else
            {
                return FuncResponseOBJ("데이터가 비어있습니다.", null, 404);
            }
        }
      

       
        /// <summary>
        /// 매개변수로 넘어온 건물DTO 데이터베이스에 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<BuildingsDTO>> UpdateBuildingInfo(BuildingsDTO dto)
        {
            if(dto is not null && !String.IsNullOrWhiteSpace(dto.BuildingCode))
            {
                BuildingsTb? model = await BuildingInfoRepository.GetBuildingInfo(dto.BuildingCode); // 해당 건물코드로 건물이 있는지 조회

                if(model == null) // 없음
                {
                    return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 404);
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(dto.Name)) // 건물이름
                        model.Name = dto.Name;

                    if (!String.IsNullOrWhiteSpace(dto.Address)) // 주소
                        model.Address = dto.Address;

                    if (!String.IsNullOrWhiteSpace(dto.Tel)) // 전화번호
                        model.Tel = dto.Tel;

                    if (!String.IsNullOrWhiteSpace(dto.Usage)) // 건물용도
                        model.Usage = dto.Usage;

                    if (!String.IsNullOrWhiteSpace(dto.ConstComp)) // 시공업체
                        model.ConstComp = dto.ConstComp;

                    if (dto.CompletionData is not null) // 준공년월
                        model.CompletionDate = dto.CompletionData;

                    if (!String.IsNullOrWhiteSpace(dto.BuildingStruct)) // 건물구조
                        model.BuildingStruct = dto.BuildingStruct;

                    if (!String.IsNullOrWhiteSpace(dto.RoofStruct)) // 지붕구조
                        model.RoofStruct = dto.RoofStruct;

                    if (dto.GrossFloorArea != null) // 연면적
                        model.GrossFloorArea = dto.GrossFloorArea;

                    if (dto.LandArea != null) // 대지면적
                        model.LandArea = dto.LandArea;

                    if (dto.BuildingArea != null) // 건축면적
                        model.BuildingArea = dto.BuildingArea;

                    if (dto.FloorNum != null) // 건물층수
                        model.FloorNum = dto.FloorNum;

                    if (dto.GroundFloorNum != null) // 지상층수
                        model.GroundFloorNum = dto.GroundFloorNum;

                    if (dto.BasementFloorNum != null) // 지하 층수
                        model.BasementFloorNum = dto.BasementFloorNum;

                    if (dto.BuildingHeight != null) // 건물 높이
                        model.BuildingHeight = dto.BuildingHeight;

                    if (dto.GroundHeight != null) // 건물 지상높이
                        model.GroundHeight = dto.GroundHeight; 

                }

            }
        }

        /// <summary>
        /// 매개변수로 넘어온 건물DTO 데이터베이스에 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<BuildingsDTO>> DeleteBuildingInfo(BuildingsDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
