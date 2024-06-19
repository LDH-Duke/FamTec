using FamTec.Server.Repository.Admin.AdminPlaces;
using FamTec.Server.Repository.Admin.AdminUser;
using FamTec.Server.Repository.Place;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO;
using FamTec.Shared.Server.DTO.Admin;
using FamTec.Shared.Server.DTO.Admin.Place;
using FamTec.Shared.Server.DTO.Place;

namespace FamTec.Server.Services.Admin.Place
{
    public class AdminPlaceService : IAdminPlaceService
    {
        private readonly IPlaceInfoRepository PlaceInfoRepository;
        private readonly IAdminPlacesInfoRepository AdminPlaceInfoRepository;
        private readonly IAdminUserInfoRepository AdminUserInfoRepository;
        private ILogService LogService;

  
        public AdminPlaceService(IAdminPlacesInfoRepository _adminplaceinforepository,
            IPlaceInfoRepository _placeinforepository,
            IAdminUserInfoRepository _adminuserinforepository,
            ILogService _logservice)
        {
            this.AdminPlaceInfoRepository = _adminplaceinforepository;
            this.PlaceInfoRepository = _placeinforepository;
            this.AdminUserInfoRepository = _adminuserinforepository;
            this.LogService = _logservice;
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
                LogService.LogMessage(ex.ToString());
                return new ResponseList<AllPlaceDTO>() { message = "서버에서 요청을 처리하지 못하였습니다.", data = new List<AllPlaceDTO>(), code = 500 };
            }
        }

        /// <summary>
        /// 해당 관리자의 서비스 사업장목록 보기
        /// </summary>
        /// <param name="adminid"></param>
        /// <returns></returns>
        public async ValueTask<ResponseList<AdminPlaceDTO>> GetMyWorksService(int? adminid)
        {
            try
            {
                if (adminid is not null)
                {
                    List<AdminPlaceDTO>? model = await AdminPlaceInfoRepository.GetMyWorks(adminid);

                    if (model is [_, ..])
                    {
                        return new ResponseList<AdminPlaceDTO>()
                        {
                            message = "요청이 정상 처리되었습니다.",
                            data = model.Select(e => new AdminPlaceDTO
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
                            }).ToList(),
                            code = 200
                        };
                    }
                    else
                    {
                        return new ResponseList<AdminPlaceDTO>() { message = "데이터가 존재하지 않습니다.", data = new List<AdminPlaceDTO>(), code = 204 };
                    }
                }
                else
                {
                    return new ResponseList<AdminPlaceDTO>() { message = "요청이 잘못되었습니다.", data = new List<AdminPlaceDTO>(), code = 404 };
                }
            }
            catch (Exception ex)
            {
                LogService.LogMessage(ex.ToString());
                return new ResponseList<AdminPlaceDTO>() { message = "서버에서 요청을 처리하지 못하였습니다.", data = new List<AdminPlaceDTO>(), code = 500 };
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
                LogService.LogMessage(ex.ToString());
                return new ResponseList<ManagerListDTO> { message = "서버에서 요청을 처리하지 못하였습니다.", data = new List<ManagerListDTO>(), code = 500 };
            }
        }

        public async ValueTask<ResponseUnit<int?>> AddPlaceService(AddPlaceDTO? dto)
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
                        return new ResponseUnit<int?>{ message = "요청이 정상 처리되었습니다.", data = place_result.Id, code = 200 };
                    }
                    else
                    {
                        return new ResponseUnit<int?> { message = "요청이 처리되지 않았습니다.", data = null, code = 200 };
                    }
                }
                else
                {
                    return new ResponseUnit<int?> { message = "잘못된 요청입니다.", data = null, code = 404 };
                }
            }
            catch(Exception ex)
            {
                LogService.LogMessage(ex.ToString());
                return new ResponseUnit<int?> { message = "서버에서 요청을 처리하지 못하였습니다.", data = null, code = 404 };
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
                LogService.LogMessage(ex.ToString());
                return new ResponseUnit<PlaceDetailDTO> { message = "서버에서 요청을 처리하지 못하였습니다.", data = new PlaceDetailDTO(), code = 500 };
            }
        }

       

        /// <summary>
        /// 관리자에 사업장 추가
        /// </summary>
        /// <param name="placemanager"></param>
        /// <returns></returns>
        public async ValueTask<ResponseUnit<bool>> AddPlaceManagerService(AddPlaceManagerDTO<ManagerListDTO> placemanager)
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

                            if (result == true)
                            {
                                return new ResponseUnit<bool> { message = "요청이 정상 처리되었습니다.", data = true, code = 200 };
                            }
                            else if (result == false)
                            {
                                return new ResponseUnit<bool> { message = "요청이 처리되지 않았습니다.", data = false, code = 404 };
                            }
                            else
                            {
                                return new ResponseUnit<bool> { message = "요청이 처리되지 않았습니다.", data = false, code = 404 };
                            }
                        }
                        else
                        {
                            return new ResponseUnit<bool> { message = "요청이 처리되지 않았습니다.", data = false, code = 404 };
                        }

                    }
                    else
                    {
                        return new ResponseUnit<bool> { message = "요청이 처리되지 않았습니다.", data = false, code = 404 };
                    }
                }
                else
                {
                    return new ResponseUnit<bool> { message = "잘못된 요청입니다.", data = false, code = 404 };
                }
            }
            catch(Exception ex)
            {
                LogService.LogMessage(ex.ToString());
                return new ResponseUnit<bool> { message = "서버에서 요청을 처리하지 못하였습니다.", data = false, code = 500 };
            }
        }

        /// <summary>
        /// 관리자에 사업장 추가
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseUnit<bool>> AddManagerPlaceSerivce(AddManagerPlaceDTO? dto)
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
                LogService.LogMessage(ex.ToString());
                return new ResponseUnit<bool>() { message = "서버에서 요청을 처리하지 못하였습니다.", data = false, code = 500 };
            }
        }

        /// <summary>
        /// 사업장 완전삭제
        /// </summary>
        /// <param name="placeidx"></param>
        /// <returns></returns>
        public async ValueTask<ResponseUnit<bool>> DeleteManagerPlaceService(List<int> placeidx)
        {
            try
            {
                if (placeidx is [_, ..])
                {
                    for (int i = 0; i < placeidx.Count(); i++)
                    {
                        AdminPlaceTb? chktb = await AdminPlaceInfoRepository.GetWorksModelInfo(placeidx[i]);

                        if(chktb is not null)
                        {
                            return new ResponseUnit<bool>() { message = "할당된 사업장이 존재합니다.", data = false, code = 401 };
                        }
                    }

                    for (int i = 0; i < placeidx.Count(); i++) 
                    {
                        // 모델조회
                        PlaceTb? placetb = await PlaceInfoRepository.GetByPlaceInfo(placeidx[i]);
                        placetb!.DelYn = 1;
                        placetb!.DelDt = DateTime.Now;
                        // 삭제
                        bool? result = await PlaceInfoRepository.DeletePlaceInfo(placetb);
                    }

                    return new ResponseUnit<bool>() { message = "삭제완료.", data = true, code = 200 };
                }
                else
                {
                    return new ResponseUnit<bool>() { message = "선택한 내용이 없습니다.", data = false, code = 404 };
                }
            }
            catch (Exception ex)
            {
                LogService.LogMessage(ex.ToString());
                return new ResponseUnit<bool>() { message = "서버에서 요청을 처리하지 못하였습니다.", data = false, code = 500 };
            }

        }
    }
}
