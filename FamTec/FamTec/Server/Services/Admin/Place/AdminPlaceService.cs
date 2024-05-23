using Azure;
using FamTec.Client.Pages.Place;
using FamTec.Server.Repository.Admin.AdminPlaces;
using FamTec.Server.Repository.Place;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO;
using FamTec.Shared.Server.DTO.Admin;
using FamTec.Shared.Server.DTO.Place;
using System.Reflection.Metadata.Ecma335;

namespace FamTec.Server.Services.Admin.Place
{
    public class AdminPlaceService : IAdminPlaceService
    {
        private readonly IPlaceInfoRepository PlaceInfoRepository;
        private readonly IAdminPlacesInfoRepository AdminPlaceInfoRepository;

        ResponseOBJ<PlacesDTO> Response;
        Func<string, PlacesDTO, int, ResponseModel<PlacesDTO>> FuncResponseOBJ;
        Func<string, List<PlacesDTO>, int, ResponseModel<PlacesDTO>> FuncResponseList;

        ResponseOBJ<AdminPlaceDTO> AdminPlaceResponse;
        Func<string, AdminPlaceDTO, int, ResponseModel<AdminPlaceDTO>> AdminPlaceResponseOBJ;
        Func<string, List<AdminPlaceDTO>, int, ResponseModel<AdminPlaceDTO>> AdminPlaceResponseList;

        public AdminPlaceService(IAdminPlacesInfoRepository _adminplaceinforepository, IPlaceInfoRepository _placeinforepository)
        {
            this.AdminPlaceInfoRepository = _adminplaceinforepository;
            this.PlaceInfoRepository = _placeinforepository;

            Response = new ResponseOBJ<PlacesDTO>();
            FuncResponseOBJ = Response.RESPMessage;
            FuncResponseList = Response.RESPMessageList;

            AdminPlaceResponse = new ResponseOBJ<AdminPlaceDTO>();
            AdminPlaceResponseOBJ = AdminPlaceResponse.RESPMessage;
            AdminPlaceResponseList = AdminPlaceResponse.RESPMessageList;
        }

        /* 사업장추가 (수정) */
        public async ValueTask<ResponseModel<PlacesDTO>> AddPlaceService(PlacesDTO? dto, SessionInfo session)
        {
            try
            {
                if (dto is not null && !String.IsNullOrWhiteSpace(session.Name))
                {
                    PlaceTb? model = await PlaceInfoRepository.GetByPlaceInfo(dto.PlaceCd);
                    if (model is null)
                    {
                        // NULL 일때만 들어가야함.
                        PlaceTb? tb = new PlaceTb
                        {
                            PlaceCd = dto.PlaceCd, // 사업장코드
                            ContractNum = dto.CONTRACT_NUM, // 계약번호
                            Name = dto.Name, // 사업장명
                            Note = dto.Note, // 비고
                            Address = dto.Address, // 주소
                            ContractDt = dto.ContractDT, // 계약일자
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
                            CreateUser = session.Name,
                            UpdateDt = DateTime.Now,
                            UpdateUser = session.Name,
                            DelYn = 0,
                            DelUser = null
                        };

                        PlaceTb? result = await PlaceInfoRepository.AddPlaceInfo(tb);
                        if (result is not null)
                        {
                            // 제대로 들어감
                            return FuncResponseOBJ("요청이 정상 처리되었습니다.", new PlacesDTO()
                            {
                                PlaceCd = dto.PlaceCd,
                                CONTRACT_NUM = dto.CONTRACT_NUM,
                                Name = dto.Name,
                                Note = dto.Note,
                                Address = dto.Address,
                                ContractDT = dto.ContractDT,
                                PermMachine = dto.PermMachine,
                                PermLift = dto.PermLift,
                                PermFire = dto.PermFire,
                                PermConstruct = dto.PermConstruct,
                                PermNetwork = dto.PermNetwork,
                                PermBeauty = dto.PermBeauty,
                                PermSecurity = dto.PermSecurity,
                                PermMaterial = dto.PermMaterial,
                                PermEnergy = dto.PermEnergy,
                                CancelDT = dto.CancelDT,
                                Status = dto.Status
                            }, 200);
                        }
                        else
                        {
                            return FuncResponseOBJ("요청이 처리되지 않았습니다.", null, 200);
                        }
                    }
                    else
                    {
                        return FuncResponseOBJ("이미 해당코드의 사업장이 존재합니다.", null, 200);
                    }
                }
                else
                {
                    return FuncResponseOBJ("요청이 잘못되었습니다.", null, 404);
                }
            }
            catch (Exception ex)
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
    }
}
