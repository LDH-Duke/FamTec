using FamTec.Server.Repository.Floor;
using FamTec.Server.Repository.Place;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO.Floor;
using System.Collections.Generic;

namespace FamTec.Server.Services.Floor
{
    public class FloorService : IFloorService
    {
        private readonly IFloorInfoRepository FloorInfoRepository;

        ResponseOBJ<FloorDTO> Response;
        Func<string, FloorDTO, int, ResponseModel<FloorDTO>> FuncResponseOBJ;
        Func<string, List<FloorDTO>, int, ResponseModel<FloorDTO>> FuncResponseList;

        ResponseOBJ<string> strResponse;
        Func<string, string, int, ResponseModel<string>> FuncResponseSTR;

        public FloorService(IFloorInfoRepository _floorinforepository)
        {
            this.FloorInfoRepository = _floorinforepository;

            Response = new ResponseOBJ<FloorDTO>();
            FuncResponseOBJ = Response.RESPMessage;
            FuncResponseList = Response.RESPMessageList;

            strResponse = new ResponseOBJ<string>();
            FuncResponseSTR = strResponse.RESPMessage;

        }

        /// <summary>
        /// 층정보 추가
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<FloorDTO>?> AddFloorService(FloorDTO? dto, SessionInfo? session)
        {
            try
            {
                if (dto is not null)
                {
                    FloorTb tb = new FloorTb
                    {
                        Name = dto.Name,
                        CreateDt = DateTime.Now,
                        CreateUser = session.Name,
                        UpdateDt = DateTime.Now,
                        UpdateUser = session.Name,
                        BuildingTbId = dto.BuildingTBID
                    };

                    FloorTb? result = await FloorInfoRepository.AddAsync(tb);

                    if(result is not null)
                    {
                        return FuncResponseOBJ("층 정보 추가 완료", new FloorDTO
                        {
                            Name = result.Name,
                            BuildingTBID = result.BuildingTbId
                        }, 200);
                    }
                    else
                    {
                        return FuncResponseOBJ("층 정보 추가 실패", null, 200);
                    }
                }
                else
                {
                    return FuncResponseOBJ("요청이 잘못되었습니다.", null, 404);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseOBJ("서버에서 요청을 처리하지 못하였습니다.", null, 500);
            }
        }



        /// <summary>
        /// 건물에 속해있는 층 리스트 반환
        /// </summary>
        /// <param name="buildingtbid"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<FloorDTO>?> GetFloorListService(int? buildingtbid)
        {
            try
            {
                if(buildingtbid is not null)
                {
                    List<FloorTb>? model = await FloorInfoRepository.GetFloorList(buildingtbid);

                    if(model is [_, ..])
                    {
                        return FuncResponseList("전체데이터 조회 성공", model.Select(e => new FloorDTO
                        {
                            FloorID = e.Id,
                            Name = e.Name,
                            BuildingTBID = e.BuildingTbId
                        }).ToList(), 200);
                    }
                    else
                    {
                        return FuncResponseOBJ("데이터 조회 실패", null, 200);
                    }
                }
                else
                {
                    return FuncResponseOBJ("잘못된 요청 입니다.", null, 404);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseOBJ("서버에서 요청을 처리하지 못하였습니다.", null, 500);
            }
        }

        /// <summary>
        /// 층삭제
        /// </summary>
        /// <param name="index"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<string>?> DeleteFloorService(List<int>? index, SessionInfo? session)
        {
            try
            {
                if(index is [_, ..])
                {
                    int count = 0;

                    for (int i = 0; i < index.Count; i++)
                    {
                        FloorTb? model = await FloorInfoRepository.GetFloorInfo(index[i]);

                        if(model is not null)
                        {
                            model.DelYn = 1;
                            model.DelDt = DateTime.Now;
                            model.DelUser = session.Name;

                            bool? result = await FloorInfoRepository.DeleteFloorInfo(model);

                            if(result == true)
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
                return FuncResponseSTR("서버에서 요청을 처리하지 못하였습니다.", null, 500);
            }
        }
    }
}





