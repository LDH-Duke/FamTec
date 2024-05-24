using FamTec.Server.Repository.Building;
using FamTec.Server.Repository.Place;
using FamTec.Server.Repository.Room;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO.Building;
using FamTec.Shared.Server.DTO.Room;

namespace FamTec.Server.Services.Room
{
    public class RoomService : IRoomService
    {
        private readonly IRoomInfoRepository RoomInfoRepository;
        private readonly IBuildingInfoRepository BuildingInfoRepository;;

        ResponseOBJ<RoomDTO> Response;
        Func<string, RoomDTO, int, ResponseModel<RoomDTO>> FuncResponseOBJ;
        Func<string, List<RoomDTO>, int, ResponseModel<RoomDTO>> FuncResponseList;

        ResponseOBJ<string> strResponse;
        Func<string, string, int, ResponseModel<string>> FuncResponseSTR;


        public RoomService(IRoomInfoRepository _roominforepository, IBuildingInfoRepository _buildinginforepository)
        {
            this.RoomInfoRepository = _roominforepository;
            this.BuildingInfoRepository = _buildinginforepository;

            Response = new ResponseOBJ<RoomDTO>();
            FuncResponseOBJ = Response.RESPMessage;
            FuncResponseList = Response.RESPMessageList;

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

        public async ValueTask<List<RoomManagementDTO>> GetRoomList(SessionInfo? sessioninfo)
        {
            try
            {
                if(sessioninfo is not null)
                {
                    List<BuildingTb>? buildingtb = await BuildingInfoRepository.GetAllBuildingList(sessioninfo.selectPlace);

                }
                else
                {

                }

            }catch(Exception ex)
            {

            }
        }
    }
}
