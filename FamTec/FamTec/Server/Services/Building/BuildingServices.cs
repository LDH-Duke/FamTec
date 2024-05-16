using FamTec.Client.Pages.Place;
using FamTec.Server.Repository.Building;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using Microsoft.AspNetCore.Routing.Constraints;
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
                        BuildingsTb temp = new BuildingsTb();
                        temp.BasementFloorNum = 1;

                    
                        return FuncResponseOBJ("건물 추가에 성공하였습니다.", new BuildingsDTO()
                        {
                            BuildingCode = buildingtb.BuildingCd, // 건물코드
                            Name = buildingtb.Name, // 건물이름
                            Address = buildingtb.Address, // 주소
                            Tel = buildingtb.Tel, // 전화번호
                            Usage = buildingtb.Usage, // 건물용도
                            ConstComp = buildingtb.ConstComp, // 시공업체
                            CompletionData = buildingtb.CompletionDate, // 준공년월
                            BuildingStruct = buildingtb.BuildingStruct, // 건물구조
                            RoofStruct = buildingtb.RoofStruct, // 지붕구조
                            GrossFloorArea = buildingtb.GrossFloorArea, // 연면적
                            LandArea = buildingtb.LandArea, // 대지면적
                            BuildingArea = buildingtb.BuildingArea, // 건축면적
                            FloorNum = buildingtb.FloorNum, // 건물층수
                            GroundFloorNum = buildingtb.GroundFloorNum, // 지상층수
                            BasementFloorNum = buildingtb.BasementFloorNum, // 지하층수
                            BuildingHeight = buildingtb.BuildingHeight, // 건물높이
                            GroundHeight = buildingtb.GroundHeight, // 건물 지상높이
                            BasementHeight = buildingtb.BasementHeight, // 건물 지상깊이
                            PackingNum = buildingtb.PackingNum, // 주차대수
                            InnerPackingNum = buildingtb.InnerPackingNum, // 옥내대수
                            OuterPackingNum = buildingtb.OuterPackingNum, // 옥외대수
                            ElecCapacity = buildingtb.ElecCapacity, // 전기용량
                            FaucetCapacity = buildingtb.FaucetCapacity, // 수전용량
                            GenerationCapacity = buildingtb.GenerationCapacity, // 발전용량
                            WaterCapacity = buildingtb.WaterCapacity, // 급수용량
                            ElevWaterTank = buildingtb.ElevWaterTank, // 고가수조
                            WaterTank = buildingtb.WaterTank, // 저수조
                            GasCapacity = buildingtb.GasCapacity, // 가스용량
                            Boiler = buildingtb.Boiler, // 보일러
                            WaterDispenser = buildingtb.WaterDispenser, // 냉온수기
                            LiftNum = buildingtb.LiftNum, // 승강대수
                            PeopleLiftNum = buildingtb.PeopleLiftNum, // 인승용
                            CargoLiftNum = buildingtb.CargoLiftNum, // 화물용
                            CoolHeatCapacity = buildingtb.CoolHeatCapacity, // 냉난방용량
                            HeatCapacity = buildingtb.HeatCapacity, // 난방용량
                            CoolCapacity = buildingtb.CoolCapacity, // 냉방용량
                            LandScapeArea = buildingtb.LandscapeArea, // 조경면적
                            GroundArea = buildingtb.GroundArea, // 지상면적
                            RooftopArea = buildingtb.RooftopArea, // 옥상면적
                            ToiletNum = buildingtb.ToiletNum, // 화장실 개수
                            MenToiletNum = buildingtb.MenToiletNum, // 남자화장실 개수
                            WomenToiletNum = buildingtb.WomenToiletNum, // 여자화장실 개수
                            FireRating = buildingtb.FireRating, // 소방등급
                            SepticTankCapacity = buildingtb.SepticTankCapacity, // 정화조 용량
                            PlaceCD = buildingtb.BuildingCd, // 사업장코드
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

                    if (dto.BasementHeight != null) // 건물 지하깊이
                        model.BasementHeight = dto.BasementHeight;

                    if (dto.PackingNum != null) // 주차대수
                        model.PackingNum = dto.PackingNum;

                    if (dto.InnerPackingNum != null) // 옥내 대수
                        model.InnerPackingNum = dto.InnerPackingNum;

                    if (dto.OuterPackingNum != null) // 옥외 대수
                        model.OuterPackingNum = dto.OuterPackingNum;

                    if (dto.ElecCapacity != null) // 전기용량
                        model.ElecCapacity = dto.ElecCapacity;

                    if (dto.FaucetCapacity != null) // 수전용량
                        model.FaucetCapacity = dto.FaucetCapacity;

                    if (dto.GenerationCapacity != null) // 발전용량
                        model.GenerationCapacity = dto.GenerationCapacity;

                    if (dto.WaterCapacity != null) // 급수용량
                        model.WaterCapacity = dto.WaterCapacity;

                    if (dto.ElevWaterTank != null) // 고가수조
                        model.ElevWaterTank = dto.ElevWaterTank;

                    if (dto.WaterTank != null) // 저수조
                        model.WaterTank = dto.WaterTank;

                    if (dto.GasCapacity != null) // 가스용량
                        model.GasCapacity = dto.GasCapacity;

                    if (dto.Boiler != null) // 보일러
                        model.Boiler = dto.Boiler;

                    if (dto.WaterDispenser != null) // 냉온수기
                        model.WaterDispenser = dto.WaterDispenser;

                    if (dto.LiftNum != null) // 승강대수
                        model.LiftNum = dto.LiftNum;

                    if (dto.PeopleLiftNum != null) // 인승용
                        model.PeopleLiftNum = dto.PeopleLiftNum;

                    if (dto.CargoLiftNum != null) // 화물용
                        model.CargoLiftNum = dto.CargoLiftNum;

                    if (dto.CoolHeatCapacity != null) // 냉 난방 용량
                        model.CoolHeatCapacity = dto.CoolHeatCapacity;

                    if (dto.HeatCapacity != null) // 난방용량
                        model.HeatCapacity = dto.HeatCapacity;

                    if (dto.CoolCapacity != null) // 냉방용량
                        model.CoolCapacity = dto.CoolCapacity;

                    if (dto.LandScapeArea != null) // 조경면적
                        model.LandscapeArea = dto.LandScapeArea;

                    if (dto.GroundArea != null) // 지상면적
                        model.GroundArea = dto.GroundArea;

                    if (dto.RooftopArea != null) // 옥상면적
                        model.RooftopArea = dto.RooftopArea;

                    if (dto.ToiletNum != null) // 화장실 개수
                        model.ToiletNum = dto.ToiletNum;

                    if (dto.MenToiletNum != null) // 남자화장실 개수
                        model.MenToiletNum = dto.MenToiletNum;

                    if (dto.WomenToiletNum != null) // 여자화장실 개수
                        model.WomenToiletNum = dto.WomenToiletNum;

                    if (!String.IsNullOrWhiteSpace(dto.FireRating)) // 소방등급
                        model.FireRating = dto.FireRating;

                    if (dto.SepticTankCapacity != null) // 정화조 용량
                        model.SepticTankCapacity = dto.SepticTankCapacity;

                    if (!String.IsNullOrWhiteSpace(dto.PlaceCD)) // 사업장 코드 - 외래키
                        model.PlacecodeCd = dto.PlaceCD;

                    model.UpdateDt = DateTime.Now;
                    model.UpdateUser = "유저TOKEN";

                    bool? result = await BuildingInfoRepository.EditBuildingInfo(model);

                    if (result == true)
                    {
                        return FuncResponseOBJ("데이터 수정 성공.", new BuildingsDTO()
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
                    else if (result == false)
                    {
                        return FuncResponseOBJ("데이터 수정 실패.", null, 404);
                    }
                    else
                    {
                        return FuncResponseOBJ("데이터가 비어있습니다.", null, 404);
                    }
                }
            }
            else
            {
                return FuncResponseOBJ("데이터가 비어있습니다.", null, 404);
            }
        }

        /// <summary>
        /// 매개변수로 넘어온 건물DTO 데이터베이스에 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<BuildingsDTO>> DeleteBuildingInfo(BuildingsDTO dto)
        {
            if(dto is not null) // 넘어온 DTO가 NULL이 아니어야 함.
            {
                BuildingsTb? model = await BuildingInfoRepository.GetBuildingInfo(dto.BuildingCode);

                if(model == null) // 없음
                {
                    return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 404);
                }
                else
                {
                    model.DelDt = DateTime.Now;
                    model.DelUser = "토큰USER";
                    model.DelYn = true;

                    bool? result = await BuildingInfoRepository.DeleteBuildingInfo(model);

                    if(result == true)
                    {
                        return FuncResponseOBJ("데이터 삭제 성공", new BuildingsDTO()
                        {
                            BuildingCode = model.BuildingCd,
                            Name = model.Name
                        }, 200);
                    }
                    else if(result == false)
                    {
                        return FuncResponseOBJ("데이터 삭제 실패", null, 404);

                    }
                    else
                    {
                        return FuncResponseOBJ("데이터가 비어있습니다.", null, 404);
                    }
                }

            }
            else
            {
                return FuncResponseOBJ("데이터가 비어있습니다.", null, 404);
            }
        }

        
      

    }
}
