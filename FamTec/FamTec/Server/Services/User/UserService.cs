using FamTec.Client.Pages.Admin.Place.PlaceMain;
using FamTec.Server.Repository.Admin.AdminPlaces;
using FamTec.Server.Repository.Admin.AdminUser;
using FamTec.Server.Repository.Admin.Departmnet;
using FamTec.Server.Repository.Place;
using FamTec.Server.Repository.User;
using FamTec.Server.Tokens;
using FamTec.Shared.Client.DTO.Normal.Users;
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

        public async ValueTask<ResponseUnit<string>?> LoginSelectPlaceService(HttpContext context, int? placeid)
        {
            List<Claim> authClaims = new List<Claim>();
            string jsonConvert = String.Empty;

            try
            {
                if (context.Items["AdminIdx"] is null)
                    return new ResponseUnit<string>() { message = "관리자 ID가 없습니다.", data = null, code = 404 };
                if(placeid is null)
                    return new ResponseUnit<string>() { message = "선택된 사업장ID가 없습니다.", data = null, code = 404 };

                int adminid = Int32.Parse(context.Items["AdminIdx"].ToString());

                List<AdminPlaceTb>? adminplace = await AdminPlaceInfoRepository.GetMyWorksModel(adminid);
                if(adminplace is [_, ..])
                {
                    AdminPlaceTb? select = adminplace.FirstOrDefault(m => m.PlaceId == placeid);
                    if (select is not null)
                    {
                        PlaceTb? placetb = await PlaceInfoRepository.GetByPlaceInfo(placeid);
                        if(placetb is not null)
                        {
                            // 토큰반환 - 다시만들어야함.
                            authClaims.Add(new Claim("UserIdx", context.Items["UserIdx"].ToString())); // USER 인덱스
                            authClaims.Add(new Claim("Name", context.Items["Name"].ToString())); // 이름
                            authClaims.Add(new Claim("Jti", context.Items["Jti"].ToString()));
                            authClaims.Add(new Claim("AlarmYN", context.Items["AlarmYN"].ToString())); // 알람 받을지 여부
                            authClaims.Add(new Claim("AdminYN", context.Items["AdminYN"].ToString())); // 관리자 여부
                            authClaims.Add(new Claim("UserType", context.Items["UserType"].ToString()));
                            authClaims.Add(new Claim("AdminIdx", context.Items["AdminIdx"].ToString())); // 관리자 인덱스

                            if (context.Items["Role"].ToString() == "시스템관리자")
                            {
                                authClaims.Add(new Claim("Role", "시스템관리자"));
                                authClaims.Add(new Claim(ClaimTypes.Role, "SystemManager"));
                            }

                            if (context.Items["Role"].ToString() == "마스터")
                            {
                                authClaims.Add(new Claim("Role", "마스터"));
                                authClaims.Add(new Claim(ClaimTypes.Role, "Master"));
                            }

                            if (context.Items["Role"].ToString() == "매니저")
                            {
                                authClaims.Add(new Claim("Role", "매니저"));
                                authClaims.Add(new Claim(ClaimTypes.Role, "Manager"));
                            }


                            JObject parse = new JObject(JObject.Parse(context.Items["UserPerms"].ToString()));
                            JObject items = new JObject();

                            /* 메뉴 접근권한 */
                            items.Add("UserPerm_Basic", parse["UserPerm_Basic"].ToString());
                            items.Add("UserPerm_Machine", parse["UserPerm_Machine"].ToString());
                            items.Add("UserPerm_Elec", parse["UserPerm_Elec"].ToString());
                            items.Add("UserPerm_Lift", parse["UserPerm_Lift"].ToString());
                            items.Add("UserPerm_Fire", parse["UserPerm_Fire"].ToString());
                            items.Add("UserPerm_Construct", parse["UserPerm_Construct"].ToString());
                            items.Add("UserPerm_Network", parse["UserPerm_Network"].ToString());
                            items.Add("UserPerm_Beauty", parse["UserPerm_Beauty"].ToString());
                            items.Add("UserPerm_Security", parse["UserPerm_Security"].ToString());
                            items.Add("UserPerm_Material", parse["UserPerm_Material"].ToString());
                            items.Add("UserPerm_Energy", parse["UserPerm_Energy"].ToString());
                            items.Add("UserPerm_User", parse["UserPerm_User"].ToString());
                            items.Add("UserPerm_Voc", parse["UserPerm_Voc"].ToString());
                            jsonConvert = JsonConvert.SerializeObject(items);
                            authClaims.Add(new Claim("UserPerms", jsonConvert));

                            /* VOC 권한 */
                            parse = new JObject(JObject.Parse(context.Items["VocPerms"].ToString()));
                            items = new JObject();
                            
                            items.Add("VocMachine", parse["VocMachine"].ToString()); // 기계민원 처리권한
                            items.Add("VocElec", parse["VocElec"].ToString()); // 전기민원 처리권한
                            items.Add("VocLift", parse["VocLift"]); // 승강민원 처리권한
                            items.Add("VocFire", parse["VocFire"].ToString()); // 소방민원 처리권한
                            items.Add("VocConstruct", parse["VocConstruct"].ToString()); // 건축민원 처리권한
                            items.Add("VocNetwork", parse["VocNetwork"].ToString()); // 통신민원 처리권한
                            items.Add("VocBeauty", parse["VocBeauty"].ToString()); // 미화민원 처리권한
                            items.Add("VocSecurity", parse["VocSecurity"].ToString()); // 보안민원 처리권한
                            items.Add("VocDefault", parse["VocDefault"].ToString()); // 기타 처리권한
                            jsonConvert = JsonConvert.SerializeObject(items);
                            authClaims.Add(new Claim("VocPerms", jsonConvert));
                            
                            /* PLACE 권한 */
                            items = new JObject();
                            items.Add("PlaceIdx", placetb.Id.ToString());
                            items.Add("PlaceName", placetb.Name.ToString());
                            items.Add("PlacePerm_Machine", placetb.PermMachine.ToString());
                            items.Add("PlacePerm_Lift", placetb.PermLift.ToString());
                            items.Add("PlacePerm_Fire", placetb.PermFire.ToString());
                            items.Add("PlacePerm_Construct", placetb.PermConstruct.ToString());
                            items.Add("PlacePerm_Network", placetb.PermNetwrok.ToString());
                            items.Add("PlacePerm_Beauty", placetb.PermBeauty.ToString());
                            items.Add("PlacePerm_Security", placetb.PermSecurity.ToString());
                            items.Add("PlacePerm_Material", placetb.PermMaterial.ToString());
                            items.Add("PlacePerm_Energy", placetb.PermEnergy.ToString());
                            items.Add("PlacePerm_Voc", placetb.PermVoc.ToString());
                            jsonConvert = JsonConvert.SerializeObject(items);
                            authClaims.Add(new Claim("PlacePerms", jsonConvert));

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
                            return new ResponseUnit<string>() { message = "사업장이 존재하지 않습니다.", data = null, code = 404 };
                        }
                    }
                    else
                    {
                        return new ResponseUnit<string>() { message = "해당 관리자는 선택된 사업장의 권한이 없습니다.", data = null, code = 404 };
                    }
                }
                else
                {
                    return new ResponseUnit<string>() { message = "해당 관리자는 선택된 사업장의 권한이 없습니다.", data = null, code = 404 };
                }
            }
            catch(Exception ex)
            {
                return new ResponseUnit<string>() { message = "서버에서 요청을 처리하지 못하였습니다.", data = null, code = 500 };
            }
        }

        /// <summary>
        /// 일반페이지 유저 로그인 서비스
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseUnit<string>?> UserLoginService(LoginDTO? dto)
        {
            List<Claim> authClaims = new List<Claim>();
            string jsonConvert = String.Empty;

            try
            {
                if (dto?.UserID is null)
                    return new ResponseUnit<string>() { message = "아이디를 입력해주세요.", data = null, code = 200 };
                if(dto?.UserPassword is null)
                    return new ResponseUnit<string>() { message = "비밀번호를 입력해주세요.", data = null, code = 200 };

                if (!String.IsNullOrWhiteSpace(dto?.UserID) && !String.IsNullOrWhiteSpace(dto?.UserPassword))
                {
                    UserTb? usertb = await UserInfoRepository.GetUserInfo(dto.UserID, dto.UserPassword);
                    
                    if(usertb is not null)
                    {
                        int? AdminYN = usertb.AdminYn;
                        if(AdminYN == 0) // 일반유저
                        {
                            PlaceTb? placetb = await PlaceInfoRepository.GetByPlaceInfo(usertb.PlaceTbId);
                            
                            if(placetb is not null)
                            {
                                authClaims.Add(new Claim("UserIdx", usertb.Id.ToString())); // + USERID
                                authClaims.Add(new Claim("Name", usertb.Name!.ToString())); // + USERID
                                authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                                authClaims.Add(new Claim("AlarmYN", usertb.AlramYn!.ToString())); // 알람 받을지 여부
                                authClaims.Add(new Claim("AdminYN", usertb.AdminYn!.ToString())); // 관리자 여부
                                authClaims.Add(new Claim("UserType", "User"));
                                
                                authClaims.Add(new Claim("Role", "User"));
                                authClaims.Add(new Claim(ClaimTypes.Role, "User"));

                                JObject items = new JObject();

                                /* 메뉴 접근권한 */
                                items.Add("UserPerm_Basic", usertb.PermBasic);
                                items.Add("UserPerm_Machine", usertb.PermMachine);
                                items.Add("UserPerm_Elec", usertb.PermElec);
                                items.Add("UserPerm_Lift", usertb.PermLift);
                                items.Add("UserPerm_Fire", usertb.PermFire);
                                items.Add("UserPerm_Construct", usertb.PermConstruct);
                                items.Add("UserPerm_Network", usertb.PermNetwork);
                                items.Add("UserPerm_Beauty", usertb.PermBeauty);
                                items.Add("UserPerm_Security", usertb.PermSecurity);
                                items.Add("UserPerm_Material", usertb.PermMaterial);
                                items.Add("UserPerm_Energy", usertb.PermEnergy);
                                items.Add("UserPerm_User", usertb.PermUser);
                                items.Add("UserPerm_Voc", usertb.PermVoc);
                                jsonConvert = JsonConvert.SerializeObject(items);
                                authClaims.Add(new Claim("UserPerms", jsonConvert));
                                
                                items = new JObject();
                                /* VOC 권한 */
                                items.Add("VocMachine", usertb.VocMachine.ToString()); // 기계민원 처리권한
                                items.Add("VocElec", usertb.VocElec.ToString()); // 전기민원 처리권한
                                items.Add("VocLift", usertb.VocLift.ToString()); // 승강민원 처리권한
                                items.Add("VocFire", usertb.VocFire.ToString()); // 소방민원 처리권한
                                items.Add("VocConstruct", usertb.VocConstruct.ToString()); // 건축민원 처리권한
                                items.Add("VocNetwork", usertb.VocNetwork.ToString()); // 통신민원 처리권한
                                items.Add("VocBeauty", usertb.VocBeauty.ToString()); // 미화민원 처리권한
                                items.Add("VocSecurity", usertb.VocSecurity.ToString()); // 보안민원 처리권한
                                items.Add("VocDefault", usertb.VocDefault.ToString()); // 기타 처리권한
                                jsonConvert = JsonConvert.SerializeObject(items);
                                authClaims.Add(new Claim("VocPerms", jsonConvert));

                                /* 사업장 권한 */
                                items.Add("PlaceIdx", usertb.PlaceTbId.ToString()); // 사업장 인덱스
                                items.Add("PlaceName", placetb.Name.ToString()); // 사업장 이름
                                items.Add("PlacePerm_Machine", placetb.PermMachine.ToString()); // 사업장 기계메뉴 권한
                                items.Add("PlacePerm_Lift", placetb.PermLift.ToString()); // 사업장 승강메뉴 권한
                                items.Add("PlacePerm_Fire", placetb.PermFire.ToString()); // 사업장 소방메뉴 권한
                                items.Add("PlacePerm_Construct", placetb.PermConstruct.ToString()); // 사업장 건축메뉴 권한
                                items.Add("PlacePerm_Network", placetb.PermNetwrok.ToString()); // 사업장 통신메뉴 권한
                                items.Add("PlacePerm_Beauty", placetb.PermBeauty.ToString()); // 사업장 미화메뉴 권한
                                items.Add("PlacePerm_Security", placetb.PermSecurity.ToString()); // 사업장 보안메뉴 권한
                                items.Add("PlacePerm_Material", placetb.PermMaterial.ToString()); // 사업장 자재메뉴 권한
                                items.Add("PlacePerm_Energy", placetb.PermEnergy.ToString()); // 사업장 전기메뉴 권한
                                items.Add("PlacePerm_Voc", placetb.PermVoc.ToString()); // 사업장 VOC 권한
                                
                                jsonConvert = JsonConvert.SerializeObject(items);
                                authClaims.Add(new Claim("PlacePerms", jsonConvert));



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
                            else // 잘못된 요청입니다.
                            {
                                return new ResponseUnit<string>() { message = "잘못된 요청입니다.", data = null, code = 404 };
                            }
                        }
                        else // 관리자
                        {
                            // 위에만큼 담는데 (사업장 은 빼고)
                            // Return 201
                            AdminTb? admintb = await AdminUserInfoRepository.GetAdminUserInfo(usertb.Id);

                            if (admintb is not null)
                            {
                                authClaims.Add(new Claim("UserIdx", usertb.Id.ToString())); // USER 인덱스
                                authClaims.Add(new Claim("Name", usertb.Name!.ToString())); // 이름
                                authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                                authClaims.Add(new Claim("AlarmYN", usertb.AlramYn!.ToString())); // 알람 받을지 여부
                                authClaims.Add(new Claim("AdminYN", usertb.AdminYn!.ToString())); // 관리자 여부
                                authClaims.Add(new Claim("UserType", "ADMIN"));
                                authClaims.Add(new Claim("AdminIdx", admintb.Id!.ToString())); // 관리자 인덱스

                                if (admintb.Type == "시스템관리자")
                                {
                                    authClaims.Add(new Claim("Role", "시스템관리자"));
                                    authClaims.Add(new Claim(ClaimTypes.Role, "SystemManager"));
                                }
                                if (admintb.Type == "마스터")
                                {
                                    authClaims.Add(new Claim("Role", "마스터"));
                                    authClaims.Add(new Claim(ClaimTypes.Role, "Master"));
                                }
                                if (admintb.Type == "매니저")
                                {
                                    authClaims.Add(new Claim("Role", "매니저"));
                                    authClaims.Add(new Claim(ClaimTypes.Role, "Manager"));
                                }

                                JObject items = new JObject();

                                /* 메뉴 접근권한 */
                                items.Add("UserPerm_Basic", usertb.PermBasic);
                                items.Add("UserPerm_Machine", usertb.PermMachine);
                                items.Add("UserPerm_Elec", usertb.PermElec);
                                items.Add("UserPerm_Lift", usertb.PermLift);
                                items.Add("UserPerm_Fire", usertb.PermFire);
                                items.Add("UserPerm_Construct", usertb.PermConstruct);
                                items.Add("UserPerm_Network", usertb.PermNetwork);
                                items.Add("UserPerm_Beauty", usertb.PermBeauty);
                                items.Add("UserPerm_Security", usertb.PermSecurity);
                                items.Add("UserPerm_Material", usertb.PermMaterial);
                                items.Add("UserPerm_Energy", usertb.PermEnergy);
                                items.Add("UserPerm_User", usertb.PermUser);
                                items.Add("UserPerm_Voc", usertb.PermVoc);
                                jsonConvert = JsonConvert.SerializeObject(items);
                                authClaims.Add(new Claim("UserPerms", jsonConvert));

                                items = new JObject();
                                /* VOC 권한 */
                                items.Add("VocMachine", usertb.VocMachine.ToString()); // 기계민원 처리권한
                                items.Add("VocElec", usertb.VocElec.ToString()); // 전기민원 처리권한
                                items.Add("VocLift", usertb.VocLift.ToString()); // 승강민원 처리권한
                                items.Add("VocFire", usertb.VocFire.ToString()); // 소방민원 처리권한
                                items.Add("VocConstruct", usertb.VocConstruct.ToString()); // 건축민원 처리권한
                                items.Add("VocNetwork", usertb.VocNetwork.ToString()); // 통신민원 처리권한
                                items.Add("VocBeauty", usertb.VocBeauty.ToString()); // 미화민원 처리권한
                                items.Add("VocSecurity", usertb.VocSecurity.ToString()); // 보안민원 처리권한
                                items.Add("VocDefault", usertb.VocDefault.ToString()); // 기타 처리권한
                                jsonConvert = JsonConvert.SerializeObject(items);
                                authClaims.Add(new Claim("VocPerms", jsonConvert));


                                // JWT 인증 페이로드 사인 비밀키
                                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:authSigningKey"]!));

                                JwtSecurityToken token = new JwtSecurityToken(
                                    issuer: Configuration["JWT:Issuer"],
                                    audience: Configuration["JWT:Audience"],
                                    expires: DateTime.Now.AddDays(1),
                                    claims: authClaims,
                                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

                                string accessToken = new JwtSecurityTokenHandler().WriteToken(token);
                                return new ResponseUnit<string>() { message = "로그인 성공(관리자).", data = accessToken, code = 201 };
                            }
                            else
                            {
                                return new ResponseUnit<string>() { message = "잘못된 요청입니다.", data = null, code = 404 };
                            }
                        }
                    }
                    else
                    {
                        return new ResponseUnit<string>() { message = "잘못된 요청입니다.", data = null, code = 404 };
                    }
                }
                else
                {
                    return new ResponseUnit<string>() { message = "잘못된 요청입니다.", data = null, code = 404 };
                }
            }
            catch (Exception ex)
            {
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="placeidx"></param>
        /// <returns></returns>
        public async ValueTask<ResponseList<ListUser>> GetPlaceUserList(JObject? jobj, int placeid)
        {
            try
            {
                string? UserType = jobj["USERTYPE"].ToString();

                if (UserType is null)
                {
                    return new ResponseList<ListUser>()
                    {
                        message = "잘못된 요청입니다.",
                        data = new List<ListUser>(),
                        code = 401
                    };
                }

                if (UserType.Equals("ADMIN")) // 이사람은 관리자임 - 할당된게 여러개일수도 있음
                {
                    int? AdminIdx = Int32.Parse(jobj["AdminIdx"].ToString());

                    if (AdminIdx is null)
                    {
                        return new ResponseList<ListUser>()
                        {
                            message = "잘못된 요청입니다.",
                            data = new List<ListUser>(),
                            code = 401
                        };
                    }


                    List<AdminPlaceTb>? placelist = await AdminPlaceInfoRepository.GetMyWorksModel(AdminIdx);

                    if (placelist is [_, ..])
                    {
                        // AdminPlaceTB랑 조회해서 --> 넘어온 PlaceIDX가 자기꺼에 있는지 확인하고 있으면 그 PlaceIdx에 해당하는 UserList 반환
                        AdminPlaceTb? adminplace = placelist.FirstOrDefault(m => m.PlaceId == placeid);
                        if (adminplace is not null)
                        {
                            PlaceTb? placetb = await PlaceInfoRepository.GetByPlaceInfo(adminplace.PlaceId);

                            if(placetb is not null) 
                            {
                                // 사업장이 있다 --> 실제 로직
                                List<UserTb>? usertb = await UserInfoRepository.GetPlaceUserList(placetb.Id);
                                
                                if(usertb is [_, ..])
                                {
                                    return new ResponseList<ListUser>()
                                    {
                                        message = "요청이 정상 처리되었습니다.",
                                        data = usertb.Select(e => new ListUser
                                        {
                                            Id = e.Id,
                                            UserId = e.UserId,
                                            Name = e.Name,
                                            Email = e.Email,
                                            Phone = e.Phone,
                                            Type = e.Job,
                                            Status = e.Status,
                                            Created = e.CreateDt.ToString()!
                                        }).ToList(),
                                        code = 200
                                    };
                                }
                                else
                                {
                                    return new ResponseList<ListUser>()
                                    {
                                        message = "요청이 정상 처리되었습니다.",
                                        data = new List<ListUser>(),
                                        code = 200
                                    };
                                }
                            }
                            else
                            {
                                return new ResponseList<ListUser>()
                                {
                                    message = "잘못된 요청입니다.",
                                    data = new List<ListUser>(),
                                    code = 401
                                };
                            }
                        }
                        else
                        {
                            return new ResponseList<ListUser>()
                            {
                                message = "잘못된 요청입니다.",
                                data = new List<ListUser>(),
                                code = 401
                            };
                        }
                    }
                    else
                    {
                        return new ResponseList<ListUser>()
                        {
                            message = "잘못된 요청입니다.",
                            data = new List<ListUser>(),
                            code = 401
                        };
                    }
                }
                else // 일반사용자
                {
                    int? UserIdx = Int32.Parse(jobj["UserIdx"].ToString());
                    
                    if (UserIdx is null)
                    {
                        return new ResponseList<ListUser>()
                        {
                            message = "잘못된 요청입니다.",
                            data = new List<ListUser>(),
                            code = 401
                        };
                    }

                    UserTb? usermodel = await UserInfoRepository.GetUserIndexInfo(UserIdx);

                    if(usermodel is not null)
                    {
                        // user테이블 조회해서 이사람의 placeid가 매개변수 placeid랑 같은지 보고
                        if (usermodel.PlaceTbId == placeid)
                        {
                            // 사업장을 검사한다.
                            PlaceTb? placetb = await PlaceInfoRepository.GetByPlaceInfo(usermodel.PlaceTbId);

                            // 사업장이 있다 --> 실제 로직
                            List<UserTb>? usertb = await UserInfoRepository.GetPlaceUserList(placetb.Id);
                            
                            if (usertb is [_, ..])
                            {
                                return new ResponseList<ListUser>()
                                {
                                    message = "요청이 정상 처리되었습니다.",
                                    data = usertb.Select(e => new ListUser
                                    {
                                        Id = e.Id,
                                        UserId = e.UserId,
                                        Name = e.Name,
                                        Email = e.Email,
                                        Phone = e.Phone,
                                        Type = e.Job,
                                        Status = e.Status,
                                        Created = e.CreateDt.ToString()!
                                    }).ToList(),
                                    code = 200
                                };
                            }
                            else
                            {
                                return new ResponseList<ListUser>()
                                {
                                    message = "요청이 정상 처리되었습니다.",
                                    data = new List<ListUser>(),
                                    code = 200
                                };
                            }
                        }
                        else
                        {
                            return new ResponseList<ListUser>()
                            {
                                message = "잘못된 요청입니다.",
                                data = new List<ListUser>(),
                                code = 401
                            };
                        }
                    }
                    else
                    {
                        return new ResponseList<ListUser>()
                        {
                            message = "잘못된 요청입니다.",
                            data = new List<ListUser>(),
                            code = 401
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseList<ListUser>()
                {
                    message = "서버에서 요청을 처리하지 못하였습니다.",
                    data = new List<ListUser>(),
                    code = 500
                };
            }

        }

   
    }
}
