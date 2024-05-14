using Azure;
using FamTec.Server.Repository.Place;
using FamTec.Server.Repository.User;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Abstractions;
using System.Reflection.Metadata.Ecma335;

namespace FamTec.Server.Services.User
{
    public class UserServices : IUserServices
    {
        private readonly IUserInfoRepository UserInfoRepository;
        

        public UserServices(IUserInfoRepository _userinforepository)
        {
            this.UserInfoRepository = _userinforepository;
        
        }
     

        /// <summary>
        /// USERID 전체 조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<ResponseObject<UsersDTO>> GetAllUserListService()
        {
            try
            {
                List<UsersTb>? result = await UserInfoRepository.GetAllAsync();

                if (result is [_, ..])
                {
                    ResponseObject<UsersDTO> obj = new()
                    {
                        Message = "성공",
                        Data = result.Select(e => new UsersDTO()
                        {
                            USERID = e.UserId,
                            PASSWORD = e.Password,
                            NAME = e.Name,
                            EMAIL = e.Email,
                            PHONE = e.Phone,
                            PERM_BUILDING = e.PermBuilding,
                            PERM_EQUIPMENT = e.PermBuilding,
                            PERM_MATERIAL = e.PermMaterial,
                            PERM_ENERGY = e.PermEnergy,
                            PERM_OFFICE = e.PermOffice,
                            PERM_COMP = e.PermComp,
                            PERM_CONST = e.PermConst,
                            PERM_CLAIM = e.PermClaim,
                            PERM_SYS = e.PermSys,
                            PERM_EMPLOYEE = e.PermEmployee,
                            PERM_LAW_CK = e.PermLawCk,
                            PERM_LAW_EDU = e.PermLawEdu,
                            ADMIN_YN = e.AdminYn,
                            ALARM_YN = e.AlarmYn,
                            STATUS = e.Status
                        }).ToList(),

                        StatusCode = 200
                    };
                    return obj;
                }
                else
                {
                    ResponseObject<UsersDTO> obj = new()
                    {
                        StatusCode = 200
                    };
                    return obj;
                }
            }
            catch (Exception ex)
            {

                ResponseObject<UsersDTO> obj = new()
                {
                    StatusCode = 500
                };
                return obj;
            }

        }


        /// <summary>
        /// USERID로 조회 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        // userid는 차후 토큰이 될것임.
        public async ValueTask<ResponseObject<UsersDTO>> GetUserService(string userid)
        {
            if (userid is not null)
            {
                UsersTb? result = await UserInfoRepository.GetByUserInfo(userid);

                if (result is not null)
                {
                    ResponseObject<UsersDTO> model = new()
                    {
                        Message = "데이터 검색 성공",
                        Data = new List<UsersDTO>()
                        {
                            new UsersDTO()
                            {
                                USERID = result.UserId,
                                PASSWORD = result.Password,
                                NAME = result.Name,
                                EMAIL = result.Email,
                                PHONE = result.Phone,
                                PERM_BUILDING = result.PermBuilding,
                                PERM_EQUIPMENT = result.PermBuilding,
                                PERM_MATERIAL = result.PermMaterial,
                                PERM_ENERGY = result.PermEnergy,
                                PERM_OFFICE = result.PermOffice,
                                PERM_COMP = result.PermComp,
                                PERM_CONST = result.PermConst,
                                PERM_CLAIM = result.PermClaim,
                                PERM_SYS = result.PermSys,
                                PERM_EMPLOYEE = result.PermEmployee,
                                PERM_LAW_CK = result.PermLawCk,
                                PERM_LAW_EDU = result.PermLawEdu,
                                ADMIN_YN = result.AdminYn,
                                ALARM_YN = result.AlarmYn,
                                STATUS = result.Status
                            }
                        },
                        StatusCode = 200
                    };
                    return model;
                }
                else
                {
                    ResponseObject<UsersDTO> model = new()
                    {
                        Message = "데이터가 존재하지 않습니다.",
                        Data = null,
                        StatusCode = 200
                    };
                    return model;
                }
            }
            else
            {
                ResponseObject<UsersDTO> model = new()
                {
                    Message = "데이터가 비어있습니다.",
                    Data = null,
                    StatusCode = 404
                };
                return model;
            }
        }

        /// <summary>
        /// 사용자 추가
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseObject<UsersDTO>> AddUserService(UsersDTO dto)
        {
            if(dto is not null)
            {
                UsersTb? placechk = await UserInfoRepository.GetByUserInfo(dto.USERID);

                if(placechk == null) // 없음
                {
                    UsersTb usertb = new UsersTb()
                    {
                        // Null 해도될듯
                        UserId = dto.USERID,
                        Password = dto.PASSWORD,
                        Name = dto.NAME,
                        Email = dto.EMAIL,
                        Phone = dto.PHONE,
                        PermBuilding = dto.PERM_BUILDING ?? 0,
                        PermEquipment = dto.PERM_EQUIPMENT ?? 0,
                        PermMaterial = dto.PERM_MATERIAL ?? 0,
                        PermEnergy = dto.PERM_ENERGY ?? 0,
                        PermOffice = dto.PERM_OFFICE ?? 0,
                        PermComp = dto.PERM_COMP ?? 0,
                        PermConst = dto.PERM_CONST ?? 0,
                        PermClaim = dto.PERM_CLAIM ?? 0,
                        PermSys = dto.PERM_SYS ?? 0,
                        PermEmployee = dto.PERM_EMPLOYEE ?? 0,
                        PermLawCk = dto.PERM_LAW_CK ?? 0,
                        PermLawEdu = dto.PERM_LAW_EDU ?? 0,
                        AdminYn = dto.ADMIN_YN,
                        AlarmYn = dto.ALARM_YN,
                        Status = dto.STATUS,
                        CreateDt = DateTime.Now,
                        CreateUser = "토큰USER", // 토큰주인
                        PlacecodeCd = dto.PLACECODE // 선택된 사업장코드
                    };

                    var result = await UserInfoRepository.AddAsync(usertb);
                    
                    if(result == null)
                    {
                        // ADD에 실패하였을때
                        ResponseObject<UsersDTO> model = new()
                        {
                            Message = "데이터 추가에 실패하였습니다.",
                            Data = null,
                            StatusCode = 404
                        };
                        return model;
                    }
                    else
                    {
                        ResponseObject<UsersDTO> model = new()
                        {
                            Message = "데이터 추가에 성공하였습니다.",
                            Data = new List<UsersDTO>()
                            {
                                new UsersDTO()
                                {
                                    USERID = result.UserId,
                                    PASSWORD = result.Password,
                                    NAME = result.Name,
                                    EMAIL = result.Email,
                                    PHONE = result.Phone,
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
                                    PERM_LAW_EDU = result.PermLawEdu,
                                    ADMIN_YN = result.AdminYn,
                                    ALARM_YN = result.AlarmYn,
                                    STATUS = result.Status,
                                    PLACECODE = result.PlacecodeCd
                                }
                            },
                            StatusCode = 200
                        };
                        return model;
                    }
                }
                else
                {
                    ResponseObject<UsersDTO> model = new()
                    {
                        Message = "이미 해당코드로 사업장이 존재합니다.",
                        Data = null,
                        StatusCode = 200
                    };
                    return model;
                }
            }
            else
            {
                // 이미 해당 아이디의 사용자가 있음
                ResponseObject<UsersDTO> model = new()
                {
                    Message = "이미 해당 아이디의 사용자가 존재합니다.",
                    Data = null,
                    StatusCode = 200
                };
                return model;
            }
        }

        /// <summary>
        /// 사용자 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseObject<UsersDTO>> UpdateUserService(UsersDTO dto)
        {
            if(dto is not null) // 넘어온 DTO가 NULL이 아니어야 함.
            {
                UsersTb? model = await UserInfoRepository.GetByUserInfo(dto.USERID); // 해당 USERID로 사용자가 있는지 조회

                if(model == null) // 없음
                {
                    // 없어서 수정 못함. return 해야함.
                    ResponseObject<UsersDTO> obj = new()
                    {
                        Message = "데이터가 존재하지 않습니다.",
                        Data = null,
                        StatusCode = 404
                    };
                    return obj;
                }
                else
                {
                    if(!String.IsNullOrWhiteSpace(dto.USERID))
                        model.UserId = dto.USERID; // ID가 수정가능하게 되야할지 고민.
                    if(!String.IsNullOrWhiteSpace(dto.PASSWORD))
                        model.Password = dto.PASSWORD;
                    if(!String.IsNullOrWhiteSpace(dto.NAME))
                        model.Name = dto.NAME;
                    if (!String.IsNullOrWhiteSpace(dto.EMAIL))
                        model.Email = dto.EMAIL;
                    if(!String.IsNullOrWhiteSpace(dto.PHONE))
                        model.Phone = dto.PHONE;
                    if(dto.PERM_BUILDING != null)
                        model.PermBuilding = dto.PERM_BUILDING ?? 0;
                    if (dto.PERM_EQUIPMENT != null)
                        model.PermEquipment = dto.PERM_EQUIPMENT ?? 0;
                    if (dto.PERM_MATERIAL != null)
                        model.PermEquipment = dto.PERM_MATERIAL ?? 0;
                    if (dto.PERM_OFFICE != null)
                        model.PermOffice = dto.PERM_OFFICE ?? 0;
                    if (dto.PERM_COMP != null)
                        model.PermComp = dto.PERM_COMP ?? 0;
                    if (dto.PERM_CONST != null)
                        model.PermConst = dto.PERM_CONST ?? 0;
                    if (dto.PERM_CLAIM != null)
                        model.PermClaim = dto.PERM_CLAIM ?? 0;
                    if (dto.PERM_SYS != null)
                        model.PermSys = dto.PERM_SYS ?? 0;
                    if (dto.PERM_EMPLOYEE != null)
                        model.PermEmployee = dto.PERM_EMPLOYEE ?? 0;
                    if (dto.PERM_LAW_CK != null)
                        model.PermLawCk = dto.PERM_LAW_CK ?? 0;
                    if (dto.PERM_LAW_EDU != null)
                        model.PermLawEdu = dto.PERM_LAW_EDU ?? 0;
                    if (dto.ADMIN_YN != null)
                        model.AdminYn = dto.ADMIN_YN;
                    if (dto.ALARM_YN != null)
                        model.AlarmYn = dto.ALARM_YN;
                    if (String.IsNullOrWhiteSpace(dto.PLACECODE))
                        model.PlacecodeCd = dto.PLACECODE;

                    model.UpdateDt = DateTime.Now;
                    model.UpdateUser = "토큰USER";

                    bool result = await UserInfoRepository.EditAsync(model);

                    if(result) // 수정성공
                    {
                        ResponseObject<UsersDTO> obj = new()
                        {
                            Message = "데이터 수정 성공.",
                            Data = new List<UsersDTO>()
                            {
                                new UsersDTO()
                                {
                                    USERID = model.UserId,
                                    PASSWORD = model.Password,
                                    NAME = model.Name,
                                    EMAIL = model.Email,
                                    PHONE = model.Phone,
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
                                    ADMIN_YN = model.AdminYn,
                                    ALARM_YN = model.AlarmYn,
                                    STATUS = model.Status,
                                    PLACECODE = model.PlacecodeCd
                                }
                            },
                            StatusCode = 200
                        };
                        return obj;
                    }
                    else
                    {
                        ResponseObject<UsersDTO> obj = new()
                        {
                            Message = "데이터 수정 실패.",
                            Data = null,
                            StatusCode = 404
                        };
                        return obj;
                    }
                }
            }
            else
            {
                ResponseObject<UsersDTO> obj = new()
                {
                    Message = "데이터가 비어있습니다.",
                    Data = null,
                    StatusCode = 404
                };
                return obj;
            }
        }

        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseObject<UsersDTO>> DeleteUserService(UsersDTO dto)
        {
            if (dto is not null) // 넘어온 DTO가 NULL이 아니어야 함.
            {
                UsersTb? model = await UserInfoRepository.GetByUserInfo(dto.USERID); // 해당 USERID로 사용자가 있는지 조회

                if (model == null) // 없음
                {
                    // 없어서 수정 못함. return 해야함.
                    ResponseObject<UsersDTO> obj = new()
                    {
                        Message = "데이터가 존재하지 않습니다.",
                        Data = null,
                        StatusCode = 404
                    };
                    return obj;
                }
                else
                {
                    model.DelDt = DateTime.Now;
                    model.DelUser = "토큰USER";
                    model.DelYn = true;

                    bool result = await UserInfoRepository.EditAsync(model);

                    if (result) // 수정성공
                    {
                        ResponseObject<UsersDTO> obj = new()
                        {
                            Message = "데이터 삭제 성공.",
                            Data = new List<UsersDTO>()
                            {
                                new UsersDTO()
                                {
                                    USERID = model.UserId,
                                    PASSWORD = model.Password,
                                    NAME = model.Name,
                                    EMAIL = model.Email,
                                    PHONE = model.Phone,
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
                                    ADMIN_YN = model.AdminYn,
                                    ALARM_YN = model.AlarmYn,
                                    STATUS = model.Status,
                                    PLACECODE = model.PlacecodeCd
                                }
                            },
                            StatusCode = 200
                        };
                        return obj;
                    }
                    else
                    {
                        ResponseObject<UsersDTO> obj = new()
                        {
                            Message = "데이터 삭제 실패.",
                            Data = null,
                            StatusCode = 404
                        };
                        return obj;
                    }
                }
            }
            else
            {
                ResponseObject<UsersDTO> obj = new()
                {
                    Message = "데이터가 비어있습니다.",
                    Data = null,
                    StatusCode = 404
                };
                return obj;
            }
        }


    }
}
