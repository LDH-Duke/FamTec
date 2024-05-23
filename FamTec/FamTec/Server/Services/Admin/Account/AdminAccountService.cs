using FamTec.Server.Repository.Admin.AdminPlaces;
using FamTec.Server.Repository.Admin.AdminUser;
using FamTec.Server.Repository.Admin.Departmnet;
using FamTec.Server.Repository.Place;
using FamTec.Server.Repository.User;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO.Admin;
using FamTec.Shared.Server.DTO.Login;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FamTec.Server.Services.Admin.Account
{
    public class AdminAccountService : IAdminAccountService
    {
        private readonly IUserInfoRepository UserInfoRepository;
        private readonly IAdminUserInfoRepository AdminUserInfoRepository;
        private readonly IDepartmentInfoRepository DepartmentInfoRepository;
        private readonly IPlaceInfoRepository PlaceInfoRepository;
        private readonly IAdminPlacesInfoRepository AdminPlacesInfoRepository;

        ResponseOBJ<AccountDTO> Response;
        Func<string, AccountDTO, int, ResponseModel<AccountDTO>> FuncResponseOBJ;
        Func<string, List<AccountDTO>, int, ResponseModel<AccountDTO>> FuncResponseList;

        public AdminAccountService(IUserInfoRepository _userinfoRepository, IAdminUserInfoRepository _admininfoRepository, IDepartmentInfoRepository _departmentinfoRepository, IPlaceInfoRepository _placeinfoRepository, IAdminPlacesInfoRepository _adminplaceinfoRepository)
        {
            UserInfoRepository = _userinfoRepository;
            AdminUserInfoRepository = _admininfoRepository;
            DepartmentInfoRepository = _departmentinfoRepository;
            PlaceInfoRepository = _placeinfoRepository;
            AdminPlacesInfoRepository = _adminplaceinfoRepository;

            Response = new ResponseOBJ<AccountDTO>();
            FuncResponseOBJ = Response.RESPMessage;
            FuncResponseList = Response.RESPMessageList;
        }

        /// <summary>
        /// 관리자 로그인 서비스
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<AccountDTO>> AdminLoginService(LoginDTO? dto)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(dto?.UserID) && !string.IsNullOrWhiteSpace(dto?.UserPassword))
                {
                    UserTb? user = await UserInfoRepository.GetUserInfo(dto.UserID, dto.UserPassword);

                    if (user is not null)
                    {
                        if (user.AdminYn == 1)
                        {
                            // 관리자테이블 조회
                            AdminTb? admin = await AdminUserInfoRepository.GetAdminUserInfo(user.Id);

                            if (admin is not null)
                            {
                                // LINQ - USERTB + 관리자TB 필요한것들 한번에 DTO넣어서 반환
                                DepartmentTb? dp = await DepartmentInfoRepository.GetDepartmentInfo(admin.DepartmentTbId);
                                if (dp is not null)
                                {
                                    return FuncResponseOBJ("관리자 로그인 성공", new AccountDTO
                                    {
                                        USER_INDEX = user.Id,
                                        USERID = user.UserId,
                                        NAME = user.Name,
                                        EMAIL = user.Email,
                                        PHONE = user.Phone,
                                        ADMIN_YN = user.AdminYn,
                                        ALRAM_YN = user.AlramYn,
                                        STATUS = user.Status,
                                        JOB = user.Job,
                                        ADMIN_INDEX = admin.Id,
                                        TYPE = admin.Type,
                                        DEPARTMENT_INDEX = dp.Id,
                                        DEPARTMENT_NAME = dp.Name
                                    }, 200);
                                }
                                else
                                {
                                    return FuncResponseOBJ("관리자 정보가 일치하지 않습니다.", null, 200);
                                }
                            }
                            else
                            {
                                // 관리자가 아님 fail
                                return FuncResponseOBJ("관리자 정보가 일치하지 않습니다.", null, 200);
                            }
                        }
                        else
                        {
                            // 해당 계정은 관리자가 아닌데 - 관리자로 접근했기때문에 접근 FAIL
                            return FuncResponseOBJ("관리자 정보가 일치하지 않습니다.", null, 200);
                        }
                    }
                    else
                    {
                        // 로그인 정보가 잘못되었습니다.
                        return FuncResponseOBJ("로그인 정보가 잘못되었습니다.", null, 200);
                    }
                }
                else
                {
                    // 매개변수 잘못됨
                    return FuncResponseOBJ("로그인 정보가 잘못되었습니다.", null, 200);
                }
            }
            catch (Exception ex)
            {
                return FuncResponseOBJ(ex.Message, null, 400);
            }

        }

        public async ValueTask<ResponseModel<AccountDTO>> AdminRegisterService(AccountDTO? dto, SessionInfo session)
        {
            try
            {
                if(dto is not null && !String.IsNullOrWhiteSpace(session.Name))
                {
                    //int level = (type)

                    UserTb? usermodel = new UserTb
                    {
                        UserId = dto.USERID,
                        Name = dto.NAME,
                        Password = dto.PASSWORD,
                        Email = dto.EMAIL,
                        Phone = dto.PHONE,
                        PermBasic = 2,
                        PermMachine = 2,
                        PermLift = 2,
                        PermFire = 2,
                        PermConstruct = 2,
                        PermNetwork = 2,
                        PermBeauty = 2,
                        PermSecurity = 2,
                        PermMaterial = 2,
                        PermEnergy = 2,
                        PermUser = 2,
                        PermVoc = 2,
                        AdminYn = 1,
                        AlramYn = 1,
                        Status = 1,
                        CreateDt = DateTime.Now,
                        CreateUser = session.Name,
                        UpdateDt = DateTime.Now,
                        UpdateUser = session.Name,
                        Job = null,
                    };

                    UserTb? userresult = await UserInfoRepository.AddAsync(usermodel);

                    if (userresult is not null)
                    {
                        AdminTb? adminmodel = new AdminTb();
                        if (session.Type == "시스템관리자")
                            adminmodel.Type = "마스터";
                        if (session.Type == "마스터")
                            adminmodel.Type = "매니저";
                        adminmodel.CreateDt = DateTime.Now;
                        adminmodel.CreateUser = session.Name;
                        adminmodel.UpdateDt = DateTime.Now;
                        adminmodel.UpdateUser = session.Name;
                        adminmodel.DelYn = 0;
                        adminmodel.UserTbId = userresult.Id;
                        adminmodel.DepartmentTbId = dto.DEPARTMENT_INDEX;

                        AdminTb? adminresult = await AdminUserInfoRepository.AddAdminUserInfo(adminmodel);

                        if (dto.placeDTO is not null && dto.placeDTO.Count > 0)
                        {
                            foreach (var item in dto.placeDTO)
                            {
                                AdminPlaceTb adminplace = new AdminPlaceTb();
                                adminplace.CreateDt = DateTime.Now;
                                adminplace.CreateUser = session.Name;
                                adminplace.UpdateDt = DateTime.Now;
                                adminplace.UpdateUser = session.Name;
                                adminplace.DelYn = 0;
                                adminplace.AdminTbId = adminresult!.Id;
                                adminplace.PlaceId = item.PlaceIndex;

                                AdminPlaceTb? placeresult = await AdminPlacesInfoRepository.AddAsync(adminplace);
                            }
                        }
                        
                    }
                    return FuncResponseOBJ("관리자 등록 완료.", null, 200);
                    //UserTb? searchtb = await UserInfoRepository.AddAsync()
                }
                else
                {
                    return FuncResponseOBJ("회원가입 정보가 잘못되었습니다.", null, 200);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseOBJ(ex.Message, null, 500);
            }
        }

      

    }
}
