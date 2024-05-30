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
                //if (model is [_, ..])
                //{
                //    return FuncResponseAllList("전체데이터 조회 성공", model.Select(e => new AllPlaceDTO
                //    {
                //        Id = e.Id,
                //        PlaceCd = e.PlaceCd,
                //        Name = e.Name,
                //        Note = e.Note,
                //        ContractNum = e.ContractNum,
                //        ContractDt = e.ContractDt,
                //        Status = e.Status
                //    }).ToList(), 200);
                //}
                //else
                //{
                //    return FuncResponseAll("데이터가 존재하지 않습니다.", null, 200);
                //}
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

        public async ValueTask<ResponseModel<AddPlaceDTO>> AddPlaceService(AddPlaceDTO? dto, SessionInfo? sessioninfo)
        {
            try
            {
                if (dto is not null && sessioninfo is not null)
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
                        PermVoc = dto.PermVoc,
                        CreateDt = DateTime.Now,
                        CreateUser = sessioninfo.Name,
                        UpdateDt = DateTime.Now,
                        UpdateUser = sessioninfo.Name
                    };

                    PlaceTb? place_result = await PlaceInfoRepository.AddPlaceInfo(place);

                    if(place_result is not null)
                    {
                        return AddPlaceResponseOBJ("사업장이 등록되었습니다.", new AddPlaceDTO
                        {
                            PlaceCd = place_result.PlaceCd, // 사업장코드
                            Name = place_result.Name, // 사업장 이름
                            Tel = place_result.Tel, // 전화번호
                            Address = place_result.Address, // 주소
                            ContractNum = place_result.ContractNum, // 계약번호
                            ContractDT = place_result.ContractDt.ToString(), // 계약일자
                            PermMachine = place_result.PermMachine, // 설비메뉴 권한
                            PermLift = place_result.PermLift, // 승강메뉴 권한
                            PermFire = place_result.PermFire, // 소방메뉴 권한
                            PermConstruct = place_result.PermConstruct, // 건축메뉴 권한
                            PermNetwork = place_result.PermNetwrok, // 통신메뉴 권한
                            PermBeauty = place_result.PermBeauty, // 미화권한
                            PermSecurity = place_result.PermSecurity, // 보안메뉴 권한
                            PermMaterial = place_result.PermMaterial, // 자재메뉴 권한
                            PermEnergy = place_result.PermEnergy, // 에너지메뉴 권한
                            PermVoc = place_result.PermVoc // VOC 메뉴 권한
                        }, 200);
                    }
                    else
                    {
                        return AddPlaceResponseOBJ("사업장 등록실패", null, 200);
                    }
                }
                else
                {
                    return AddPlaceResponseOBJ("잘못된 요청입니다.", null, 404);
                }
            }
            catch(Exception ex)
            {
                return AddPlaceResponseOBJ("서버에서 요청을 처리하지 못하였습니다.", null, 500);
            }
        }

        public async ValueTask<ResponseModel<AddPlaceDTO>> AddPlaceAdminService(AddPlaceDTO? dto, SessionInfo? sessioninfo)
        {
            /*
                try
                {
                    int count = 0;

                    List<AdminPlaceTb> adminplacetb = new List<AdminPlaceTb>();

                    for (int i = 0; i < dto.AdminList.Count; i++)
                    {
                        adminplacetb.Add(new AdminPlaceTb
                        {
                            CreateDt = DateTime.Now,
                            CreateUser = sessioninfo.Name,
                            UpdateDt = DateTime.Now,
                            UpdateUser = sessioninfo.Name,
                            AdminTbId = dto.AdminList[i].AdminID,
                            PlaceId = dto!.ID
                        });
                    }

                    bool? result = await AdminPlaceInfoRepository.AddAsync(adminplacetb);

                    if (result == true)
                    {
                        AddPlaceDTO? resultdto = new AddPlaceDTO();
                        resultdto.ID = dto.ID;
                        resultdto.PlaceCd = dto.PlaceCd;
                        resultdto.ContractNum = dto.ContractNum;
                        resultdto.Name = dto.Name;
                        resultdto.Note = dto.Note;
                        resultdto.Address = dto.Address;
                        resultdto.ContractDT = dto.ContractDT;
                        resultdto.PermMachine = dto.PermMachine;
                        resultdto.PermLift = dto.PermLift;
                        resultdto.PermFire = dto.PermFire;
                        resultdto.PermConstruct = dto.PermConstruct;
                        resultdto.PermNetwork = dto.PermNetwork;
                        resultdto.PermBeauty = dto.PermBeauty;
                        resultdto.PermSecurity = dto.PermSecurity;
                        resultdto.PermMaterial = dto.PermMaterial;
                        resultdto.PermEnergy = dto.PermEnergy;
                        resultdto.CancelDT = dto.CancelDT;
                        resultdto.Status = dto.Status;


                        for(int i=0;i< adminplacetb.Count; i++)
                        {
                            resultdto.AdminList.Add(new ManagerListDTO
                            {
                                UserId = dto.AdminList[i].UserId,
                                UserName = dto.AdminList[i].UserName,
                            });
                        }

                        return AddPlaceResponseOBJ("사업장의 관리자가 등록되었습니다.", resultdto, 200);
                    }
                    else if (result == false)
                    {

                    }
                    else
                    {

                    }
                }
                catch(Exception ex)
                {
                }
            */
            return null;
        }

        public async ValueTask<ResponseModel<AddPlaceDTO>?> GetPlaceService(int? placeid)
        {
            try
            {
                if (placeid is not null)
                {
                    AddPlaceDTO? model = await AdminPlaceInfoRepository.GetWorksInfo(placeid);

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
