using FamTec.Server.Repository.Admin.AdminPlaces;
using FamTec.Server.Repository.Admin.AdminUser;
using FamTec.Server.Repository.Admin.Departmnet;
using FamTec.Server.Repository.Place;
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
        private readonly IPlaceInfoRepository PlaceInfoRepository;

        private readonly IConfiguration Configuration;
        private ILogService LogService;


        public UserService(IUserInfoRepository _userinforepository,
            IAdminUserInfoRepository _adminuserinforepository,
            IDepartmentInfoRepository _departmentinforepository,
            IAdminPlacesInfoRepository _adminplaceinforepository,
            IPlaceInfoRepository _placeinforpeository,
            IConfiguration _configuration,
            ILogService _logservice)
        {
            this.UserInfoRepository = _userinforepository;
            this.AdminUserInfoRepository = _adminuserinforepository;
            this.DepartmentInfoRepository = _departmentinforepository;
            this.AdminPlaceInfoRepository = _adminplaceinforepository;
            this.PlaceInfoRepository = _placeinforpeository;

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
                List<AdminPlaceTb>? adminplacetb = null;
                List<PlaceTb>? placetb = null;
                List<Claim> authClaims = new List<Claim>();
                string jsonConvert = String.Empty;

                if (!String.IsNullOrWhiteSpace(dto?.UserID) && !String.IsNullOrWhiteSpace(dto?.UserPassword))
                {
                    UserTb? usertb = await UserInfoRepository.GetUserInfo(dto.UserID, dto.UserPassword);

                    if(usertb is not null)
                    {
                        authClaims.Add(new Claim("UserIdx", usertb.Id.ToString())); // + USERID
                        authClaims.Add(new Claim("Name", usertb.Name!.ToString())); // + USERNAME
                        authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        authClaims.Add(new Claim("AlarmYN", usertb.AlramYn!.ToString())); // AlarmYN

                        JObject pemrsjson = new JObject();
                        pemrsjson.Add("PermBasic", usertb.PermBasic.ToString());
                        pemrsjson.Add("PermMachine", usertb.PermMachine.ToString());
                        pemrsjson.Add("PermLift", usertb.PermLift.ToString());
                        pemrsjson.Add("PermFire", usertb.PermFire.ToString());
                        pemrsjson.Add("PermConstruct", usertb.PermConstruct.ToString());
                        pemrsjson.Add("PermNetwork", usertb.PermNetwork.ToString());
                        pemrsjson.Add("PermBeauty", usertb.PermBeauty.ToString());
                        pemrsjson.Add("PermSecurity", usertb.PermSecurity.ToString());
                        pemrsjson.Add("PermMaterial", usertb.PermMaterial.ToString());
                        pemrsjson.Add("PermEnergy", usertb.PermEnergy.ToString());
                        pemrsjson.Add("PermUser", usertb.PermUser.ToString());
                        pemrsjson.Add("PermVoc", usertb.PermVoc.ToString());

                        jsonConvert = JsonConvert.SerializeObject(pemrsjson);
                        authClaims.Add(new Claim("UserPerms", pemrsjson.ToString()));


                        if (usertb.AdminYn == 1) // 관리자가 로그인 했을경우
                        {
                            AdminTb? admintb = await AdminUserInfoRepository.GetAdminUserInfo(usertb.Id);
                            authClaims.Add(new Claim("USERTYPE", "ADMIN"));
                            authClaims.Add(new Claim("AdminIdx", admintb.Id.ToString()));

                            if (admintb.Type == "시스템관리자")
                            {
                                authClaims.Add(new Claim("JOB", "시스템관리자"));
                                authClaims.Add(new Claim(ClaimTypes.Role, "SystemManager"));
                            }
                            if(admintb.Type == "마스터")
                            {
                                authClaims.Add(new Claim("JOB", "마스터"));
                                authClaims.Add(new Claim(ClaimTypes.Role, "Master"));
                            }
                            if (admintb.Type == "매니저")
                            {
                                authClaims.Add(new Claim("JOB", "매니저"));
                                authClaims.Add(new Claim(ClaimTypes.Role, "Manager"));
                            }

                            // 관리자의 사업장이 있을경우
                            adminplacetb = await AdminPlaceInfoRepository.GetMyWorksModel(admintb.Id);

                            if (adminplacetb is [_, ..])
                            {
                                placetb = await AdminPlaceInfoRepository.GetMyWorksDetails(adminplacetb);

                                if (placetb is [_, ..])
                                {
                                    JObject placelist = new JObject();
                                    
                                    for (int i = 0; i < placetb.Count(); i++)
                                    {
                                        JObject placeperm = new JObject();
                                        placeperm.Add("PermMachine", placetb[i].PermMachine);
                                        placeperm.Add("PermLift", placetb[i].PermLift);
                                        placeperm.Add("PermFire", placetb[i].PermFire);
                                        placeperm.Add("PermConstruct", placetb[i].PermConstruct);
                                        placeperm.Add("PermNetwork", placetb[i].PermNetwrok);
                                        placeperm.Add("PermBeauty", placetb[i].PermBeauty);
                                        placeperm.Add("PermSecurity", placetb[i].PermSecurity);
                                        placeperm.Add("PermMaterial", placetb[i].PermMaterial);
                                        placeperm.Add("PermEnergy", placetb[i].PermEnergy);
                                        placeperm.Add("PermVoc", placetb[i].PermVoc);

                                        placelist.Add(placetb[i].Name, placeperm);
                                    }

                                    jsonConvert = JsonConvert.SerializeObject(placelist);
                                    authClaims.Add(new Claim("Perms", jsonConvert));
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
                            authClaims.Add(new Claim("USERTYPE", "USER"));

                            authClaims.Add(new Claim("JOB", usertb.Job ?? ""));
                            authClaims.Add(new Claim(ClaimTypes.Role, "User"));

                            PlaceTb? userplace = await PlaceInfoRepository.GetByPlaceInfo(usertb.PlaceTbId);

                            JObject placelist = new JObject();
                            
                            if(userplace is not null)
                            {
                                JObject placeperm = new JObject();
                                placeperm.Add("PermMachine", userplace.PermMachine);
                                placeperm.Add("PermLift", userplace.PermLift);
                                placeperm.Add("PermFire", userplace.PermFire);
                                placeperm.Add("PermConstruct", userplace.PermConstruct);
                                placeperm.Add("PermNetwork", userplace.PermNetwrok);
                                placeperm.Add("PermBeauty", userplace.PermBeauty);
                                placeperm.Add("PermSecurity", userplace.PermSecurity);
                                placeperm.Add("PermMaterial", userplace.PermMaterial);
                                placeperm.Add("PermEnergy", userplace.PermEnergy);
                                placeperm.Add("PermVoc", userplace.PermVoc);

                                placelist.Add(userplace.Name, placeperm);
                            }

                            jsonConvert = JsonConvert.SerializeObject(placelist);
                            authClaims.Add(new Claim("Perms", jsonConvert));

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
