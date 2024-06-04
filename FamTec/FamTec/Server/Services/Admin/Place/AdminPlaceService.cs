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
using Microsoft.JSInterop.Infrastructure;

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

        ResponseOBJ<AllPlaceDTO> ResponseAll;
        Func<string, AllPlaceDTO, int, ResponseModel<AllPlaceDTO>> FuncResponseAll;
        Func<string, List<AllPlaceDTO>, int, ResponseModel<AllPlaceDTO>> FuncResponseAllList;

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

        ResponseOBJ<PlaceDetailDTO> PlaceDetailResponse;
        Func<string, PlaceDetailDTO, int, ResponseModel<PlaceDetailDTO>> FuncPlaceDetailOBJ;

        ResponseOBJ<int?> IntResponse;
        Func<string, int?, int, ResponseModel<int?>> FuncResponseINT;


        public AdminPlaceService(IAdminPlacesInfoRepository _adminplaceinforepository, IPlaceInfoRepository _placeinforepository, IAdminUserInfoRepository _adminuserinforepository)
        {
            this.AdminPlaceInfoRepository = _adminplaceinforepository;
            this.PlaceInfoRepository = _placeinforepository;
            this.AdminUserInfoRepository = _adminuserinforepository;

            Response = new ResponseOBJ<PlacesDTO>();
            FuncResponseOBJ = Response.RESPMessage;
            FuncResponseList = Response.RESPMessageList;

            ResponseAll = new ResponseOBJ<AllPlaceDTO>();
            FuncResponseAll = ResponseAll.RESPMessage;
            FuncResponseAllList = ResponseAll.RESPMessageList;

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

            PlaceDetailResponse = new ResponseOBJ<PlaceDetailDTO>();
            FuncPlaceDetailOBJ = PlaceDetailResponse.RESPMessage;

            IntResponse = new ResponseOBJ<int?>();
            FuncResponseINT = IntResponse.RESPMessage;
        }

    
        /// <summary>
        /// 전체사업장 조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<ResponseModel<AllPlaceDTO>> GetAllWorksService()
        {
            try
            {
                List<PlaceTb>? model = await PlaceInfoRepository.GetAllList();
                return FuncResponseAllList("전체데이터 조회 성공", model.Select(e => new AllPlaceDTO
                {
                    Id = e.Id,
                    PlaceCd = e.PlaceCd,
                    Name = e.Name,
                    Note = e.Note,
                    ContractNum = e.ContractNum,
                    ContractDt = e.ContractDt,
                    Status = e.Status
                }).ToList(), 200);
              
            }
            catch(Exception ex)
            {
                return FuncResponseAll("서버에서 요청을 처리하지 못하였습니다.", null, 500);
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

        public async ValueTask<ResponseModel<int?>> AddPlaceService(AddPlaceDTO? dto)
        {
            try
            {
                if (dto is not null)
                {
                    PlaceTb? place = new PlaceTb
                    {
                        PlaceCd = dto.PlaceCd,
                        Name = dto.Name,
                        Tel = dto.Tel,
                        Address = dto.Address,
                        ContractNum = dto.ContractNum,
                        ContractDt = Convert.ToDateTime(dto.ContractDT),
                        PermMachine = dto.PermMachine,
                        PermLift = dto.PermLift,
                        PermFire = dto.PermFire,
                        PermConstruct = dto.PermConstruct,
                        PermNetwrok = dto.PermNetwork,
                        PermBeauty = dto.PermBeauty,
                        PermSecurity = dto.PermSecurity,
                        PermMaterial = dto.PermMaterial,
                        PermEnergy = dto.PermEnergy,
                        PermVoc = dto.PermVoc
                    };

                    PlaceTb? place_result = await PlaceInfoRepository.AddPlaceInfo(place);

                    if(place_result is not null)
                    {
                        return FuncResponseINT("사업장이 등록되었습니다.", place_result.Id , 200);
                    }
                    else
                    {
                        return FuncResponseINT("사업장 등록실패", null, 200);
                    }
                }
                else
                {
                    return FuncResponseINT("잘못된 요청입니다.", null, 404);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseINT("서버에서 요청을 처리하지 못하였습니다.", null, 500);
            }
        }

      

        /// <summary>
        /// 사업장정보 상세조회
        /// </summary>
        /// <param name="placeid"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<PlaceDetailDTO>?> GetPlaceService(int? placeid)
        {
            try
            {
                if (placeid is not null)
                {
                    PlaceDetailDTO? model = await AdminPlaceInfoRepository.GetWorksInfo(placeid);

                    if (model is not null)
                        return FuncPlaceDetailOBJ("데이터 조회 성공", model, 200);
                    else
                        return FuncPlaceDetailOBJ("데이터 조회 실패", new PlaceDetailDTO(), 200);
                }
                else
                {
                    return FuncPlaceDetailOBJ("요청이 잘못되었습니다", new PlaceDetailDTO(), 404);
                }
            }
            catch(Exception ex)
            {
                return FuncPlaceDetailOBJ("서버에서 요청을 처리하지 못하였습니다.", new PlaceDetailDTO(), 500);
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

        /// <summary>
        /// 관리자에 사업장 추가
        /// </summary>
        /// <param name="placemanager"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<string>> AddPlaceManagerService(AddPlaceManagerDTO<ManagerListDTO> placemanager)
        {
            try
            {
                if (placemanager is not null)
                {
                    int placeid = placemanager.PlaceId;
                    List<ManagerListDTO> placeManagers = placemanager.PlaceManager;

                    if (placeManagers is [_, ..])
                    {
                        List<AdminPlaceTb> adminplace = new List<AdminPlaceTb>();

                        foreach (var manager in placeManagers)
                        {
                            adminplace.Add(new AdminPlaceTb
                            {
                                AdminTbId = manager.Id,
                                PlaceId = placeid
                            });
                        }

                        if (adminplace is [_, ..])
                        {
                            bool? result = await AdminPlaceInfoRepository.AddAsync(adminplace);

                            if(result == true)
                            {
                                return FuncResponseSTR("관리자 추가 완료", null, 200);
                            }
                            else if(result == false)
                            {
                                return FuncResponseSTR("관리자 추가 실패", null, 200);
                            }
                            else
                            {
                                return FuncResponseSTR("관리자 추가 실패", null, 200);
                            }
                        }
                        else
                        {
                            return FuncResponseSTR("요청이 잘못되었습니다.", null, 404);
                        }
                    }
                    else
                    {
                        return FuncResponseSTR("요청이 잘못되었습니다.", null, 404);
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
