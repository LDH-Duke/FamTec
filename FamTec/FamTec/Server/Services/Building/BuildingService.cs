﻿using FamTec.Server.Repository.Building;
using FamTec.Shared;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO;
using FamTec.Shared.Server.DTO.Building;

namespace FamTec.Server.Services.Building
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingInfoRepository BuildingRepository;
        private ILogService LogService;

        public BuildingService(IBuildingInfoRepository _buildingrepository,
            ILogService _logservice)
        {
            this.BuildingRepository = _buildingrepository;
            this.LogService = _logservice;
        }

        /// <summary>
        /// 해당 사업장에 건물추가
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="placeidx"></param>
        /// <returns></returns>
        public async ValueTask<ResponseUnit<bool>> AddBuildingService(BuildingsDTO? dto, int? placeidx )
        {
            try
            {
                if(dto is not null && placeidx is not null)
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
                        

                        // 토큰부분
                        PlaceTbId = placeidx
                    };

                    BuildingTb? buildingtb = await BuildingRepository.AddAsync(model);
                    
                    if(buildingtb is not null)
                    {
                        return new ResponseUnit<bool>() { message = "요청이 정상적으로 처리되었습니다.", data = true, code = 200 };
                    }
                    else
                    {
                        return new ResponseUnit<bool>() { message = "요청이 처리되지 않았습니다.", data = false, code = 404 };
                    }
                }
                else
                {
                    return new ResponseUnit<bool>() { message = "잘못된 요청입니다.", data = false, code = 404 };
                }
            }
            catch(Exception ex)
            {
                LogService.LogMessage(ex.ToString());
                return new ResponseUnit<bool>() { message = "서버에서 요청을 처리하지 못하였습니다.", data = false, code = 500 };
            }
        }

        /// <summary>
        /// 사업장에 등록되어있는 건물리스트 출력
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public async ValueTask<ResponseList<BuildingsDTO>> GetBuilidngListService(int? placeidx)
        {
            try
            {
                if (placeidx is not null)
                {
                    List<BuildingTb>? model = await BuildingRepository.GetAllBuildingList(placeidx);

                    if (model is [_, ..])
                    {
                        return new ResponseList<BuildingsDTO>()
                        {
                            message = "요청이 정상적으로 처리되었습니다.",
                            data = model.Select(e => new BuildingsDTO
                            {
                                BuildingID = e.Id,
                                BuildingCode = e.BuildingCd,
                                Name = e.Name,
                                Address = e.Address,
                                FloorNum = e.FloorNum,
                                CreateDT = e.CreateDt
                            }).ToList(),
                            code = 200
                        };
                    }
                    else
                    {
                        return new ResponseList<BuildingsDTO>() { message = "요청이 정상적으로 처리되었습니다.", data = new List<BuildingsDTO>(), code = 200 };
                    }
                }
                else
                {
                    return new ResponseList<BuildingsDTO>() { message = "요청이 잘못되었습니다.", data = new List<BuildingsDTO>(), code = 404 };
                }
            }
            catch(Exception ex)
            {
                LogService.LogMessage(ex.ToString());
                return new ResponseList<BuildingsDTO>() { message = "서버에서 요청을 처리하지 못하였습니다.", data = new List<BuildingsDTO>(), code = 500 };
            }
        }

        /// <summary>
        /// 건물삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public async ValueTask<ResponseUnit<string>?> DeleteBuildingService(List<int>? index, SessionInfo? session)
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

                    return new ResponseUnit<string>() { message = "요청이 정상 처리되었습니다.", data = count.ToString(), code = 200 };
                }
                else
                {
                    return new ResponseUnit<string>() { message = "요청이 잘못되었습니다.", data = null, code = 404 };
                }
            }
            catch(Exception ex)
            {
                LogService.LogMessage(ex.ToString());
                return new ResponseUnit<string>() { message = "서버에서 요청을 처리하지 못하였습니다.", data = null, code = 500 };
            }
        }

    }
}
