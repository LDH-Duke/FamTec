using Azure.Core;
using FamTec.Server.Repository.Admin.AdminPlaces;
using FamTec.Server.Repository.Admin.AdminUser;
using FamTec.Server.Repository.Admin.Departmnet;
using FamTec.Server.Repository.Place;
using FamTec.Server.Repository.User;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO;
using FamTec.Shared.Server.DTO.Admin;
using FamTec.Shared.Server.DTO.Admin.Place;
using FamTec.Shared.Server.DTO.Login;
using FamTec.Shared.Server.DTO.Place;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        ResponseOBJ<string> ResponseSTR;
        Func<string, string, int, ResponseModel<string>> FuncResponseSTR;
        Func<string, List<string>, int, ResponseModel<string>> FuncResponseSTRList;

        ResponseOBJ<AddManagerDTO> ResponseAdd;
        Func<string, AddManagerDTO, int, ResponseModel<AddManagerDTO>> FuncResponseAdd;

        ResponseOBJ<int?> ResponseINT;
        Func<string, int?, int, ResponseModel<int?>> FuncResponseINT;


        public AdminAccountService(IUserInfoRepository _userinfoRepository,
            IAdminUserInfoRepository _admininfoRepository,
            IDepartmentInfoRepository _departmentinfoRepository,
            IAdminPlacesInfoRepository _adminplaceinforepository,
            IConfiguration _configuration)
        {
            this.UserInfoRepository = _userinfoRepository;
            this.AdminUserInfoRepository = _admininfoRepository;
            this.DepartmentInfoRepository = _departmentinfoRepository;
            this.AdminPlaceInfoRepository = _adminplaceinforepository;


            Configuration = _configuration;

            ResponseSTR = new ResponseOBJ<string>();
            FuncResponseSTR = ResponseSTR.RESPMessage;
            FuncResponseSTRList = ResponseSTR.RESPMessageList;

            ResponseAdd = new ResponseOBJ<AddManagerDTO>();
            FuncResponseAdd = ResponseAdd.RESPMessage;

            ResponseINT = new ResponseOBJ<int?>();
            FuncResponseINT = ResponseINT.RESPMessage;
        }

        /// <summary>
        /// 관리자 접속화면 서비스
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<string>> AdminLoginService(LoginDTO? dto)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(dto?.UserID) && !string.IsNullOrWhiteSpace(dto?.UserPassword))
                {
                    UserTb? usertb = await UserInfoRepository.GetUserInfo(dto.UserID, dto.UserPassword);

                    if(usertb is not null)
                    {
                        if(usertb.AdminYn == 1)
                        {
                            AdminTb? admintb = await AdminUserInfoRepository.GetAdminUserInfo(usertb.Id);

                            if(admintb is not null)
                            {
                                DepartmentTb? departmenttb = await DepartmentInfoRepository.GetDepartmentInfo(admintb.DepartmentTbId);
                                if(departmenttb is not null)
                                {
                                    List<Claim> temp = new List<Claim>
                                    {
                                        new Claim("temp","123")
                                    };

                                    // 로그인성공
                                    List<Claim> authClaims = new List<Claim>
                                    {
                                        new Claim("UserIdx",usertb.Id.ToString()),
                                        new Claim(ClaimTypes.NameIdentifier, usertb.Name!),
                                        new Claim("AdminIdx",admintb.Id.ToString()),
                                        new Claim("DepartIdx",admintb.DepartmentTbId.ToString()!),
                                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                    };

                                    
                                    if (admintb.Type == "시스템관리자")
                                    {
                                        authClaims.Add(new Claim(ClaimTypes.Role, "SystemManager"));
                                    }
                                    if(admintb.Type == "마스터")
                                    {
                                        authClaims.Add(new Claim(ClaimTypes.Role, "Master"));
                                    }
                                    if(admintb.Type =="매니저")
                                    {
                                        authClaims.Add(new Claim(ClaimTypes.Role, "Manager"));
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
                                    return FuncResponseSTR("로그인 성공", accessToken, 200);
                                }
                                else
                                {
                                    // 정보가 잘못되었습니다.
                                    return FuncResponseSTR("로그인 실패", "로그인 정보가 잘못되었습니다.", 200);
                                }
                            }
                            else
                            {
                                // 관리자 아님
                                return FuncResponseSTR("로그인 실패", "로그인 정보가 잘못되었습니다.", 200);
                            }
                        }
                        else
                        {
                            // 관리자 아님
                            return FuncResponseSTR("로그인 실패", "로그인 정보가 잘못되었습니다.", 200);
                        }
                    }
                    else
                    {
                        // 아이디가 존재하지 않음
                        return FuncResponseSTR("로그인 실패", "해당 아이디가 존재하지 않습니다.", 200);
                    }
                }
                else
                {
                    // 요청이 잘못됨
                    return FuncResponseSTR("로그인 실패", "요청 정보가 잘못되었습니다.", 404);
                }
            }
            catch (Exception ex) 
            {
                return FuncResponseSTR("로그인 실패", "서버에서 요청을 처리하지 못하였습니다.", 500);
            }
        }

        /// <summary>
        /// 관리자 아이디 생성 서비스
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public async ValueTask<ResponseUnit<AdminTb>?> AdminRegisterService(AddManagerDTO? dto)
        {
            try
            {
                if(dto is not null)
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
                        Status = 1
                    };

                    UserTb? userresult = await UserInfoRepository.AddAsync(usermodel);

                    if (userresult is not null)
                    {
                        AdminTb? adminmodel = new AdminTb();
                        /*
                        if (session.Type == "시스템관리자")
                            adminmodel.Type = "마스터";
                        if (session.Type == "마스터")
                            adminmodel.Type = "매니저";
                        */
                        //adminmodel.CreateDt = DateTime.Now;
                        //adminmodel.CreateUser = session.Name;
                        //adminmodel.UpdateDt = DateTime.Now;
                        //adminmodel.UpdateUser = session.Name;
                        adminmodel.DelYn = 0;
                        adminmodel.UserTbId = userresult.Id;
                        adminmodel.DepartmentTbId = dto.DepartmentId;

                        AdminTb? adminresult = await AdminUserInfoRepository.AddAdminUserInfo(adminmodel);
                        //return (adminmodel, 200);
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
                return new ResponseUnit<AdminTb> { message = "서버에서 요청을 처리하지 못하였습니다.", data = new AdminTb(), code = 500 };
            }
        }

        public async ValueTask<ResponseUnit<int>?> DeleteAdminService(List<int> adminid)
        {
            int count = 0;
            bool?[] isStep;
            try
            {
                if(adminid is [_, ..])
                {

                    for (int i=0;i<adminid.Count;i++)
                    {
                        isStep = new bool?[3];

                        AdminTb? admintb = await AdminUserInfoRepository.GetAdminUserInfo(adminid[i]);

                        if (admintb is not null)
                        {
                            admintb.DelYn = 1;
                            admintb.DelDt = DateTime.Now;

                            bool? deleteresult = await AdminUserInfoRepository.DeleteAdminInfo(admintb);
                            
                            if (deleteresult == true)
                            {
                                isStep[0] = deleteresult;

                                UserTb? usertb = await UserInfoRepository.GetUserIndexInfo(admintb.UserTbId);

                                if (usertb is not null)
                                {
                                    usertb.DelYn = 1;
                                    usertb.DelDt = DateTime.Now;

                                    bool? delresult = await UserInfoRepository.DeleteUserInfo(usertb);

                                    if (delresult == true)
                                    {
                                        isStep[1] = delresult;

                                        List<AdminPlaceTb>? adminplacetb = await AdminPlaceInfoRepository.GetMyWorksModel(admintb.Id);

                                        
                                        if (adminplacetb is [_, ..])
                                        {
                                            bool? result = await AdminPlaceInfoRepository.DeleteMyWorks(adminplacetb);
                                            
                                            isStep[2] = result;


                                            if (isStep[0] == true && isStep[1] == true && isStep[2] == true)
                                                count++;
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
                return new ResponseUnit<int> { message = "서버에서 요청을 처리하지 못하였습니다.", data = count, code = 500 };
            }
        }
    }
}
