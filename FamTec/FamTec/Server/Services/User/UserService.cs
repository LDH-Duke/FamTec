using Azure.Core;
using FamTec.Server.Repository.Admin.AdminPlaces;
using FamTec.Server.Repository.Admin.AdminUser;
using FamTec.Server.Repository.Admin.Departmnet;
using FamTec.Server.Repository.Place;
using FamTec.Server.Repository.User;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO.Login;
using FamTec.Shared.Server.DTO.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop.Infrastructure;
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


        ResponseOBJ<UsersDTO> Response;

        Func<string, UsersDTO, int, ResponseModel<UsersDTO>> FuncResponseOBJ;
        Func<string, List<UsersDTO>, int, ResponseModel<UsersDTO>> FuncResponseList;

        ResponseOBJ<string> ResponseSTR;
        Func<string, string, int, ResponseModel<string>> FuncResponseSTR;


        public UserService(IUserInfoRepository _userinforepository,
            IAdminUserInfoRepository _adminuserinforepository,
            IDepartmentInfoRepository _departmentinforepository,
            IAdminPlacesInfoRepository _adminplaceinforepository,
            IConfiguration _configuration)
        {
            this.UserInfoRepository = _userinforepository;
            this.AdminUserInfoRepository = _adminuserinforepository;
            this.DepartmentInfoRepository = _departmentinforepository;
            this.AdminPlaceInfoRepository = _adminplaceinforepository;
          

            Response = new ResponseOBJ<UsersDTO>();
            FuncResponseOBJ = Response.RESPMessage;
            FuncResponseList = Response.RESPMessageList;

            ResponseSTR = new ResponseOBJ<string>();
            FuncResponseSTR = ResponseSTR.RESPMessage;

        }

        /// <summary>
        /// 일반페이지 로그인 서비스
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<string>?> UserLoginService(LoginDTO? dto)
        {
            try
            {
                if(!String.IsNullOrWhiteSpace(dto?.UserID) && !String.IsNullOrWhiteSpace(dto?.UserPassword))
                {
                    UserTb? usertb = await UserInfoRepository.GetUserInfo(dto.UserID, dto.UserPassword);

                    if(usertb is not null)
                    {
                        if(usertb.AdminYn == 1)
                        {
                            // 관리자가 로그인 했을경우
                            AdminTb? admintb = await AdminUserInfoRepository.GetAdminUserInfo(usertb.Id);
                            if(admintb is not null)
                            {
                                // 관리자 테이블에 정상접근 됐을때
                                DepartmentTb? departmenttb = await DepartmentInfoRepository.GetDepartmentInfo(admintb.DepartmentTbId);

                                if(departmenttb is not null)
                                {
                                    // 관리자가 사업장 반환
                                    List<AdminPlaceTb>? adminplacetb = await AdminPlaceInfoRepository.GetMyWorksModel(admintb.Id);

                                    if(adminplacetb is [_, ..])
                                    {
                                        List<PlaceTb>? placetb = await AdminPlaceInfoRepository.GetMyWorksDetails(adminplacetb);

                                        if(placetb is [_, ..])
                                        {
                                            List<Claim> authClaims = new List<Claim>
                                            {
                                                new Claim("UserIdx", usertb.Id.ToString()),
                                                new Claim(ClaimTypes.NameIdentifier, usertb.Name!),
                                                new Claim("AdminIdx", admintb.Id.ToString()),
                                                new Claim("DepartIdx", admintb.DepartmentTbId.ToString()!),
                                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                                            };

                                            if(admintb.Type == "시스템관리자")
                                            {
                                                authClaims.Add(new Claim(ClaimTypes.Role, "SystemManager"));
                                                
                                            }
                                            if(admintb.Type == "마스터")
                                            {
                                                authClaims.Add(new Claim(ClaimTypes.Role, "Master"));
                                            }
                                            if(admintb.Type == "매니저")
                                            {
                                                authClaims.Add(new Claim(ClaimTypes.Role, "Manager"));
                                            }

                                            if (placetb.Count > 0)
                                            {
                                                JObject json = new JObject();
                                                for (int i = 0; i < placetb.Count; i++)
                                                {
                                                    json.Add(placetb[i].Name!, placetb[i].Id);
                                                }

                                                string jsonConvert = JsonConvert.SerializeObject(json);
                                                authClaims.Add(new Claim("Places", jsonConvert));
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

                                            return FuncResponseSTR("로그인 성공", accessToken, 200);
                                        }
                                        else
                                        {
                                            return FuncResponseSTR("로그인 실패", null, 200);
                                        }
                                    }
                                    else
                                    {
                                        return FuncResponseSTR("로그인 실패", null, 200);
                                    }
                                }
                                else
                                {
                                    return FuncResponseSTR("로그인 실패", null, 200);
                                }

                            }
                            else
                            {
                                // 관리자 테이블에 정상접근 못했을때
                                return FuncResponseSTR("로그인 실패", null, 200);
                            }
                        }
                        else
                        {
                            // 일반유저가 로그인 했을 경우
                            
                            /*
                                -- TODO -- 
                             
                             */
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    // 요청이 잘못되었습니다.
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// 아이디 중복검사
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<UsersDTO>?> UserIdCheck(string? userid)
        {
            try
            {
                if(!String.IsNullOrWhiteSpace(userid))
                {
                    UserTb? model = await UserInfoRepository.UserIdCheck(userid);
                    if(model is not null)
                    {
                        return FuncResponseOBJ("해당아이디가 존재합니다.", new UsersDTO()
                        {
                            ID = model.Id,
                            USERID = model.UserId,
                            NAME = model.Name
                        },200);
                    }
                    else
                    {
                        return FuncResponseOBJ("해당아이디가 존재하지 않습니다.", null, 200);
                    }
                }
                else
                {
                    return FuncResponseOBJ("요청이 잘못되었습니다.", null, 404);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseOBJ("서버에서 요청을 처리하지 못하였습니다.", null, 500);
            }
        }

   
    }
}
