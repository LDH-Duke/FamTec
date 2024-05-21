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
                        ID = e.Id,
                        USERID = e.UserId,
                        NAME = e.Name,
                        EMAIL = e.Email,
                        PHONE = e.Phone,
                        PERM_BASIC = e.PermBasic,
                        PERM_MACHINE = e.PermMachine,
                        PERM_LIFT = e.PermLift,
                        PERM_FIRE = e.PermFire,
                        PERM_CONSTRUCT = e.PermConstruct,
                        PERM_NETWORK = e.PermNetwork,
                        PERM_BEAUTY = e.PermBeauty,
                        PERM_SECURITY = e.PermSecurity,
                        PERM_MATERIAL = e.PermMaterial,
                        PERM_ENERGY = e.PermEnergy,
                        PERM_USER = e.PermUser,
                        PERM_VOC = e.PermVoc,
                        ADMIN_YN = e.AdminYn,
                        ALRAM_YN = e.AlramYn,
                        STATUS = e.Status,
                        JOB = e.Job,
                        PLACEID = e.PlaceTbId,
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
                            PERM_BASIC = e.PermBasic,
                            PERM_MACHINE = e.PermMachine,
                            PERM_LIFT = e.PermLift,
                            PERM_FIRE = e.PermFire,
                            PERM_CONSTRUCT = e.PermConstruct,
                            PERM_NETWORK = e.PermNetwork,
                            PERM_BEAUTY = e.PermBeauty,
                            PERM_SECURITY = e.PermSecurity,
                            PERM_MATERIAL = e.PermMaterial,
                            PERM_ENERGY = e.PermEnergy,
                            PERM_USER = e.PermUser,
                            PERM_VOC = e.PermVoc,
                            ADMIN_YN = e.AdminYn,
                            ALRAM_YN = e.AlramYn,
                            STATUS = e.Status,
                            JOB = e.Job,
                            PLACEID = e.PlaceTbId,
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
                            ID = model.Id, // 인덱스
                            USERID = model.UserId, // 사용자 아이디
                            NAME = model.Name, // 사용자이름
                            EMAIL = model.Email, // 이메일
                            PHONE = model.Phone, // 전화번호
                            PERM_BASIC = model.PermBasic, // 기본정보등록 권한
                            PERM_MACHINE = model.PermMachine, // 설비 권한
                            PERM_LIFT = model.PermLift, // 승강권한
                            PERM_FIRE = model.PermFire, // 소방 권한
                            PERM_CONSTRUCT = model.PermConstruct, // 건축 권한
                            PERM_NETWORK = model.PermNetwork, // 통신 권한
                            PERM_BEAUTY= model.PermBeauty, // 미화권한
                            PERM_SECURITY = model.PermSecurity, // 보안권한
                            PERM_MATERIAL = model.PermMaterial, // 자재권한
                            PERM_ENERGY = model.PermEnergy, // 에너지 권한
                            PERM_USER = model.PermUser, // 사용자 설정 권한
                            PERM_VOC = model.PermVoc, // VOC 권한
                            ADMIN_YN = model.AdminYn, // 관리자여부
                            STATUS = model.Status, // 재직여부
                            JOB = model.Job, // 직책
                            PLACEID = model.PlaceTbId, // 사업장정보
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
                            ID = model.Id, // 인덱스
                            USERID = model.UserId, // 사용자 아이디
                            NAME = model.Name, // 사용자이름
                            EMAIL = model.Email, // 이메일
                            PHONE = model.Phone, // 전화번호
                            PERM_BASIC = model.PermBasic, // 기본정보등록 권한
                            PERM_MACHINE = model.PermMachine, // 설비 권한
                            PERM_LIFT = model.PermLift, // 승강권한
                            PERM_FIRE = model.PermFire, // 소방 권한
                            PERM_CONSTRUCT = model.PermConstruct, // 건축 권한
                            PERM_NETWORK = model.PermNetwork, // 통신 권한
                            PERM_BEAUTY = model.PermBeauty, // 미화권한
                            PERM_SECURITY = model.PermSecurity, // 보안권한
                            PERM_MATERIAL = model.PermMaterial, // 자재권한
                            PERM_ENERGY = model.PermEnergy, // 에너지 권한
                            PERM_USER = model.PermUser, // 사용자 설정 권한
                            PERM_VOC = model.PermVoc, // VOC 권한
                            ADMIN_YN = model.AdminYn, // 관리자여부
                            STATUS = model.Status, // 재직여부
                            JOB = model.Job, // 직책
                            PLACEID = model.PlaceTbId, // 사업장정보
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
                        Password = dto.PASSWORD, // 비밀번호
                        Name = dto.NAME, // 이름
                        Email = dto.EMAIL, // 이메일
                        Phone = dto.PHONE, // 전화번호
                        PermBasic = dto.PERM_BASIC, // 기본정보등록 권한
                        PermMachine = dto.PERM_MACHINE, // 설비권한
                        PermLift = dto.PERM_LIFT, // 승강 권한
                        PermFire = dto.PERM_FIRE, // 소방 권한
                        PermConstruct = dto.PERM_CONSTRUCT, // 건축 권한
                        PermNetwork = dto.PERM_NETWORK, // 통신 권한
                        PermBeauty = dto.PERM_BEAUTY, // 미화 권한
                        PermSecurity = dto.PERM_SECURITY, // 보안권한
                        PermMaterial = dto.PERM_MATERIAL, // 자재권한
                        PermEnergy = dto.PERM_ENERGY, // 에너지권한
                        PermUser = dto.PERM_USER, // 사용자 설정 권한
                        PermVoc = dto.PERM_VOC, // VOC 권한
                        AdminYn = dto.ADMIN_YN, // 관리자 유무
                        AlramYn = dto.ALRAM_YN, // 알람유무
                        Status = 1, // 재직
                        Job = dto.JOB, // 직책

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
                            NAME = result.Name, // 이름
                            EMAIL = result.Email, // 이메일
                            PHONE = result.Phone, // 전화번호
                            PERM_BASIC = result.PermBasic, // 기본정보등록 권한
                            PERM_MACHINE = result.PermMachine, // 설비권한
                            PERM_LIFT = result.PermLift, // 승강권한
                            PERM_FIRE = result.PermFire, // 소방권한
                            PERM_CONSTRUCT = result.PermConstruct, // 건축권한
                            PERM_NETWORK = result.PermNetwork, // 통신권한
                            PERM_BEAUTY = result.PermBeauty, // 미화권한
                            PERM_SECURITY = result.PermSecurity, // 보안권한
                            PERM_MATERIAL = result.PermMaterial, // 자재권한
                            PERM_ENERGY = result.PermEnergy, // 에너지 권한
                            PERM_USER = result.PermUser, // 사용자 설정 권한
                            PERM_VOC = result.PermVoc, // VOC 권한
                            ADMIN_YN = result.AdminYn, // 관리자유무
                            ALRAM_YN = result.AlramYn, // 알람유무
                            JOB = result.Job, // 직책
                            STATUS = result.Status, 
                            PLACEID = result.PlaceTbId,
                            
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
                        model.UserId = dto.USERID; // 사용자 아이디
                        model.Password = dto.PASSWORD; // 사용자 비밀번호
                        model.Name = dto.NAME; // 사용자 이름
                        model.Email = dto.EMAIL; // 이메일
                        model.Phone = dto.PHONE; // 전화번호
                        model.PermBasic = dto.PERM_BASIC; // 기본정보등록 권한
                        model.PermMachine = dto.PERM_MACHINE; // 설비권한
                        model.PermLift = dto.PERM_LIFT; // 승강권한
                        model.PermFire = dto.PERM_FIRE; // 소방권한
                        model.PermConstruct = dto.PERM_CONSTRUCT; // 건축권한
                        model.PermNetwork = dto.PERM_NETWORK; // 통신권한
                        model.PermBeauty = dto.PERM_BEAUTY; // 미화권한
                        model.PermSecurity = dto.PERM_SECURITY; // 보안 권한
                        model.PermMaterial = dto.PERM_MATERIAL; // 자재권한
                        model.PermEnergy = dto.PERM_ENERGY; // 에너지권한
                        model.PermUser = dto.PERM_USER; // 사용자 설정 권한
                        model.PermVoc = dto.PERM_VOC; // VOC 권한

                        model.AlramYn = dto.ALRAM_YN; // 알람유무
                        model.Status = dto.STATUS; // 재직유무

                        model.UpdateDt = DateTime.Now;
                        model.UpdateUser = "토큰USER";

                        bool? result = await UserInfoRepository.EditUserInfo(model);

                        if(result == true)
                        {
                            return FuncResponseOBJ("데이터 수정 성공", new UsersDTO()
                            {
                                USERID = model.UserId, // 사용자 ID
                                PASSWORD = model.Password, // 비밀번호
                                NAME = model.Name, // 사용자이름
                                EMAIL = model.Email, // 이메일
                                PHONE = model.Phone, // 전화번호

                                PERM_BASIC = model.PermBasic, // 기본정보등록 권한
                                PERM_MACHINE = model.PermMachine, // 설비권한
                                PERM_LIFT = model.PermLift, // 승강권한
                                PERM_FIRE = model.PermFire, // 소방권한
                                PERM_CONSTRUCT = model.PermConstruct, // 건축권한
                                PERM_NETWORK = model.PermNetwork, // 통신권한
                                PERM_BEAUTY = model.PermBeauty, // 미화권한
                                PERM_SECURITY = model.PermSecurity, // 보안권한
                                PERM_MATERIAL = model.PermMaterial, // 자재권한
                                PERM_ENERGY = model.PermEnergy, // 에너지권한
                                PERM_USER = model.PermUser, // 사용자 설정 권한
                                PERM_VOC = model.PermVoc, // VOC 권한
                                ALRAM_YN = model.AlramYn, // 알람유무
                                STATUS = model.Status, // 재직유무
                                JOB = model.Job

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
                        model.DelYn = 1;
                        model.UpdateDt = DateTime.Now;
                        model.UpdateUser = "토큰USER";

                        bool? result = await UserInfoRepository.DeleteUserInfo(model);

                        if(result == true)
                        {
                            return FuncResponseOBJ("데이터 삭제 성공", new UsersDTO()
                            {
                                USERID = model.UserId,
                                NAME = model.Name,
                                EMAIL = model.Email,
                                PHONE = model.Phone,
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
