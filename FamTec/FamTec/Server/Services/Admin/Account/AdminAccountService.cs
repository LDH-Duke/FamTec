using FamTec.Server.Repository.Admin.AdminPlaces;
using FamTec.Server.Repository.Admin.AdminUser;
using FamTec.Server.Repository.Admin.Departmnet;
using FamTec.Server.Repository.Place;
using FamTec.Server.Repository.User;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO.Admin;
using FamTec.Shared.Server.DTO.Admin.Place;
using FamTec.Shared.Server.DTO.Login;
using FamTec.Shared.Server.DTO.Place;
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

        ResponseOBJ<ManagerLoginResultDTO> Response;
        Func<string, ManagerLoginResultDTO, int, ResponseModel<ManagerLoginResultDTO>> FuncResponseOBJ;
        Func<string, List<ManagerLoginResultDTO>, int, ResponseModel<ManagerLoginResultDTO>> FuncResponseList;

        ResponseOBJ<AddManagerDTO> ResponseAdd;
        Func<string, AddManagerDTO, int, ResponseModel<AddManagerDTO>> FuncResponseAdd;
        

        public AdminAccountService(IUserInfoRepository _userinfoRepository, IAdminUserInfoRepository _admininfoRepository, IDepartmentInfoRepository _departmentinfoRepository, IPlaceInfoRepository _placeinfoRepository, IAdminPlacesInfoRepository _adminplaceinfoRepository)
        {
            UserInfoRepository = _userinfoRepository;
            AdminUserInfoRepository = _admininfoRepository;
            DepartmentInfoRepository = _departmentinfoRepository;
            PlaceInfoRepository = _placeinfoRepository;
            AdminPlacesInfoRepository = _adminplaceinfoRepository;

            Response = new ResponseOBJ<ManagerLoginResultDTO>();
            FuncResponseOBJ = Response.RESPMessage;
            FuncResponseList = Response.RESPMessageList;

            ResponseAdd = new ResponseOBJ<AddManagerDTO>();
            FuncResponseAdd = ResponseAdd.RESPMessage;
        }

        /// <summary>
        /// 관리자 접속화면 서비스
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<ManagerLoginResultDTO>> AdminLoginService(LoginDTO? dto)
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

                                    ManagerLoginResultDTO account = new ManagerLoginResultDTO();
                                    account.USER_INDEX = user.Id;
                                    account.PASSWORD = user.Password;
                                    account.NAME = user.Name;
                                    account.EMAIL = user.Email;
                                    account.PHONE = user.Phone;
                                    account.ADMIN_YN = user.AdminYn;
                                    account.ALRAM_YN = user.AlramYn;
                                    account.STATUS = user.Status;
                                    account.ADMIN_INDEX = admin.Id;
                                    account.TYPE = admin.Type;
                                    account.DEPARTMENT_INDEX = dp.Id;
                                    account.DEPARTMENT_NAME = dp.Name;

                                    // 로그인후 PlaceDTO 나오는것 다시 만들어야할듯.

                                    List<PlacesDTO>? placedto = await AdminPlacesInfoRepository.GetLoginWorks(admin.Id);

                                    if(placedto is [_, ..])
                                    {
                                        for(int i = 0; i < placedto.Count; i++)
                                        {
                                            account.placeDTO!.Add(placedto[i]);
                                        }
                                    }
                                    return FuncResponseOBJ("관리자 로그인 성공", account, 200);
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

        /// <summary>
        /// 관리자 아이디 생성 서비스
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<AddManagerDTO>> AdminRegisterService(AddManagerDTO? dto, SessionInfo session)
        {
            try
            {
                if(dto is not null && !String.IsNullOrWhiteSpace(session.Name))
                {
                    UserTb? usermodel = new UserTb
                    {
                        UserId = dto.UserId,
                        Name = dto.Name,
                        Password = dto.Password,
                        Email = dto.Email,
                        Phone = dto.Phone,
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
                        UpdateUser = session.Name
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
                        adminmodel.DepartmentTbId = dto.DepartmentId;

                        AdminTb? adminresult = await AdminUserInfoRepository.AddAdminUserInfo(adminmodel);

                        return FuncResponseAdd("관리자 등록 완료.", new AddManagerDTO
                        {
                            UserId = userresult.UserId,
                            Password = userresult.Password,
                            Email = userresult.Email,
                            Phone = userresult.Phone,
                            Type = adminresult.Type,
                            DepartmentId = adminresult.DepartmentTbId,
                            Name = userresult.Name
                        }, 200);
                    }
                    else
                    {
                        return FuncResponseAdd("관리자 등록 실패", new AddManagerDTO(), 200);
                    }
                }
                else
                {
                    return FuncResponseAdd("요청이 잘못되었습니다.", new AddManagerDTO(), 404);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseAdd("서버에서 요청을 처리하지 못하였습니다.", new AddManagerDTO(), 500);
            }
        }

      

    }
}
