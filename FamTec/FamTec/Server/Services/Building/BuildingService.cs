using FamTec.Server.Repository.Building;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO.Building;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;

namespace FamTec.Server.Services.Building
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingInfoRepository BuildingRepository;

        ResponseOBJ<BuildingsDTO> Response;
        Func<string, BuildingsDTO, int, ResponseModel<BuildingsDTO>> FuncResponseOBJ;
        Func<string, List<BuildingsDTO>, int, ResponseModel<BuildingsDTO>> FuncResponseList;

        ResponseOBJ<string> strResponse;
        Func<string, string, int, ResponseModel<string>> FuncResponseSTR;
        

        public BuildingService(IBuildingInfoRepository _buildingrepository)
        {
            this.BuildingRepository = _buildingrepository;

            Response = new ResponseOBJ<BuildingsDTO>();
            FuncResponseOBJ = Response.RESPMessage;
            FuncResponseList = Response.RESPMessageList;

            strResponse = new ResponseOBJ<string>();
            FuncResponseSTR = strResponse.RESPMessage;
        }

        public async ValueTask<ResponseModel<BuildingsDTO>?> AddBuildingService(BuildingsDTO? dto, SessionInfo? sessioninfo)
        {
            try
            {
                if(dto is not null && sessioninfo is not null)
                {
                    BuildingTb? model = new BuildingTb
                    {
                        BuildingCd = dto.BuildingCode,
                        Name = dto.Name,
                        Address = dto.Address,
                        Tel = dto.Tel,
                        Usage = dto.Usage,
                        ConstComp = dto.ConstComp,
                        CompletionDt = dto.CompletionDt,
                        BuildingStruct = dto.BuildingStruct,
                        RoofStruct = dto.RoofStruct,
                        GrossFloorArea = dto.GrossFloorArea,
                        LandArea = dto.LandArea,
                        BuildingArea = dto.BuildingArea,
                        FloorNum = dto.FloorNum,
                        GroundFloorNum = dto.GroundFloorNum,
                        BasementFloorNum = dto.BasementFloorNum,
                        BuildingHeight = dto.BuildingHeight,
                        GroundHeight = dto.GroundHeight,
                        BasementHeight = dto.BasementHeight,
                        ParkingNum = dto.PackingNum,
                        InnerParkingNum = dto.InnerPackingNum,
                        OuterParkingNum = dto.OuterPackingNum,
                        ElecCapacity = dto.ElecCapacity,
                        FaucetCapacity = dto.FaucetCapacity,
                        GenerationCapacity = dto.GenerationCapacity,
                        WaterCapacity = dto.WaterCapacity,
                        ElevWaterCapacity = dto.ElevWaterCapacity,
                        WaterTank = dto.WaterTank,
                        GasCapacity = dto.GasCapacity,
                        Boiler = dto.Boiler,
                        WaterDispenser = dto.WaterDispenser,
                        LiftNum = dto.LiftNum,
                        PeopleLiftNum = dto.PeopleLiftNum,
                        CargoLiftNum = dto.CargoLiftNum,
                        HeatCapacity = dto.HeatCapacity,
                        CoolCapacity = dto.CoolCapacity,
                        LandscapeArea = dto.LandScapeArea,
                        GroundArea = dto.GroundArea,
                        RooftopArea = dto.RooftopArea,
                        ToiletNum = dto.ToiletNum,
                        MenToiletNum = dto.MenToiletNum,
                        WomenToiletNum = dto.WomenToiletNum,
                        FireRating = dto.FireRating,
                        SepticTankCapacity = dto.SepticTankCapacity,
                        CreateDt = DateTime.Now,
                        CreateUser = sessioninfo.Name,
                        UpdateDt = DateTime.Now,
                        UpdateUser = sessioninfo.Name,
                        PlaceTbId = sessioninfo.selectPlace
                    };

                    BuildingTb? result = await BuildingRepository.AddAsync(model);
                    if(result is not null)
                    {
                        return FuncResponseOBJ("건물 등록 완료", new BuildingsDTO
                        {
                            BuildingCode = result.BuildingCd,
                            Name = result.Name,
                            Address = result.Address,
                            Tel = result.Tel,
                            Usage = result.Usage,
                            ConstComp = result.ConstComp,
                            CompletionDt = result.CompletionDt,
                            BuildingStruct = result.BuildingStruct,
                            RoofStruct = result.RoofStruct,
                            GrossFloorArea = result.GrossFloorArea,
                            LandArea = result.LandArea,
                            BuildingArea = result.BuildingArea,
                            FloorNum = result.FloorNum,
                            GroundFloorNum = result.GroundFloorNum,
                            BasementFloorNum = result.BasementFloorNum,
                            BuildingHeight = result.BuildingHeight,
                            GroundHeight = result.GroundHeight,
                            PackingNum = result.ParkingNum,
                            InnerPackingNum = result.InnerParkingNum,
                            OuterPackingNum = result.OuterParkingNum,
                            ElecCapacity = result.ElecCapacity,
                            FaucetCapacity= result.FaucetCapacity,
                            GenerationCapacity = result.GenerationCapacity,
                            WaterCapacity = result.WaterCapacity,
                            ElevWaterCapacity = result.ElevWaterCapacity,
                            WaterTank = result.WaterTank,
                            GasCapacity = result.GasCapacity,
                            Boiler = result.Boiler,
                            WaterDispenser = result.WaterDispenser,
                            LiftNum = result.LiftNum,
                            PeopleLiftNum = result.PeopleLiftNum,
                            CargoLiftNum = result.CargoLiftNum,
                            CoolHeatCapacity = result.CoolHeatCapacity,
                            HeatCapacity = result.HeatCapacity,
                            CoolCapacity = result.CoolCapacity,
                            LandScapeArea = result.LandscapeArea,
                            GroundArea = result.GroundArea,
                            RooftopArea = result.RooftopArea,
                            ToiletNum = result.ToiletNum,
                            MenToiletNum = result.MenToiletNum,
                            WomenToiletNum = result.WomenToiletNum,
                            FireRating = result.FireRating,
                            SepticTankCapacity = result.SepticTankCapacity,
                            PlaceIdx = result.PlaceTbId
                        }, 200);
                    }
                    else
                    {
                        return FuncResponseOBJ("건물 등록 실패", null, 200);
                    }
                }
                else
                {
                    return FuncResponseOBJ("요청이 잘못되었습니다", null, 404);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseOBJ("서버에서 요청을 처리하지 못하였습니다.", null, 500);
            }
        }

        /// <summary>
        /// 사업장에 등록되어있는 건물리스트 출력
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<BuildingsDTO>?> GetBuilidngListService(SessionInfo? session)
        {
            try
            {
                List<BuildingTb>? model = await BuildingRepository.GetAllBuildingList(session.selectPlace);

                if(model is [_, ..])
                {
                    return FuncResponseList("전체데이터 조회 성공", model.Select(e => new BuildingsDTO
                    {
                        BuildingID = e.Id,
                        BuildingCode = e.BuildingCd,
                        Name = e.Name,
                        Address = e.Address,
                        FloorNum = e.FloorNum,
                        CreateDT = e.CreateDt
                    }).ToList(), 200);
                }
                else
                {
                    return FuncResponseOBJ("건물데이터가 존재하지 않습니다.", null, 200);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseOBJ("서버에서 요청을 처리하지 못하였습니다.", null, 500);
            }
        }

        /// <summary>
        /// 건물삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<string>?> DeleteBuildingService(List<int>? index, SessionInfo? session)
        {
            try
            {
                if (index is [_, ..])
                {
                    int count = 0;

                    for(int i = 0; i < index.Count; i++)
                    {
                        BuildingTb? model = await BuildingRepository.GetBuildingInfo(index[i]);
                        
                        if (model is not null)
                        {
                            model.DelYn = 1;
                            model.DelDt = DateTime.Now;
                            model.DelUser = session.Name;

                            bool? result = await BuildingRepository.DeleteBuildingInfo(model);

                            if (result == true)
                            {
                                count++;
                            }
                        }
                    }

                    return FuncResponseSTR("데이터 삭제 완료", count.ToString(), 200);
                }
                else
                {
                    return FuncResponseSTR("잘못된 요청 입니다.", null, 404);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseSTR("서버에서 요청을 처리하지 못하였습니다",null, 200);
            }
        }

    }
}
