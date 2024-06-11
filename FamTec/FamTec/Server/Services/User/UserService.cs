using FamTec.Server.Repository.Admin.AdminPlaces;
using FamTec.Server.Repository.Admin.AdminUser;
using FamTec.Server.Repository.Admin.Departmnet;
using FamTec.Server.Repository.User;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO;
using FamTec.Shared.Server.DTO.Login;
using FamTec.Shared.Server.DTO.User;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FamTec.Server.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserInfoRepository UserInfoRepository;
        private readonly IAdminUserInfoRepository AdminUserInfoRepository;
        private readonly IDepartmentInfoRepository DepartmentInfoRepository;
        private readonly IAdminPlacesInfoRepository AdminPlaceInfoRepository;

        private readonly IConfiguration Configuration;
        private ILogService LogService;


        public UserService(IUserInfoRepository _userinforepository,
            IAdminUserInfoRepository _adminuserinforepository,
            IDepartmentInfoRepository _departmentinforepository,
            IAdminPlacesInfoRepository _adminplaceinforepository,
            IConfiguration _configuration,
            ILogService _logservice)
        {
            this.UserInfoRepository = _userinforepository;
            this.AdminUserInfoRepository = _adminuserinforepository;
            this.DepartmentInfoRepository = _departmentinforepository;
            this.AdminPlaceInfoRepository = _adminplaceinforepository;


            this.Configuration = _configuration;
            this.LogService = _logservice;
        }

        /// <summary>
        /// 일반페이지 로그인 서비스
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseUnit<string>?> UserLoginService(LoginDTO? dto)
        {
            try
            {
                DepartmentTb? departmenttb = null;
                List<AdminPlaceTb>? adminplacetb = null;
                List<PlaceTb>? placetb = null;
                List<Claim> authClaims = new List<Claim>();

                if (!String.IsNullOrWhiteSpace(dto?.UserID) && !String.IsNullOrWhiteSpace(dto?.UserPassword))
                {
                    UserTb? usertb = await UserInfoRepository.GetUserInfo(dto.UserID, dto.UserPassword);

                    if(usertb is not null)
                    {
                        authClaims.Add(new Claim("UserIdx", usertb.Id.ToString())); // + USERID
                        authClaims.Add(new Claim("Name", usertb.Name!.ToString())); // + USERNAME
                        authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        
                        if (usertb.AdminYn == 1) // 관리자가 로그인 했을경우
                        {
                            AdminTb? admintb = await AdminUserInfoRepository.GetAdminUserInfo(usertb.Id);
                            if(admintb is not null)
                            {
                                authClaims.Add(new Claim("AlarmYN", usertb.AlramYn.ToString()!)); // + 알람여부
                                authClaims.Add(new Claim("AdminIdx", admintb.Id.ToString())); // + ADMINID
                                if (admintb.Type == "시스템관리자")
                                {
                                    authClaims.Add(new Claim("UserType", "시스템관리자"));
                                    authClaims.Add(new Claim(ClaimTypes.Role, "SystemManager"));
                                }
                                if (admintb.Type == "마스터")
                                {
                                    authClaims.Add(new Claim("UserType", "마스터"));
                                    authClaims.Add(new Claim(ClaimTypes.Role, "Master"));
                                }
                                if (admintb.Type == "매니저")
                                {
                                    authClaims.Add(new Claim("UserType", "매니저"));
                                    authClaims.Add(new Claim(ClaimTypes.Role, "Manager"));
                                }

                                // 관리자 테이블에 정상접근 됐을때
                                departmenttb = await DepartmentInfoRepository.GetDepartmentInfo(admintb.DepartmentTbId);
                                if(departmenttb is not null)
                                {
                                    authClaims.Add(new Claim("DepartIdx", admintb.DepartmentTbId.ToString()!)); // + DEPARTMENTID
                                    
                                    // 관리자의 사업장이 있을경우
                                    adminplacetb = await AdminPlaceInfoRepository.GetMyWorksModel(admintb.Id);
                                    if(adminplacetb is [_, ..])
                                    {
                                        placetb = await AdminPlaceInfoRepository.GetMyWorksDetails(adminplacetb);
                                        if (placetb is [_, ..])
                                        {
                                            JObject json = new JObject();
                                            for (int i = 0; i < placetb.Count; i++)
                                            {
                                                json.Add(placetb[i].Name!, placetb[i].Id);
                                            }

                                            string jsonConvert = JsonConvert.SerializeObject(json);
                                            authClaims.Add(new Claim("Places", jsonConvert));
                                        }
                                    }

                                    // JWT 인증 페이로드 사인 비밀키
                                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:authSigningKey"]!));

                                    JwtSecurityToken token = new JwtSecurityToken(
                                        issuer: Configuration["JWT:Issuer"],
                                        audience: Configuration["JWT:Audience"],
                                        expires: DateTime.Now.AddDays(1),
                                        claims: authClaims,
                                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

                                    string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

                                    return new ResponseUnit<string>() { message = "로그인 성공(관리자).", data = accessToken, code = 200 };
                                }
                                else
                                {
                                    return new ResponseUnit<string>() { message = "로그인 실패(부서가 존재하지 않습니다).", data = null, code = 401 };
                                }
                            }
                            else
                            {
                                return new ResponseUnit<string>() { message = "로그인 실패(잘못된 접근입니다).", data = null, code = 401 };
                            }
                        }
                        else // 일반유저가 로그인 했을 경우
                        {
                            authClaims.Add(new Claim("Places", usertb.PlaceTbId.ToString() ?? "")); // 속한 사업장
                            authClaims.Add(new Claim("PermBasic", usertb.PermBasic.ToString() ?? "")); // 기초 권한 여부
                            authClaims.Add(new Claim("PermMachine", usertb.PermMachine.ToString() ?? "")); // 설비 권한 여부
                            authClaims.Add(new Claim("PermLift", usertb.PermLift.ToString() ?? "")); //  승강관리 권한 여부
                            authClaims.Add(new Claim("PermFire", usertb.PermFire.ToString() ?? "")); // 소방관리 권한 여부
                            authClaims.Add(new Claim("PermConstruct", usertb.PermConstruct.ToString() ?? "")); // 건축관리 권한 여부
                            authClaims.Add(new Claim("PermNetwork", usertb.PermNetwork.ToString() ?? "")); // 통신연동 권한 여부
                            authClaims.Add(new Claim("PermBeauty", usertb.PermBeauty.ToString() ?? "")); // 미화 권한 여부
                            authClaims.Add(new Claim("PermSecurity", usertb.PermSecurity.ToString() ?? "")); // 보안 권한 여부
                            authClaims.Add(new Claim("PermMaterial", usertb.PermMaterial.ToString() ?? "")); // 자재관리 권한 여부
                            authClaims.Add(new Claim("PermEnergy", usertb.PermEnergy.ToString() ?? "")); // 에너지관리 권한 여부
                            authClaims.Add(new Claim("PermUser", usertb.PermUser.ToString() ?? "")); // 사용자 관리 권한 여부
                            authClaims.Add(new Claim("PermVoc", usertb.PermVoc.ToString() ?? "")); // 민원관리 권한 여부
                            authClaims.Add(new Claim("Job", usertb.Job!.ToString() ?? "")); // 직책
                            authClaims.Add(new Claim("UserType", "유저"));

                            // JWT 인증 페이로드 사인 비밀키
                            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:authSigningKey"]!));

                            JwtSecurityToken token = new JwtSecurityToken(
                                        issuer: Configuration["JWT:Issuer"],
                                        audience: Configuration["JWT:Audience"],
                                        expires: DateTime.Now.AddDays(1),
                                        claims: authClaims,
                                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

                            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

                            return new ResponseUnit<string>() { message = "로그인 성공(유저).", data = accessToken, code = 200 };
                        }
                    }
                    else
                    {
                        return new ResponseUnit<string>() { message = "존재하지 않는 사용자 입니다.", data = null, code = 401 };
                    }
                }
                else
                {
                    // 요청이 잘못되었습니다.
                    return new ResponseUnit<string>() { message = "요청이 잘못되었습니다.", data = null, code = 404 };
                }
            }
            catch(Exception ex)
            {
                LogService.LogMessage(ex.ToString());
                return new ResponseUnit<string>() { message = "서버에서 요청을 처리하지 못하였습니다.", data = null, code = 500 };
            }
        }


        /// <summary>
        /// 아이디 중복검사
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<ResponseUnit<UsersDTO>?> UserIdCheck(string? userid)
        {
            try
            {
                if(!String.IsNullOrWhiteSpace(userid))
                {
                    UserTb? model = await UserInfoRepository.UserIdCheck(userid);
                    if(model is not null)
                    {
                        return new ResponseUnit<UsersDTO>() { message = "해당아이디가 존재합니다.", data = new UsersDTO() { ID = model.Id, USERID = model.UserId, NAME = model.Name }, code = 200 };
                    }
                    else
                    {
                        return new ResponseUnit<UsersDTO>() { message = "해당아이디가 존재하지 않습니다.", data = new UsersDTO(), code = 200 };
                    }
                }
                else
                {
                    return new ResponseUnit<UsersDTO>() { message = "요청이 잘못되었습니다.", data = new UsersDTO(), code = 404 };
                }
            }
            catch(Exception ex)
            {
                LogService.LogMessage(ex.ToString());
                return new ResponseUnit<UsersDTO>() { message = "서버에서 요청을 처리하지 못하였습니다.", data = new UsersDTO(), code = 500 };
            }
        }

   
    }
}
