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
        private ITokenComm TokenComm;

        public UserService(IUserInfoRepository _userinforepository,
            IAdminUserInfoRepository _adminuserinforepository,
            IDepartmentInfoRepository _departmentinforepository,
            IAdminPlacesInfoRepository _adminplaceinforepository,
            IPlaceInfoRepository _placeinforpeository,
            IConfiguration _configuration,
            ITokenComm _tokencomm,
            ILogService _logservice)
        {
            this.UserInfoRepository = _userinforepository;
            this.AdminUserInfoRepository = _adminuserinforepository;
            this.DepartmentInfoRepository = _departmentinforepository;
            this.AdminPlaceInfoRepository = _adminplaceinforepository;
            this.PlaceInfoRepository = _placeinforpeository;

            this.Configuration = _configuration;
            this.TokenComm = _tokencomm;
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
                        //authClaims.Add(new Claim("AlarmYN", usertb.AlramYn!.ToString())); // AlarmYN

                        JObject items = new JObject();
                        
                        /* 메뉴접근권한 */
                        items.Add("User_PermBasic", usertb.PermBasic.ToString()); // 기본정보관리메뉴 접근권한
                        items.Add("User_PermMachine", usertb.PermMachine.ToString()); // 기계메뉴 접근권한
                        items.Add("User_PermElec", usertb.PermElec.ToString()); // 전기메뉴 접근권한
                        items.Add("User_PermLift", usertb.PermLift.ToString()); // 승강메뉴 접근권한
                        items.Add("User_PermFire", usertb.PermFire.ToString()); // 소방메뉴 접근권한
                        items.Add("User_PermConstruct", usertb.PermConstruct.ToString()); // 건축메뉴 접근권한
                        items.Add("User_PermNetwork", usertb.PermNetwork.ToString()); // 통신메뉴 접근권한
                        items.Add("User_PermBeauty", usertb.PermBeauty.ToString()); // 미화메뉴 접근권한
                        items.Add("User_PermSecurity", usertb.PermSecurity.ToString()); // 보안메뉴 접근권한
                        items.Add("User_PermMaterial", usertb.PermMaterial.ToString()); // 자재관리메뉴 접근권한
                        items.Add("User_PermEnergy", usertb.PermEnergy.ToString()); // 에너지관리메뉴 접근권한
                        items.Add("User_PermUser", usertb.PermUser.ToString()); // 사용자관리메뉴 접근권한
                        items.Add("User_PermVoc", usertb.PermVoc.ToString()); // 민원관리메뉴 접근권한
                        items.Add("User_PermAlarmYN", usertb.AlramYn.ToString()); // 알람유무
                        jsonConvert = JsonConvert.SerializeObject(items);
                        authClaims.Add(new Claim("UserPerms", jsonConvert));

                        /* VOC 권한 */
                        items = new JObject();
                        items.Add("VocMachine", usertb.VocMachine.ToString()); // 기계민원 처리권한
                        items.Add("VocElec", usertb.VocElec.ToString()); // 전기민원 처리권한
                        items.Add("VocLift", usertb.VocLift.ToString()); // 승강민원 처리권한
                        items.Add("VocFire", usertb.VocFire.ToString()); // 소방민원 처리권한
                        items.Add("VocConstruct", usertb.VocConstruct.ToString()); // 건축민원 처리권한
                        items.Add("VocNetwork", usertb.VocNetwork.ToString()); // 통신민원 처리권한
                        items.Add("VocBeauty", usertb.VocBeauty.ToString()); // 미화민원 처리권한
                        items.Add("VocSecurity", usertb.VocSecurity.ToString()); // 보안민원 처리권한
                        items.Add("VocDefault", usertb.VocDefault.ToString()); // 기타 처리권한
                        
                        if(usertb.PlaceTbId is not null)
                            items.Add("VocPlaceId", usertb.PlaceTbId.ToString()); // 할당된 사업장ID

                        jsonConvert = JsonConvert.SerializeObject(items);
                        authClaims.Add(new Claim("VocPerms", jsonConvert));


                        if (usertb.AdminYn == 1) // 관리자가 로그인 했을경우
                        {
                            AdminTb? admintb = await AdminUserInfoRepository.GetAdminUserInfo(usertb.Id);

                            if (admintb is not null)
                            {
                                authClaims.Add(new Claim("USERTYPE", "ADMIN"));
                                authClaims.Add(new Claim("AdminIdx", admintb.Id.ToString()));

                                if (admintb.Type == "시스템관리자")
                                {
                                    authClaims.Add(new Claim("JOB", "시스템관리자"));
                                    authClaims.Add(new Claim(ClaimTypes.Role, "SystemManager"));
                                }
                                if (admintb.Type == "마스터")
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
                                            placeperm.Add("Place_PermMachine", placetb[i].PermMachine);
                                            placeperm.Add("Place_PermLift", placetb[i].PermLift);
                                            placeperm.Add("Place_PermFire", placetb[i].PermFire);
                                            placeperm.Add("Place_PermConstruct", placetb[i].PermConstruct);
                                            placeperm.Add("Place_PermNetwork", placetb[i].PermNetwrok);
                                            placeperm.Add("Place_PermBeauty", placetb[i].PermBeauty);
                                            placeperm.Add("Place_PermSecurity", placetb[i].PermSecurity);
                                            placeperm.Add("Place_PermMaterial", placetb[i].PermMaterial);
                                            placeperm.Add("Place_PermEnergy", placetb[i].PermEnergy);
                                            placeperm.Add("Place_PermVoc", placetb[i].PermVoc);

                                            placelist.Add(placetb[i].Name, placeperm);
                                        }

                                        jsonConvert = JsonConvert.SerializeObject(placelist);
                                        authClaims.Add(new Claim("PlacePerms", jsonConvert));
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
                                return new ResponseUnit<string>() { message = "로그인 실패(관리자).", data = null, code = 200 };
                            }
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
                                placeperm.Add("Place_PermMachine", userplace.PermMachine);
                                placeperm.Add("Place_PermLift", userplace.PermLift);
                                placeperm.Add("Place_PermFire", userplace.PermFire);
                                placeperm.Add("Place_PermConstruct", userplace.PermConstruct);
                                placeperm.Add("Place_PermNetwork", userplace.PermNetwrok);
                                placeperm.Add("Place_PermBeauty", userplace.PermBeauty);
                                placeperm.Add("Place_PermSecurity", userplace.PermSecurity);
                                placeperm.Add("Place_PermMaterial", userplace.PermMaterial);
                                placeperm.Add("Place_PermEnergy", userplace.PermEnergy);
                                placeperm.Add("Place_PermVoc", userplace.PermVoc);

                                placelist.Add(userplace.Name, placeperm);
                            }

                            jsonConvert = JsonConvert.SerializeObject(placelist);
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
