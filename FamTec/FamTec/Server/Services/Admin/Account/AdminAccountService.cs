using FamTec.Server.Repository.Admin.AdminPlaces;
using FamTec.Server.Repository.Admin.AdminUser;
using FamTec.Server.Repository.Admin.Departmnet;
using FamTec.Server.Repository.User;
using FamTec.Server.Tokens;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO;
using FamTec.Shared.Server.DTO.Admin;
using FamTec.Shared.Server.DTO.Admin.Place;
using FamTec.Shared.Server.DTO.Login;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FamTec.Server.Services.Admin.Account
{
    public class AdminAccountService : IAdminAccountService
    {
        private readonly IUserInfoRepository UserInfoRepository;
        private readonly IAdminUserInfoRepository AdminUserInfoRepository;
        private readonly IDepartmentInfoRepository DepartmentInfoRepository;
        private readonly IAdminPlacesInfoRepository AdminPlaceInfoRepository;


        private readonly IConfiguration Configuration;
        private ILogService LogService;


        public AdminAccountService(IUserInfoRepository _userinfoRepository,
            IAdminUserInfoRepository _admininfoRepository,
            IDepartmentInfoRepository _departmentinfoRepository,
            IAdminPlacesInfoRepository _adminplaceinforepository,
            IConfiguration _configuration,
            ILogService _logservice)
        {
            this.UserInfoRepository = _userinfoRepository;
            this.AdminUserInfoRepository = _admininfoRepository;
            this.DepartmentInfoRepository = _departmentinfoRepository;
            this.AdminPlaceInfoRepository = _adminplaceinforepository;


            this.Configuration = _configuration;
            this.LogService = _logservice;
        }

        /// <summary>
        /// 관리자 접속화면 서비스
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async ValueTask<ResponseUnit<string>> AdminLoginService(LoginDTO? dto)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(dto?.UserID) && !string.IsNullOrWhiteSpace(dto?.UserPassword))
                {
                    UserTb? usertb = await UserInfoRepository.GetUserInfo(dto.UserID, dto.UserPassword);

                    if (usertb is not null)
                    {
                        if (usertb.AdminYn == 1)
                        {
                            AdminTb? admintb = await AdminUserInfoRepository.GetAdminUserInfo(usertb.Id);

                            if (admintb is not null)
                            {
                                DepartmentTb? departmenttb = await DepartmentInfoRepository.GetDepartmentInfo(admintb.DepartmentTbId);
                                if (departmenttb is not null)
                                {
                                    // 로그인성공
                                    List<Claim> authClaims = new List<Claim>
                                    {
                                        new Claim("UserIdx",usertb.Id.ToString()),
                                        new Claim("Name", usertb.Name!),
                                        new Claim("AdminIdx",admintb.Id.ToString()),
                                        new Claim("DepartIdx",admintb.DepartmentTbId.ToString()!),
                                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                    };


                                    /* 토큰에 같이 넣고싶은 데이터 */
                                    authClaims.Add(new Claim("DepartmentName",  departmenttb.Name ?? ""));

                                    if (admintb.Type == "시스템관리자")
                                    {
                                        authClaims.Add(new Claim("UserType", "시스템관리자"));
                                        authClaims.Add(new Claim(ClaimTypes.Role, "SystemManager"));
                                        //authClaims.Add(new Claim("Roles", "SystemManager"));
                                    }
                                    if (admintb.Type == "마스터")
                                    {
                                        authClaims.Add(new Claim("UserType", "마스터"));
                                        authClaims.Add(new Claim(ClaimTypes.Role, "Master"));
                                        //authClaims.Add(new Claim("Roles", "Master"));
                                    }
                                    if (admintb.Type == "매니저")
                                    {
                                        authClaims.Add(new Claim("UserType", "매니저"));
                                        authClaims.Add(new Claim(ClaimTypes.Role, "Manager"));
                                        //authClaims.Add(new Claim("Roles", "Manager"));
                                    }

                                    // JWT 인증 페이로드 사인 비밀키
                                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:authSigningKey"]!));

                                    var token = new JwtSecurityToken(
                                        issuer: Configuration["JWT:Issuer"],
                                        audience: Configuration["JWT:Audience"],
                                        expires: DateTime.Now.AddDays(1),
                                        claims: authClaims,
                                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

                                    string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

                                    // 로그인 성공
                                    return new ResponseUnit<string>() { message = "관리자 로그인 성공.", data = accessToken, code = 200 };
                                }
                                else
                                {
                                    // 정보가 잘못되었습니다.
                                    return new ResponseUnit<string>() { message = "관리자 로그인 실패.", data = null, code = 401 };
                                }
                            }
                            else
                            {
                                // 관리자 아님
                                return new ResponseUnit<string>() { message = "로그인 실패 (해당 아이디는 관리자가 아닙니다.)", data = null, code = 401 };
                            }
                        }
                        else
                        {
                            // 관리자 아님
                            return new ResponseUnit<string>() { message = "로그인 실패 (해당 아이디는 관리자가 아닙니다.)", data = null, code = 401 };
                        }
                    }
                    else
                    {
                        // 아이디가 존재하지 않음
                        return new ResponseUnit<string>() { message = "로그인 실패 (해당 아이디가 존재하지 않습니다.)", data = null, code = 401 };
                    }
                }
                else
                {
                    // 요청이 잘못됨
                    return new ResponseUnit<string>() { message = "로그인 실패 (요청 정보가 잘못되었습니다.)", data = null, code = 404 };
                }
            }
            catch (Exception ex) 
            {
                LogService.LogMessage(ex.ToString());
                return new ResponseUnit<string>() { message = "로그인 실패 (서버에서 요청을 처리하지 못하였습니다.)", data = null, code = 500 };
            }
        }

        /// <summary>
        /// 관리자 아이디 생성 서비스
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public async ValueTask<ResponseUnit<AdminTb>> AdminRegisterService(AddManagerDTO? dto, AdminSettingModel? token)
        {
            try
            {
                if(dto is not null)
                {
                    AdminTb? verifictoken = await AdminUserInfoRepository.GetAdminUserInfo(token!.UserIdx);

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
                        CreateUser = token.UserName,
                        UpdateDt = DateTime.Now,
                        UpdateUser = token.UserName
                    };

                    UserTb? userresult = await UserInfoRepository.AddAsync(usermodel);

                    if (userresult is not null)
                    {
                        AdminTb? adminmodel = new AdminTb();
                        
                        if (verifictoken!.Type == "시스템관리자")
                            adminmodel.Type = "마스터";
                        if (verifictoken!.Type == "마스터")
                            adminmodel.Type = "매니저";
                        
                        adminmodel.CreateDt = DateTime.Now;
                        adminmodel.CreateUser = token.UserName;
                        adminmodel.UpdateDt = DateTime.Now;
                        adminmodel.UpdateUser = token.UserName;
                        adminmodel.DelYn = 0;
                        adminmodel.UserTbId = userresult.Id;
                        adminmodel.DepartmentTbId = dto.DepartmentId;

                        AdminTb? adminresult = await AdminUserInfoRepository.AddAdminUserInfo(adminmodel);
                        
                        if (adminresult is not null)
                        {
                            return new ResponseUnit<AdminTb> { message = "요청이 정상 처리되었습니다.", data = adminresult, code = 200 };
                        }
                        else
                        {
                            return new ResponseUnit<AdminTb> { message = "요청이 처리되지 않았습니다.", data = adminresult, code = 404 };
                        }
                    }
                    else
                    {
                        return new ResponseUnit<AdminTb> { message = "요청이 처리되지 않았습니다.", data = new AdminTb(), code = 404 };
                    }
                }
                else
                {
                    return new ResponseUnit<AdminTb> { message = "요청이 잘못되었습니다.", data = new AdminTb(), code = 404 };
                }
            }
            catch(Exception ex)
            {
                LogService.LogMessage(ex.ToString());
                return new ResponseUnit<AdminTb> { message = "서버에서 요청을 처리하지 못하였습니다.", data = new AdminTb(), code = 500 };
            }
        }

        public async ValueTask<ResponseUnit<int>> DeleteAdminService(List<int> adminid)
        {
            int count = 0;
            
            try
            {
                if(adminid is [_, ..])
                {
                    for (int i=0;i<adminid.Count;i++)
                    {
                        AdminTb? admintb = await AdminUserInfoRepository.GetAdminUserInfo(adminid[i]);

                        if (admintb is not null)
                        {
                            admintb.DelYn = 1;
                            admintb.DelDt = DateTime.Now;

                            bool? deleteresult = await AdminUserInfoRepository.DeleteAdminInfo(admintb);
                            
                            if (deleteresult == true)
                            {
                                UserTb? usertb = await UserInfoRepository.GetUserIndexInfo(admintb.UserTbId);

                                if (usertb is not null)
                                {
                                    usertb.DelYn = 1;
                                    usertb.DelDt = DateTime.Now;

                                    bool? delresult = await UserInfoRepository.DeleteUserInfo(usertb);

                                    if (delresult == true)
                                    {
                                        count++; // 삭제개수 카운팅

                                        List<AdminPlaceTb>? adminplacetb = await AdminPlaceInfoRepository.GetMyWorksModel(admintb.Id);
                                        
                                        if (adminplacetb is [_, ..])
                                        {
                                            bool? result = await AdminPlaceInfoRepository.DeleteMyWorks(adminplacetb);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            return new ResponseUnit<int> { message = "요청이 처리되지 않았습니다.", data = count, code = 404 };
                        }
                    }

                    return new ResponseUnit<int> { message = $"요청이 {count}건 정상 처리되었습니다.", data = count, code = 200 };
                }
                else
                {
                    return new ResponseUnit<int> { message = "잘못된 요청입니다.", data = count, code = 404 };
                }
            }
            catch(Exception ex)
            {
                LogService.LogMessage(ex.ToString());
                return new ResponseUnit<int> { message = "서버에서 요청을 처리하지 못하였습니다.", data = count, code = 500 };
            }
        }

        /// <summary>
        /// 매니저 상세보기 서비스
        /// </summary>
        /// <param name="adminidx"></param>
        /// <returns></returns>
        public async ValueTask<ResponseUnit<DManagerDTO>> DetailAdminService(int? adminidx)
        {
            try
            {
                if (adminidx is not null)
                {
                    DManagerDTO? dto = await AdminPlaceInfoRepository.GetManagerDetails(adminidx);
                    
                    if(dto is not null)
                    {
                        return new ResponseUnit<DManagerDTO>() { message = "요청이 정상 처리되었습니다.", data = dto, code = 200 };
                    }
                    else
                    {
                        return new ResponseUnit<DManagerDTO>() { message = "요청이 처리되지 않았습니다.", data = new DManagerDTO(), code = 200 };
                    }
                }
                else
                {
                    return new ResponseUnit<DManagerDTO>() { message = "잘못된 요청입니다.", data = new DManagerDTO(), code = 404 };
                }
            }
            catch (Exception ex)
            {
                LogService.LogMessage(ex.ToString());
                return new ResponseUnit<DManagerDTO>() { message = "서버에서 요청을 처리하지 못하였습니다.", data = new DManagerDTO(), code = 500 };
            }
        }
    }
}
