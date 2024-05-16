using FamTec.Server.Repository.Admin.AdminPlaces;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;

namespace FamTec.Server.Services.Admin.Place
{
    /// <summary>
    /// 관리자 사업장 서비스
    /// </summary>
    public class AdminPlaceService : IAdminPlaceService
    {
        private readonly IAdminPlacesInfoRepository AdminPlacesInfoRepository;

        ResponseOBJ<AdminPlacesDTO> Response;
        Func<string, AdminPlacesDTO, int, ResponseModel<AdminPlacesDTO>> FuncResponseOBJ;
        Func<string, List<AdminPlacesDTO>, int, ResponseModel<AdminPlacesDTO>> FuncResponseList;

        public AdminPlaceService(IAdminPlacesInfoRepository _adminplacesuinforepository)
        {
            this.AdminPlacesInfoRepository = _adminplacesuinforepository;

            Response = new ResponseOBJ<AdminPlacesDTO>();
            FuncResponseOBJ = Response.RESPMessage;
            FuncResponseList = Response.RESPMessageList;
        }

        /// <summary>
        /// 관리자 사업장 전체 조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<ResponseModel<AdminPlacesDTO>> GetAllWorks()
        {
            List<AdminPlacesTb>? result = await AdminPlacesInfoRepository.GetAllList();

            if(result is [_, ..])
            {
                return FuncResponseList("전체데이터 조회 성공", result.Select(e => new AdminPlacesDTO()
                {
                    UserID = e.UsersUserid,
                    PlaceCode = e.PlacecodeCd
                }).ToList(), 200);
            }
            else
            {
                return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
            }
        }

        /// <summary>
        /// 관리자 USERID에 해당하는 전체 관리자 사업장 모델리스트 출력
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<AdminPlacesDTO>> GetUserIDWorksList(string userid)
        {
            List<AdminPlacesTb>? result = await AdminPlacesInfoRepository.GetAllUserList(userid);

            if(result is [_, ..])
            {
                return FuncResponseList("전체데이터 조회 성공", result.Select(e => new AdminPlacesDTO()
                {
                    UserID = e.UsersUserid,
                    PlaceCode = e.PlacecodeCd
                }).ToList(), 200);
            }
            else
            {
                return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
            }

        }

        /// <summary>
        /// 사업장 코드에 해당하는 전체 관리자 사업장 모델리스트 출력
        /// </summary>
        /// <param name="placecd"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<AdminPlacesDTO>> GetPlaceCDWorksList(string placecd)
        {
            List<AdminPlacesTb>? result = await AdminPlacesInfoRepository.GetAllPlaceList(placecd);

            if(result is [_, ..])
            {
                return FuncResponseList("전체데이터 조회 성공", result.Select(e => new AdminPlacesDTO()
                {
                    UserID = e.UsersUserid,
                    PlaceCode = e.PlacecodeCd
                }).ToList(), 200);
            }
            else
            {
                return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
            }
        }

        /// <summary>
        /// 관리자 사업장 추가
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<AdminPlacesDTO>> AddAdminWorksInfo(AdminPlacesDTO dto)
        {
            if (dto is not null)
            {
                AdminPlacesTb? chk = await AdminPlacesInfoRepository.GetPlaceInfo(dto.UserID, dto.PlaceCode);
                if (chk is null)
                {
                    AdminPlacesTb model = new AdminPlacesTb();
                    model.UsersUserid = dto.UserID;
                    model.PlacecodeCd = dto.PlaceCode;
                    model.CreateDt = DateTime.Now;

                    var result = await AdminPlacesInfoRepository.AddAsync(model);

                    if (result == null)
                    {
                        return FuncResponseOBJ("데이터 추가에 실패하였습니다.", null, 404);
                    }
                    else
                    {
                        return FuncResponseOBJ("관리자 사업장 추가에 성공하였습니다.", new AdminPlacesDTO()
                        {
                            UserID = result.UsersUserid,
                            PlaceCode = result.PlacecodeCd
                        }, 200);
                    }
                }
                else
                {
                    return FuncResponseOBJ("이미 해당관리자는 해당 사업장에 존재합니다.", null, 200);
                }
            }
            else
            {
                return FuncResponseOBJ("데이터가 비어있습니다.", null, 404);
            }
        }

        /// <summary>
        /// 관리자 사업장 DTO에 해당하는 내용 데이터베이스에 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<AdminPlacesDTO>> UpdateAdminWorks(AdminPlacesDTO beforedto, AdminPlacesDTO afterdto)
        {
            if(beforedto is not null && afterdto is not null) // 넘어온 DTO가 NULL이 아니어야 함.
            {
                AdminPlacesTb? model = await AdminPlacesInfoRepository.GetPlaceInfo(beforedto.UserID, beforedto.PlaceCode);
                if(model == null)
                {
                    return FuncResponseOBJ("해당 데이터가 존재하지 않습니다.", null, 404);
                }
                else
                {
                    model.UsersUserid = afterdto.UserID;
                    model.PlacecodeCd = afterdto.PlaceCode;
                    model.UpdateDt = DateTime.Now;

                    bool? result = await AdminPlacesInfoRepository.EditAdminPlacesInfo(model);

                    if(result == true)
                    {
                        return FuncResponseOBJ("데이터 수정 성공", new AdminPlacesDTO()
                        {
                            UserID = model.UsersUserid,
                            PlaceCode = model.PlacecodeCd
                        }, 200);
                    }
                    else if(result == false)
                    {
                        return FuncResponseOBJ("데이터 수정 실패", null, 404);
                    }
                    else
                    {
                        return FuncResponseOBJ("데이터가 비어있습니다.", null, 404);
                    }
                }
            }
            else
            {
                return FuncResponseOBJ("데이터가 비어있습니다.", null, 404);
            }
        }

        /// <summary>
        /// 관리자 사업장 DTO에 해당하는 내용 데이터베이스에 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<AdminPlacesDTO>> DeleteAdminWorks(AdminPlacesDTO dto)
        {
            if(dto is not null)
            {
                AdminPlacesTb? model = await AdminPlacesInfoRepository.GetPlaceInfo(dto.UserID, dto.PlaceCode);
                if (model == null)
                {
                    return FuncResponseOBJ("해당 데이터가 존재하지 않습니다.", null, 404);
                }
                else
                {
                    model.DelDt = DateTime.Now;
                    model.DelYn = true;

                    bool? result = await AdminPlacesInfoRepository.DeleteAdminPlacesInfo(model);

                    if(result == true)
                    {
                        return FuncResponseOBJ("데이터 삭제 성공", new AdminPlacesDTO()
                        {
                            UserID = model.UsersUserid,
                            PlaceCode = model.PlacecodeCd
                        }, 200);
                    }
                    else if(result == false)
                    {
                        return FuncResponseOBJ("데이터 삭제 실패", null, 404);

                    }
                    else
                    {
                        return FuncResponseOBJ("데이터가 비어있습니다.", null, 404);
                    }
                }
            }
            else
            {
                return FuncResponseOBJ("데이터가 비어있습니다.", null, 404);
            }
        }
    }
}
