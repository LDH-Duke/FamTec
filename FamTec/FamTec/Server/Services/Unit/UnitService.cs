using FamTec.Server.Repository.Unit;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.Unit;

namespace FamTec.Server.Services.Unit
{
    public class UnitService : IUnitService
    {
        private readonly IUnitInfoRepository UnitInfoRepository;

        ResponseOBJ<UnitsDTO> Response;
        Func<string, UnitsDTO, int, ResponseModel<UnitsDTO>> FuncResponseOBJ;
        Func<string, List<UnitsDTO>, int, ResponseModel<UnitsDTO>> FuncResponseList;

        ResponseOBJ<string> strResponse;
        Func<string, string, int, ResponseModel<string>> FuncResponseSTR;

        public UnitService(IUnitInfoRepository _unitinforepository)
        {
            this.UnitInfoRepository = _unitinforepository;

            Response = new ResponseOBJ<UnitsDTO>();
            FuncResponseOBJ = Response.RESPMessage;
            FuncResponseList = Response.RESPMessageList;

            strResponse = new ResponseOBJ<string>();
            FuncResponseSTR = strResponse.RESPMessage;
        }

        /// <summary>
        /// 단위정보 추가
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="sessioninfo"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<UnitsDTO>> AddUnitService(UnitsDTO? dto, SessionInfo? sessioninfo)
        {
            try
            {
                if (dto is not null && sessioninfo is not null)
                {
                    UnitTb? model = new UnitTb
                    {
                        Unit = dto.Unit,
                        CreateDt = DateTime.Now,
                        CreateUser = sessioninfo.Name,
                        UpdateDt = DateTime.Now,
                        UpdateUser = sessioninfo.Name,
                        PlaceTbId = sessioninfo.selectPlace
                    };

                    UnitTb? result = await UnitInfoRepository.AddAsync(model);

                    if(result is not null)
                    {
                        return FuncResponseOBJ("단위정보 등록 완료", new UnitsDTO
                        {
                            Id = result.Id,
                            Unit = result.Unit,
                            PlaceCode = sessioninfo.selectPlace
                        }, 200);
                    }
                    else
                    {
                        return FuncResponseOBJ("단위정보 등록 실패", null, 200);
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
        /// 해당 사업장의 단위리스트 조회
        /// </summary>
        /// <param name="sessioninfo"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<UnitsDTO>?> GetUnitList(SessionInfo? sessioninfo)
        {
            try
            {
                if (sessioninfo is not null)
                {
                    List<UnitTb>? model = await UnitInfoRepository.GetUnitList(sessioninfo.selectPlace);

                    if(model is [_, ..])
                    {
                        return FuncResponseList("전체데이터 조회 성공", model.Select(e => new UnitsDTO
                        {
                            Id = e.Id,
                            Unit = e.Unit,
                            PlaceCode = e.PlaceTbId
                        }).ToList(), 200);
                    }
                    else
                    {
                        return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
                    }
                }
                else
                {
                    return FuncResponseOBJ("요청이 잘못되었습니다.", null, 200);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseOBJ("서버에서 요청을 처리하지 못하였습니다.", null, 200);
            }
        }

        /// <summary>
        /// 단위정보 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="sessioninfo"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<string>?> DeleteUnitService(UnitsDTO? dto, SessionInfo? sessioninfo)
        {
            try
            {
                if(dto is not null && sessioninfo is not null)
                {
                    UnitTb? model = await UnitInfoRepository.GetUnitInfo(dto.Id);

                    if(model is not null)
                    {
                        model.DelYn = 1;
                        model.DelDt = DateTime.Now;
                        model.DelUser = sessioninfo.Name;

                        bool? result = await UnitInfoRepository.DeleteUnitInfo(model);
                        if(result == true)
                        {
                            return FuncResponseSTR("데이터 삭제 완료.", "1", 200);
                        }
                        else
                        {
                            return FuncResponseSTR("데이터 삭제 실패.", null, 200);
                        }
                    }
                    else
                    {
                        return FuncResponseSTR("데이터가 존재하지 않습니다.", null, 200);
                    }
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
