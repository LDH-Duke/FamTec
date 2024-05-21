using FamTec.Server.Repository.User;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop.Infrastructure;

namespace FamTec.Server.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserInfoRepository UserInfoRepository;

        ResponseOBJ<UsersDTO> Response;
        Func<string, UsersDTO, int, ResponseModel<UsersDTO>> FuncResponseOBJ;
        Func<string, List<UsersDTO>, int, ResponseModel<UsersDTO>> FuncResponseList;


        public UserService(IUserInfoRepository _userinforepository)
        {
            this.UserInfoRepository = _userinforepository;

            Response = new ResponseOBJ<UsersDTO>();
            FuncResponseOBJ = Response.RESPMessage;
            FuncResponseList = Response.RESPMessageList;
        }


        /// <summary>
        /// 전체조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<ResponseModel<UsersDTO>?> GetAllUserService()
        {
            try
            {
                List<UserTb>? model = await UserInfoRepository.GetAllList();

                if(model is [_, ..])
                {
                    return FuncResponseList("전체데이터 조회 성공", model.Select(e => new UsersDTO
                    {
                        USERID = e.UserId,
                        NAME = e.Name,
                        EMAIL = e.Email,
                        PHONE = e.Phone,
                        STATUS = e.Status,
                        PLACEID = e.PlaceTbId,
                        ADMIN_YN = e.AdminYn,
                        ALARM_YN = e.AlramYn,
                        PERM_BUILDING = e.PermBuilding,
                        PERM_EQUIPMENT = e.PermEquipment,
                        PERM_MATERIAL = e.PermMaterial,
                        PERM_ENERGY = e.PermEnergy,
                        PERM_OFFICE = e.PermOffice,
                        PERM_COMP = e.PermComp,
                        PERM_CONST = e.PermConst,
                        PERM_CLAIM = e.PermClaim,
                        PERM_SYS = e.PermSys,
                        PERM_EMPLOYEE = e.PermEmployee,
                        PERM_LAW_CK = e.PermLawCk,
                        PERM_LAW_EDU = e.PermLawEdu
                    }).ToList(), 200);
                }
                else
                {
                    return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseOBJ(ex.Message, null, 500);
            }
        }


        /// <summary>
        /// 사업장에 해당하는 사용자 리스트 조회
        /// </summary>
        /// <param name="placetbid"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<UsersDTO>?> GetAllPlaceUser(int? placetbid)
        {
            try
            {
                if (placetbid != null)
                {
                    List<UserTb>? model = await UserInfoRepository.GetAllUserList(placetbid);

                    if(model is [_, ..])
                    {
                        return FuncResponseList("전체데이터 조회 성공", model.Select(e => new UsersDTO
                        {
                            ID = e.Id,
                            USERID = e.UserId,
                            NAME = e.Name,
                            EMAIL = e.Email,
                            PHONE = e.Phone,
                            STATUS = e.Status,
                            PLACEID = e.PlaceTbId,
                            ADMIN_YN = e.AdminYn,
                            ALARM_YN = e.AlramYn,
                            PERM_BUILDING = e.PermBuilding,
                            PERM_EQUIPMENT = e.PermEquipment,
                            PERM_MATERIAL = e.PermMaterial,
                            PERM_ENERGY = e.PermEnergy,
                            PERM_OFFICE = e.PermOffice,
                            PERM_COMP = e.PermComp,
                            PERM_CONST = e.PermConst,
                            PERM_CLAIM = e.PermClaim,
                            PERM_SYS = e.PermSys,
                            PERM_EMPLOYEE = e.PermEmployee,
                            PERM_LAW_CK = e.PermLawCk,
                            PERM_LAW_EDU = e.PermLawEdu
                        }).ToList(), 200);
                    }
                    else
                    {
                        return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
                    }
                }
                else
                {
                    return FuncResponseOBJ("데이터 요청이 잘못되었습니다.", null, 400);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseOBJ(ex.Message, null, 500);
            }
        }

  
        /// <summary>
        /// 사업장-유저 단일모델 조회
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="placetbid"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<UsersDTO>?> GetUserService(string? userid, int? placetbid)
        {
            try
            {
                if(!String.IsNullOrWhiteSpace(userid) && placetbid is not null)
                {
                    UserTb? model = await UserInfoRepository.GetUserInfo(userid, placetbid);

                    if(model is not null)
                    {
                        return FuncResponseOBJ("전체데이터 조회 선공", new UsersDTO
                        {
                            USERID = model.UserId,
                            NAME = model.Name,
                            EMAIL = model.Email,
                            PHONE = model.Phone,
                            STATUS = model.Status, // 재직여부
                            PLACEID = model.PlaceTbId, // 사업장정보
                            ADMIN_YN = model.AdminYn, // 관리자여부
                            ALARM_YN = model.AlramYn, // 알람받기여부
                            PERM_BUILDING = model.PermBuilding,
                            PERM_EQUIPMENT = model.PermEquipment,
                            PERM_MATERIAL = model.PermMaterial,
                            PERM_ENERGY = model.PermEnergy,
                            PERM_OFFICE = model.PermOffice,
                            PERM_COMP = model.PermComp,
                            PERM_CONST = model.PermConst,
                            PERM_CLAIM = model.PermClaim,
                            PERM_SYS = model.PermSys,
                            PERM_EMPLOYEE = model.PermEmployee,
                            PERM_LAW_CK = model.PermLawCk,
                            PERM_LAW_EDU = model.PermLawEdu,
                            
                        }, 200);
                    }
                    else
                    {
                        return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
                    }

                }
                else
                {
                    return FuncResponseOBJ("데이터 요청이 잘못되었습니다.", null, 400);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseOBJ(ex.Message, null, 500);
            }
        }

        /// <summary>
        /// INDEX로 사용자 모델 조회
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<UsersDTO>?> GetUserService(int? idx)
        {
            try
            {
                if (idx is not null)
                {
                    UserTb? model = await UserInfoRepository.GetUserInfo(idx);

                    if(model is not null)
                    {
                        return FuncResponseOBJ("전체데이터 조회 선공", new UsersDTO
                        {
                            ID = model.Id,
                            USERID = model.UserId,
                            NAME = model.Name,
                            EMAIL = model.Email,
                            PHONE = model.Phone,
                            STATUS = model.Status, // 재직여부
                            PLACEID = model.PlaceTbId, // 사업장정보
                            ADMIN_YN = model.AdminYn, // 관리자여부
                            ALARM_YN = model.AlramYn, // 알람받기여부
                            PERM_BUILDING = model.PermBuilding,
                            PERM_EQUIPMENT = model.PermEquipment,
                            PERM_MATERIAL = model.PermMaterial,
                            PERM_ENERGY = model.PermEnergy,
                            PERM_OFFICE = model.PermOffice,
                            PERM_COMP = model.PermComp,
                            PERM_CONST = model.PermConst,
                            PERM_CLAIM = model.PermClaim,
                            PERM_SYS = model.PermSys,
                            PERM_EMPLOYEE = model.PermEmployee,
                            PERM_LAW_CK = model.PermLawCk,
                            PERM_LAW_EDU = model.PermLawEdu,

                        }, 200);
                    }
                    else
                    {
                        return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
                    }
                }
                else
                {
                    return FuncResponseOBJ("데이터 요청이 잘못되었습니다.", null, 400);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseOBJ(ex.Message, null, 500);
            }
        }


        /// <summary>
        /// 사용자 추가
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<UsersDTO>?> AddUserService(UsersDTO? dto)
        {
            try
            {
                if(dto is not null)
                {
                    UserTb model = new UserTb
                    {
                        Password = dto.PASSWORD,
                        Name = dto.NAME,
                        Email = dto.EMAIL,
                        Phone = dto.PHONE,
                        PermBuilding = dto.PERM_BUILDING,
                        PermEquipment = dto.PERM_EQUIPMENT,
                        PermMaterial = dto.PERM_MATERIAL,
                        PermEnergy = dto.PERM_ENERGY,
                        PermOffice = dto.PERM_OFFICE,
                        PermComp = dto.PERM_COMP,
                        PermConst = dto.PERM_CONST,
                        PermClaim = dto.PERM_CLAIM,
                        PermSys = dto.PERM_SYS,
                        PermEmployee = dto.PERM_EMPLOYEE,
                        PermLawCk = dto.PERM_LAW_CK,
                        PermLawEdu = dto.PERM_LAW_EDU,

                        AdminYn = dto.ADMIN_YN,
                        AlramYn = dto.ALARM_YN,

                        Status = dto.STATUS,

                        CreateDt = DateTime.Now,
                        CreateUser = "토큰",
                        UpdateDt = DateTime.Now,
                        UpdateUser = "토큰",
                        PlaceTbId = dto.PLACEID, /* combobox 선택 */
                    };

                    var result = await UserInfoRepository.AddAsync(model);

                    if(result is not null)
                    {
                        return FuncResponseOBJ("데이터가 정상 처리되었습니다.", new UsersDTO()
                        {
                            NAME = result.Name,
                            EMAIL = result.Email,
                            PHONE = result.Phone,
                            STATUS = result.Status,
                            PLACEID = result.PlaceTbId,
                            ADMIN_YN = result.AdminYn,
                            ALARM_YN = result.AlramYn,
                            PERM_BUILDING = result.PermBuilding,
                            PERM_EQUIPMENT = result.PermEquipment,
                            PERM_MATERIAL = result.PermMaterial,
                            PERM_ENERGY = result.PermEnergy,
                            PERM_OFFICE = result.PermOffice,
                            PERM_COMP = result.PermComp,
                            PERM_CONST = result.PermConst,
                            PERM_CLAIM = result.PermClaim,
                            PERM_SYS = result.PermSys,
                            PERM_EMPLOYEE = result.PermEmployee,
                            PERM_LAW_CK = result.PermLawCk,
                            PERM_LAW_EDU = result.PermLawEdu
                        }, 200);
                    }
                    else
                    {
                        return FuncResponseOBJ("데이터가 처리되지 않았습니다.", null, 200);
                    }
                }
                else
                {
                    return FuncResponseOBJ("데이터 요청이 잘못되었습니다.", null, 400);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseOBJ(ex.Message, null, 500);
            }
        }


        /// <summary>
        /// 사용자 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<UsersDTO>?> EditUserService(UsersDTO? dto)
        {
            try
            {
                if(dto is not null)
                {
                    UserTb? model = await UserInfoRepository.GetUserInfo(dto.USERID, dto.PLACEID);

                    if(model is not null)
                    {
                        model.UserId = dto.USERID;
                        model.Password = dto.PASSWORD;
                        model.Name = dto.NAME;
                        model.Email = dto.EMAIL;
                        model.Phone = dto.PHONE;
                        model.Status = dto.STATUS;

                        model.PermBuilding = dto.PERM_BUILDING;
                        model.PermEquipment = dto.PERM_EQUIPMENT;
                        model.PermMaterial = dto.PERM_MATERIAL;
                        model.PermEnergy = dto.PERM_ENERGY;
                        model.PermOffice = dto.PERM_OFFICE;
                        model.PermComp = dto.PERM_COMP;
                        model.PermConst = dto.PERM_CONST;
                        model.PermClaim = dto.PERM_CLAIM;
                        model.PermSys = dto.PERM_SYS;
                        model.PermEmployee = dto.PERM_EMPLOYEE;
                        model.PermLawCk = dto.PERM_LAW_CK;
                        model.PermLawEdu = dto.PERM_LAW_EDU;


                        model.UpdateDt = DateTime.Now;
                        model.UpdateUser = "토큰USER";

                        bool? result = await UserInfoRepository.EditUserInfo(model);

                        if(result == true)
                        {
                            return FuncResponseOBJ("데이터 수정 성공", new UsersDTO()
                            {
                                USERID = model.UserId,
                                PASSWORD = model.Password,
                                NAME = model.Name,
                                EMAIL = model.Email,
                                PHONE = model.Phone,
                                STATUS = model.Status, // 재직여부

                                PERM_BUILDING = model.PermBuilding,
                                PERM_EQUIPMENT = model.PermEquipment,
                                PERM_MATERIAL = model.PermMaterial,
                                PERM_ENERGY = model.PermEnergy,
                                PERM_OFFICE = model.PermOffice,
                                PERM_COMP = model.PermComp,
                                PERM_CONST = model.PermConst,
                                PERM_CLAIM = model.PermClaim,
                                PERM_SYS = model.PermSys,
                                PERM_EMPLOYEE = model.PermEmployee,
                                PERM_LAW_CK = model.PermLawCk,
                                PERM_LAW_EDU = model.PermLawEdu,

                            }, 200);
                        }
                        else
                        {
                            return FuncResponseOBJ("데이터 수정 실패", null, 200);
                        }
                    }
                    else
                    {
                        return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
                    }


                }
                else
                {
                    return FuncResponseOBJ("데이터 요청이 잘못되었습니다.", null, 400);
                }


            }
            catch(Exception ex)
            {
                return FuncResponseOBJ(ex.Message, null, 500);
            }
         
        }


        /// <summary>
        /// 사용자 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<UsersDTO>?> DeleteUserService(UsersDTO? dto)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(dto.USERID) && dto.PLACEID is not null)
                {
                    UserTb? model = await UserInfoRepository.GetUserInfo(dto.USERID, dto.PLACEID);

                    if(model is not null)
                    {
                        model.UpdateDt = DateTime.Now;
                        model.UpdateUser = "토큰USER";

                        bool? result = await UserInfoRepository.DeleteUserInfo(model);

                        if(result == true)
                        {
                            return FuncResponseOBJ("데이터 삭제 성공", new UsersDTO()
                            {
                                USERID = model.UserId,
                                PASSWORD = model.Password,
                                NAME = model.Name,
                                EMAIL = model.Email,
                                PHONE = model.Phone,
                                STATUS = model.Status, // 재직여부

                                PERM_BUILDING = model.PermBuilding,
                                PERM_EQUIPMENT = model.PermEquipment,
                                PERM_MATERIAL = model.PermMaterial,
                                PERM_ENERGY = model.PermEnergy,
                                PERM_OFFICE = model.PermOffice,
                                PERM_COMP = model.PermComp,
                                PERM_CONST = model.PermConst,
                                PERM_CLAIM = model.PermClaim,
                                PERM_SYS = model.PermSys,
                                PERM_EMPLOYEE = model.PermEmployee,
                                PERM_LAW_CK = model.PermLawCk,
                                PERM_LAW_EDU = model.PermLawEdu,

                            }, 200);
                        }
                        else
                        {
                            return FuncResponseOBJ("데이터 삭제 실패", null, 200);
                        }
                    }
                    else
                    {
                        return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
                    }
                }
                else
                {
                    return FuncResponseOBJ("데이터 요청이 잘못되었습니다.", null, 400);
                }


            }
            catch(Exception ex)
            {
                return FuncResponseOBJ(ex.Message, null, 500);
            }
        }

    
    }
}
