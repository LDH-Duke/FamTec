using FamTec.Server.Repository.Admin.AdminPlaces;
using FamTec.Server.Repository.Admin.AdminUser;
using FamTec.Server.Repository.Place;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO.Admin;
using FamTec.Shared.Server.DTO.Admin.Place;
using FamTec.Shared.Server.DTO.Place;
using Microsoft.IdentityModel.Abstractions;

namespace FamTec.Server.Services.Admin.Place
{
    public class AdminPlaceService : IAdminPlaceService
    {
        private readonly IPlaceInfoRepository PlaceInfoRepository;
        private readonly IAdminPlacesInfoRepository AdminPlaceInfoRepository;
        private readonly IAdminUserInfoRepository AdminUserInfoRepository;

        ResponseOBJ<PlacesDTO> Response;
        Func<string, PlacesDTO, int, ResponseModel<PlacesDTO>> FuncResponseOBJ;
        Func<string, List<PlacesDTO>, int, ResponseModel<PlacesDTO>> FuncResponseList;
        
        ResponseOBJ<AdminPlaceDTO> AdminPlaceResponse;
        Func<string, AdminPlaceDTO, int, ResponseModel<AdminPlaceDTO>> AdminPlaceResponseOBJ;
        Func<string, List<AdminPlaceDTO>, int, ResponseModel<AdminPlaceDTO>> AdminPlaceResponseList;

        ResponseOBJ<ManagerListDTO> AddManagerResponse;
        Func<string, ManagerListDTO, int, ResponseModel<ManagerListDTO>> AddManagerResponseOBJ;
        Func<string, List<ManagerListDTO>, int, ResponseModel<ManagerListDTO>> AddManagerResponseList;

        ResponseOBJ<string> strResponse;
        Func<string, string, int, ResponseModel<string>> FuncResponseSTR;

        ResponseOBJ<AddPlaceDTO> AddPlaceResponse;
        Func<string, AddPlaceDTO, int, ResponseModel<AddPlaceDTO>> AddPlaceResponseOBJ;


        public AdminPlaceService(IAdminPlacesInfoRepository _adminplaceinforepository, IPlaceInfoRepository _placeinforepository, IAdminUserInfoRepository _adminuserinforepository)
        {
            this.AdminPlaceInfoRepository = _adminplaceinforepository;
            this.PlaceInfoRepository = _placeinforepository;
            this.AdminUserInfoRepository = _adminuserinforepository;

            Response = new ResponseOBJ<PlacesDTO>();
            FuncResponseOBJ = Response.RESPMessage;
            FuncResponseList = Response.RESPMessageList;

            AdminPlaceResponse = new ResponseOBJ<AdminPlaceDTO>();
            AdminPlaceResponseOBJ = AdminPlaceResponse.RESPMessage;
            AdminPlaceResponseList = AdminPlaceResponse.RESPMessageList;

            AddManagerResponse = new ResponseOBJ<ManagerListDTO>();
            AddManagerResponseOBJ = AddManagerResponse.RESPMessage;
            AddManagerResponseList = AddManagerResponse.RESPMessageList;

            AddPlaceResponse = new ResponseOBJ<AddPlaceDTO>();
            AddPlaceResponseOBJ = AddPlaceResponse.RESPMessage;

            strResponse = new ResponseOBJ<string>();
            FuncResponseSTR = strResponse.RESPMessage;
        }

    
        /// <summary>
        /// 전체사업장 조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<ResponseModel<PlacesDTO>> GetAllWorksService()
        {
            try
            {
                List<PlaceTb>? model = await PlaceInfoRepository.GetAllList();

                if(model is [_, ..])
                {
                    return FuncResponseList("전체데이터 조회 성공", model.Select(e => new PlacesDTO
                    {
                        PlaceIndex = e.Id,
                        PlaceCd = e.PlaceCd,
                        Name = e.Name,
                        CONTRACT_NUM = e.ContractNum,
                        ContractDT = e.ContractDt,
                        CancelDT = e.CancelDt,
                        Status = e.Status
                    }).ToList(), 200);
                }
                else
                {
                    return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseOBJ("서버에서 요청을 처리하지 못하였습니다.", null, 500);
            }
        }

        /// <summary>
        /// 해당 관리자의 서비스 사업장목록 보기
        /// </summary>
        /// <param name="adminid"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<AdminPlaceDTO>> GetMyWorksService(int? adminid)
        {
            try
            {
                if (adminid is not null)
                {
                    List<AdminPlaceDTO>? model = await AdminPlaceInfoRepository.GetMyWorks(adminid);

                    if (model is [_, ..])
                    {
                        return AdminPlaceResponseList("요청이 정상처리 되었습니다.", model.Select(e => new AdminPlaceDTO
                        {
                            AdminPlaceTBID = e.AdminPlaceTBID,
                            AdminPlaceUserTBID = e.AdminPlaceUserTBID,
                            PlaceTBID = e.PlaceTBID,
                            PlaceCD = e.PlaceCD,
                            Name = e.Name,
                            ContractNum = e.ContractNum,
                            ContractDT = e.ContractDT,
                            CancelDT = e.CancelDT,
                            status = e.status
                        }).ToList(), 200);
                    }
                    else
                    {
                        return AdminPlaceResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
                    }
                }
                else
                {
                    return AdminPlaceResponseOBJ("요청이 잘못되었습니다.", null, 404);
                }
            }
            catch (Exception ex)
            {
                return AdminPlaceResponseOBJ("서버에서 요청을 처리하지 못하였습니다.", null, 500);
            }
        }

        /// <summary>
        /// 전체 관리자 리스트 반환
        /// </summary>
        /// <returns></returns>
        public async ValueTask<ResponseModel<ManagerListDTO>> GetAllManagerListService()
        {
            try
            {
                List<ManagerListDTO>? model = await AdminUserInfoRepository.GetAllAdminUserList();

                if(model is [_, ..])
                {
                    return AddManagerResponseList("전체데이터 조회 성공", model, 200);
                }
                else 
                {
                    return AddManagerResponseOBJ("데이터 조회 실패", null, 200);
                }
                
            }
            catch(Exception ex)
            {
                return AddManagerResponseOBJ("서버에서 요청을 처리하지 못하였습니다.", null, 500);
            }
        }

        public async ValueTask<ResponseModel<string>> AddPlaceService(AddPlaceDTO? dto, SessionInfo? sessioninfo)
        {
            try
            {
                if (dto is not null && sessioninfo is not null)
                {
                    PlaceTb? place = new PlaceTb
                    {
                        PlaceCd = dto.PlaceCd,
                        ContractNum = dto.ContractNum,
                        Name = dto.Name,
                        Note = dto.Note,
                        Address = dto.Address,
                        ContractDt = dto.ContractDT,
                        PermMachine = dto.PermMachine,
                        PermLift = dto.PermLift,
                        PermFire = dto.PermFire,
                        PermConstruct = dto.PermConstruct,
                        PermNetwrok = dto.PermNetwork,
                        PermBeauty = dto.PermBeauty,
                        PermSecurity = dto.PermSecurity,
                        PermMaterial = dto.PermMaterial,
                        PermEnergy = dto.PermEnergy,
                        CancelDt = dto.CancelDT,
                        Status = dto.Status,
                        CreateDt = DateTime.Now,
                        CreateUser = sessioninfo.Name,
                        UpdateDt = DateTime.Now,
                        UpdateUser = sessioninfo.Name
                    };

                    PlaceTb? place_result = await PlaceInfoRepository.AddPlaceInfo(place);

                    int count = 0;

                    for (int i = 0; i < dto.AdminList.Count; i++)
                    {
                        AdminPlaceTb? model = new AdminPlaceTb
                        {
                            CreateUser = sessioninfo.Name,
                            CreateDt = DateTime.Now,
                            UpdateUser = sessioninfo.Name,
                            UpdateDt = DateTime.Now,
                            AdminTbId = dto.AdminList[i].AdminID,
                            PlaceId = place_result!.Id
                        };

                        AdminPlaceTb? result = await AdminPlaceInfoRepository.AddAsync(model);
                        if (result is not null)
                        {
                            count++;
                        }
                    }

                    return FuncResponseSTR("사업장 등록완료.", count.ToString(), 200);
                }
                else
                {
                    return FuncResponseSTR("잘못된 요청입니다.", null, 404);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseSTR("서버에서 요청을 처리하지 못하였습니다.", null, 500);
            }
        }

        public async ValueTask<ResponseModel<AddPlaceDTO>?> GetPlaceService(int? placeid)
        {
            try
            {
                if (placeid is not null)
                {
                    AddPlaceDTO? model = await AdminPlaceInfoRepository.GetWorksInfo(7);

                    if (model is not null)
                        return AddPlaceResponseOBJ("데이터 조회 성공", model, 200);
                    else
                        return AddPlaceResponseOBJ("데이터 조회 실패", null, 200);
                }
                else
                {
                    return AddPlaceResponseOBJ("요청이 잘못되었습니다", null, 404);
                }
            }
            catch(Exception ex)
            {
                return AddPlaceResponseOBJ("서버에서 요청을 처리하지 못하였습니다.", null, 500);
            }
        }

        /// <summary>
        /// 선택된 사업장 삭제
        /// </summary>
        /// <param name="placeidx"></param>
        /// <param name="sessioninfo"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<string>> DeletePlaceService(List<int> placeidx, SessionInfo? sessioninfo)
        {
            try
            {
                if (placeidx is [_, ..] && sessioninfo is not null)
                {
                    bool? result = await AdminPlaceInfoRepository.DeleteMyWorks(placeidx, sessioninfo.Name);

                    if (result == true)
                    {
                        return FuncResponseSTR("데이터 삭제 성공", null, 200);
                    }
                    else
                    {
                        return FuncResponseSTR("데이터 삭제 실패", null, 200);
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
