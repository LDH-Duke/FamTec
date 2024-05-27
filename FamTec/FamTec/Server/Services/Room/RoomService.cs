using FamTec.Server.Repository.Building;
using FamTec.Server.Repository.Floor;
using FamTec.Server.Repository.Place;
using FamTec.Server.Repository.Room;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO.Building;
using FamTec.Shared.Server.DTO.Floor;
using FamTec.Shared.Server.DTO.Room;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FamTec.Server.Services.Room
{
    public class RoomService : IRoomService
    {
        private readonly IRoomInfoRepository RoomInfoRepository;
        private readonly IBuildingInfoRepository BuildingInfoRepository;
        private readonly IFloorInfoRepository FloorInfoRepository;

        ResponseOBJ<RoomDTO> Response;
        Func<string, RoomDTO, int, ResponseModel<RoomDTO>> FuncResponseOBJ;
        Func<string, List<RoomDTO>, int, ResponseModel<RoomDTO>> FuncResponseList;


        ResponseOBJ<RoomManagementDTO> ResponseManage;
        Func<string, RoomManagementDTO, int, ResponseModel<RoomManagementDTO>> FuncResponseManage;
        Func<string, List<RoomManagementDTO>, int, ResponseModel<RoomManagementDTO>> FuncResponseManageList;

        ResponseOBJ<BuildingsDTO> ResponseBuilding;
        Func<string, BuildingsDTO, int, ResponseModel<BuildingsDTO>> FuncResponseBuilding;
        Func<string, List<BuildingsDTO>, int, ResponseModel<BuildingsDTO>> FuncResponseBuildingList;

        ResponseOBJ<FloorDTO> ResponseFloor;
        Func<string, FloorDTO, int, ResponseModel<FloorDTO>> FuncResponseFloor;
        Func<string, List<FloorDTO>, int, ResponseModel<FloorDTO>> FuncResponseFloorList;

        ResponseOBJ<string> strResponse;
        Func<string, string, int, ResponseModel<string>> FuncResponseSTR;

        public RoomService(IRoomInfoRepository _roominforepository, IBuildingInfoRepository _buildinginforepository, IFloorInfoRepository _floorinforepository)
        {
            this.RoomInfoRepository = _roominforepository;
            this.BuildingInfoRepository = _buildinginforepository;
            this.FloorInfoRepository = _floorinforepository;

            Response = new ResponseOBJ<RoomDTO>();
            FuncResponseOBJ = Response.RESPMessage;
            FuncResponseList = Response.RESPMessageList;

            ResponseManage = new ResponseOBJ<RoomManagementDTO>();
            FuncResponseManage = ResponseManage.RESPMessage;
            FuncResponseManageList = ResponseManage.RESPMessageList;

            ResponseBuilding = new ResponseOBJ<BuildingsDTO>();
            FuncResponseBuilding = ResponseBuilding.RESPMessage;
            FuncResponseBuildingList = ResponseBuilding.RESPMessageList;

            ResponseFloor = new ResponseOBJ<FloorDTO>();
            FuncResponseFloor = ResponseFloor.RESPMessage;
            FuncResponseFloorList = ResponseFloor.RESPMessageList;

            strResponse = new ResponseOBJ<string>();
            FuncResponseSTR = strResponse.RESPMessage;
        }

        public async ValueTask<ResponseModel<RoomDTO>> AddRoomService(RoomDTO? dto, SessionInfo sessioninfo)
        {
            try
            {
                if(dto is not null && sessioninfo is not null)
                {
                    RoomTb? model = new RoomTb
                    {
                        Name = dto.Name,
                        CreateDt = DateTime.Now,
                        CreateUser = sessioninfo.Name,
                        UpdateDt = DateTime.Now,
                        UpdateUser = sessioninfo.Name,
                        FloorTbId = dto.FloorTBID
                    };

                    RoomTb? result = await RoomInfoRepository.AddAsync(model);

                    if(result is not null)
                    {
                        return FuncResponseOBJ("공간 등록 완료", new RoomDTO
                        {
                            Name = result.Name,
                            FloorTBID = result.FloorTbId
                        }, 200);
                    }
                    else
                    {
                        return FuncResponseOBJ("공간 등록 실패", null, 200);
                    }
                }
                else
                {
                    return FuncResponseOBJ("요청이 잘못되었습니다.", null, 404);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseOBJ("서버에서 요청을 처리하지 못하였습니다.", null, 404);
            }
        }



        public async ValueTask<ResponseModel<RoomManagementDTO>> GetRoomListService(SessionInfo sessioninfo)
        {
            try
            {
                if(sessioninfo is not null)
                {
                    List<BuildingTb>? buildingtb = await BuildingInfoRepository.GetAllBuildingList(sessioninfo.selectPlace);

                    if (buildingtb is [_, ..])
                    {
                        List<FloorTb>? floortb = await FloorInfoRepository.GetFloorList(buildingtb!);

                        if (floortb is [_, ..])
                        {
                            List<RoomTb>? roomtb = await RoomInfoRepository.GetRoomList(floortb!);

                            if(roomtb is [_, ..])
                            {
                                List<RoomManagementDTO> dto = (from btd in buildingtb
                                                               join ftb in floortb
                                                               on btd.Id equals ftb.BuildingTbId
                                                               join rtb in roomtb
                                                               on ftb.Id equals rtb.FloorTbId
                                                               where btd.DelYn != 1 && ftb.DelYn != 1 && rtb.DelYn != 1
                                                               select new RoomManagementDTO
                                                               {
                                                                   RoomId = rtb.Id,
                                                                   RoomName = rtb.Name,
                                                                   FloorName = ftb.Name,
                                                                   BuilidingName = btd.Name,
                                                                   RoomCreateDT = rtb.CreateDt
                                                               }).ToList();


                                return FuncResponseManageList("전체데이터 조회 성공", dto, 200);

                            }
                            else
                            {
                                return FuncResponseManage("공간 정보가 없습니다.", null, 200);
                            }
                        }
                        else
                        {
                            return FuncResponseManage("층 정보가 없습니다.", null, 200);
                        }
                    }
                    else
                    {
                        return FuncResponseManage("건물 정보가 없습니다.", null, 200);
                    }
                }
                else
                {
                    return FuncResponseManage("요청이 잘못되었습니다.", null, 404);
                }

            }catch(Exception ex)
            {
                return FuncResponseManage("서버에서 요청을 처리하지 못하였습니다.", null, 500);
            }
        }

        public async ValueTask<ResponseModel<BuildingsDTO>> GetBuildingList(SessionInfo sessionsinfo)
        {
            try
            {
                if (sessionsinfo is not null)
                {
                    List<BuildingTb>? model = await BuildingInfoRepository.GetAllBuildingList(sessionsinfo.selectPlace);

                    if (model is [_, ..])
                    {
                        return FuncResponseBuildingList("전체데이터 조회 성공", model.Select(e => new BuildingsDTO
                        {
                            BuildingID = e.Id,
                            BuildingCode = e.BuildingCd,
                            Name = e.Name,
                            Address = e.Address,
                            Tel = e.Tel,
                            Usage = e.Usage,
                            ConstComp = e.ConstComp,
                            CompletionDt = e.CompletionDt,
                            BuildingStruct = e.BuildingStruct,
                            RoofStruct = e.RoofStruct,
                            GrossFloorArea = e.GrossFloorArea,
                            LandArea = e.LandArea,
                            BuildingArea = e.BuildingArea,
                            FloorNum = e.FloorNum,
                            GroundFloorNum = e.GroundFloorNum,
                            BasementFloorNum = e.BasementFloorNum,
                            BuildingHeight = e.BuildingHeight,
                            GroundHeight = e.GroundHeight,
                            BasementHeight = e.BasementHeight,
                            PackingNum = e.ParkingNum,
                            InnerPackingNum = e.InnerParkingNum,
                            OuterPackingNum = e.OuterParkingNum,
                            ElecCapacity = e.ElecCapacity,
                            FaucetCapacity = e.FaucetCapacity,
                            GenerationCapacity = e.GenerationCapacity,
                            WaterCapacity = e.WaterCapacity,
                            ElevWaterCapacity = e.ElevWaterCapacity,
                            WaterTank = e.WaterTank,
                            GasCapacity = e.GasCapacity,
                            Boiler = e.Boiler,
                            WaterDispenser = e.WaterDispenser,
                            LiftNum = e.LiftNum,
                            PeopleLiftNum = e.PeopleLiftNum,
                            CargoLiftNum = e.CargoLiftNum,
                            CoolHeatCapacity = e.CoolHeatCapacity,
                            HeatCapacity = e.HeatCapacity,
                            CoolCapacity = e.CoolCapacity,
                            LandScapeArea = e.LandscapeArea,
                            GroundArea = e.GroundArea,
                            RooftopArea = e.RooftopArea,
                            ToiletNum = e.ToiletNum,
                            MenToiletNum = e.MenToiletNum,
                            WomenToiletNum = e.WomenToiletNum,
                            FireRating = e.FireRating,
                            SepticTankCapacity = e.SepticTankCapacity,
                            CreateDT = e.CreateDt,
                            PlaceIdx = e.PlaceTbId
                        }).ToList(), 200);
                    }
                    else
                    {
                        return FuncResponseBuilding("데이터가 존재하지 않습니다.", null, 200);
                    }
                }
                else
                {
                    return FuncResponseBuilding("요청이 잘못되었습니다.", null, 200);
                }
            }catch(Exception ex)
            {
                return FuncResponseBuilding("서버에서 요청을 처리하지 못하였습니다.", null, 500);
            }
        }

        public async ValueTask<ResponseModel<FloorDTO>> GetFloorList(int? buildingidx)
        {
            try
            {
                if (buildingidx is not null)
                {
                    List<FloorTb>? model = await FloorInfoRepository.GetFloorList(buildingidx);
                    if (model is [_, ..])
                    {
                        return FuncResponseFloorList("전체데이터 조회 성공", model.Select(e => new FloorDTO
                        {
                            FloorID = e.Id,
                            Name = e.Name,
                            BuildingTBID = e.BuildingTbId
                        }).ToList(), 200);
                    }
                    else
                    {
                        return FuncResponseFloor("데이터가 존재하지 않습니다.", null, 200);
                    }
                }
                else
                {
                    return FuncResponseFloor("요청이 잘못되었습니다.", null, 200);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseFloor("서버에서 요청을 처리하지 못하였습니다.", null, 200);
            }

        }

        public async ValueTask<ResponseModel<string>?> DeleteRoomService(List<int>? index, SessionInfo? session)
        {
            try
            {
                if(index is [_, ..])
                {
                    int count = 0;

                    for (int i = 0; i < index.Count; i++)
                    {
                        RoomTb? model = await RoomInfoRepository.GetRoomInfo(index[i]);

                        if(model is not null)
                        {
                            model.DelYn = 1;
                            model.DelDt = DateTime.Now;
                            model.DelUser = session.Name;

                            bool? result = await RoomInfoRepository.DeleteRoomInfo(model);

                            if(result == true)
                            {
                                count++;
                            }
                        }
                    }
                    return FuncResponseSTR("데이터 삭제완료", count.ToString(), 200);
                }
                else
                {
                    return FuncResponseSTR("요청이 잘못되었습니다.", null, 404);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseSTR("서버에서 요청을 처리하지 못하였습니다.", null, 500);
            }
        }
    }
}
