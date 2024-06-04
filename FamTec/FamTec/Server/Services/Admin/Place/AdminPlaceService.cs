using FamTec.Client.Pages.Admin.Manager.ManagerMain;
using FamTec.Server.Repository.Admin.AdminPlaces;
using FamTec.Server.Repository.Admin.AdminUser;
using FamTec.Server.Repository.Place;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO;
using FamTec.Shared.Server.DTO.Admin;
using FamTec.Shared.Server.DTO.Admin.Place;
using FamTec.Shared.Server.DTO.Place;
using Microsoft.EntityFrameworkCore.Infrastructure;
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

        ResponseOBJ<bool> boolResponse;
        Func<string, bool, int, ResponseModel<bool>> FuncResponseBool;


        public AdminPlaceService(IAdminPlacesInfoRepository _adminplaceinforepository, IPlaceInfoRepository _placeinforepository, IAdminUserInfoRepository _adminuserinforepository)
        {
            this.AdminPlaceInfoRepository = _adminplaceinforepository;
            this.PlaceInfoRepository = _placeinforepository;
            this.AdminUserInfoRepository = _adminuserinforepository;

            Response = new ResponseOBJ<PlacesDTO>();
            FuncResponseOBJ = Response.RESPMessage;
            FuncResponseList = Response.RESPMessageList;

            boolResponse = new ResponseOBJ<bool>();
            FuncResponseBool = boolResponse.RESPMessage;

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
        public async ValueTask<ResponseList<AllPlaceDTO>?> GetAllWorksService()
        {
            try
            {
                List<PlaceTb>? model = await PlaceInfoRepository.GetAllList();

                if(model is [_ , ..])
                {
                    return new ResponseList<AllPlaceDTO>()
                    {
                        message = "요청이 정상 처리되었습니다.",
                        data = model.Select(e => new AllPlaceDTO
                        {
                            Id = e.Id,
                            PlaceCd = e.PlaceCd,
                            Name = e.Name,
                            Note = e.Note,
                            ContractNum = e.ContractNum,
                            ContractDt = e.ContractDt,
                            Status = e.Status
                        }).ToList(),
                        code = 200
                    };
                }
                else
                {
                    return new ResponseList<AllPlaceDTO>() { message = "요청이 처리되지 않았습니다.", data = new List<AllPlaceDTO>(), code = 404 };
                }
            }
            catch(Exception ex)
            {
                return new ResponseList<AllPlaceDTO>() { message = "서버에서 요청을 처리하지 못하였습니다.", data = new List<AllPlaceDTO>(), code = 500 };
                
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
        public async ValueTask<ResponseList<ManagerListDTO>?> GetAllManagerListService()
        {
            try
            {
                List<ManagerListDTO>? model = await AdminUserInfoRepository.GetAllAdminUserList();

                if(model is [_, ..])
                {
                    return new ResponseList<ManagerListDTO> { message = "데이터가 정상 처리되었습니다.", data = model, code = 200 };
                }
                else 
                {
                    return new ResponseList<ManagerListDTO> { message = "데이터가 처리되지 않았습니다.", data = new List<ManagerListDTO>(), code = 404 };
                }
            }
            catch(Exception ex)
            {
                return new ResponseList<ManagerListDTO> { message = "서버에서 요청을 처리하지 못하였습니다.", data = new List<ManagerListDTO>(), code = 500 };
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
        public async ValueTask<ResponseUnit<PlaceDetailDTO>?> GetPlaceService(int? placeid)
        {
            try
            {
                if (placeid is not null)
                {
                    PlaceDetailDTO? model = await AdminPlaceInfoRepository.GetWorksInfo(placeid);

                    if (model is not null)
                        return new ResponseUnit<PlaceDetailDTO> { message = "요청이 정상 처리되었습니다.", data = model, code = 200};
                    else
                        return new ResponseUnit<PlaceDetailDTO> { message = "요청이 처리되지 않았습니다.", data = new PlaceDetailDTO(), code = 404 };
                }
                else
                {
                    return new ResponseUnit<PlaceDetailDTO> { message = "요청이 처리되지 않았습니다.", data = new PlaceDetailDTO(), code = 404 };
                }
            }
            catch(Exception ex)
            {
                return new ResponseUnit<PlaceDetailDTO> { message = "서버에서 요청을 처리하지 못하였습니다.", data = new PlaceDetailDTO(), code = 500 };
            }
        }

       

        /// <summary>
        /// 관리자에 사업장 추가
        /// </summary>
        /// <param name="placemanager"></param>
        /// <returns></returns>
        public async ValueTask<object> AddPlaceManagerService(AddPlaceManagerDTO<ManagerListDTO> placemanager)
        {
            try
            {
                /*
                if(placemanager.PlaceId is not null)
                {
                    int placeid = placemanager.PlaceId;
                    
                }
                else
                {

                }
                */

                /*
                if(placemanager is null)
                {
                    return new { data = "", code = 404 };
                }

                // 사업장 
                int placeid = placemanager.PlaceId;
                List<ManagerListDTO> placeManagers = placemanager.PlaceManager;

                if (placeManagers is not [_, ..])
                {
                    return FuncResponseSTR("요청이 잘못되었습니다.", null, 404);
                }

                List<AdminPlaceTb> adminplace = new List<AdminPlaceTb>();

                foreach (var manager in placeManagers)
                {
                    adminplace.Add(new AdminPlaceTb
                    {
                        AdminTbId = manager.Id,
                        PlaceId = placeid
                    });
                }

                if (adminplace is not [_, ..])
                {
                    return FuncResponseSTR("요청이 잘못되었습니다.", null, 404);
                }

              

                bool? result = await AdminPlaceInfoRepository.AddAsync(adminplace);

                if (result == true)
                {
                    return new { data = result, code = 200 };
                    //return FuncResponseSTR("관리자 추가 완료", null, 200);
                }
                else if (result == false)
                {
                    return new { data= "", code = 401};
                }
                else
                {
                    return new { data = "", code = 401 };
                }
                */
                return null;
            }
            catch(Exception ex)
            {
                return FuncResponseSTR("서버에서 요청을 처리하지 못하였습니다.", null, 500);
            }

        }

        /// <summary>
        /// 관리자에 사업장 추가
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseUnit<bool>?> AddManagerPlaceSerivce(AddManagerPlaceDTO? dto)
        {
            try
            {
                if(dto is not null)
                {
                    if (dto.PlaceList is [_, ..])
                    {
                        List<AdminPlaceTb?> placeadmintb = new List<AdminPlaceTb?>();
                        for (int i = 0; i < dto.PlaceList.Count(); i++)
                        {
                            PlaceTb? placetb = await PlaceInfoRepository.GetByPlaceInfo(dto.PlaceList[i]);

                            if (placetb is not null)
                            {
                                placeadmintb.Add(new AdminPlaceTb
                                {
                                    AdminTbId = dto.AdminID,
                                    PlaceId = dto.PlaceList[i]
                                });
                            }
                        }

                        bool? result = await AdminPlaceInfoRepository.AddAsync(placeadmintb);

                        if(result == true)
                        {
                            return new ResponseUnit<bool>() { message = "요청이 정상 처리되었습니다.", data = true, code = 200 };
                            //return FuncResponseBool("사업장 등록 성공", true, 200);
                        }
                        else if(result == false)
                        {
                            return new ResponseUnit<bool>() { message = "요청이 처리되지 않았습니다.", data = false, code = 404 };
                        }
                        else
                        {
                            return new ResponseUnit<bool>() { message = "요청이 처리되지 않았습니다.", data = false, code = 404 };
                        }
                    }
                    else
                    {
                        return new ResponseUnit<bool>() { message = "요청이 잘못되었습니다.", data = false, code = 404 };
                    }  
                }
                else
                {
                    return new ResponseUnit<bool>() { message = "요청이 잘못되었습니다.", data = false, code = 404 };
                }
            }
            catch(Exception ex)
            {
                return new ResponseUnit<bool>() { message = "서버에서 요청을 처리하지 못하였습니다.", data = false, code = 500 };
            }
        }

      
    }
}
