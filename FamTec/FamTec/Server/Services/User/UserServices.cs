using Azure;
using FamTec.Server.Repository.User;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection.Metadata.Ecma335;

namespace FamTec.Server.Services.User
{
    public class UserServices : IUserServices
    {
        private readonly IUserInfoRepository UserInfoRepository;

        public UserServices(IUserInfoRepository _userinforepository)
        {
            this.UserInfoRepository = _userinforepository;
        }

        /// <summary>
        /// USERID로 조회 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        // userid는 차후 토큰이 될것임.
        public async ValueTask<ResponseObject<UsersDTO>> GetUserService(string userid)
        {
            try
            {
                UsersTb? result = await UserInfoRepository.GetByUserIdAsync(userid);

                if (result is not null)
                {
                    ResponseObject<UsersDTO> model = new()
                    {
                        Data = new List<UsersDTO>()
                        {
                            new UsersDTO()
                            {
                                USERID = result.UserId,
                                PASSWORD = result.Password,
                                NAME = result.Name,
                                EMAIL = result.Email,
                                PHONE = result.Phone,
                                PERM_BUILDING = result.PermBuilding,
                                PERM_EQUIPMENT = result.PermBuilding,
                                PERM_MATERIAL = result.PermMaterial,
                                PERM_ENERGY = result.PermEnergy,
                                PERM_OFFICE = result.PermOffice,
                                PERM_COMP = result.PermComp,
                                PERM_CONST = result.PermConst,
                                PERM_CLAIM = result.PermClaim,
                                PERM_SYS = result.PermSys,
                                PERM_EMPLOYEE = result.PermEmployee,
                                PERM_LAW_CK = result.PermLawCk,
                                PERM_LAW_EDU = result.PermLawEdu,
                                ADMIN_YN = result.AdminYn,
                                ALARM_YN = result.AlarmYn,
                                STATUS = result.Status
                            }
                        },
                        StatusCode = 200
                    };
                    return model;
                }
                else
                {
                    ResponseObject<UsersDTO> model = new()
                    {
                        StatusCode = 200
                    };
                    return model;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// USERID 전체 조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<ResponseObject<UsersDTO>> GetAllUserListService()
        {
            try
            {
                List<UsersTb>? result = await UserInfoRepository.GetAllAsync();

                if (result is [_, ..])
                {
                    ResponseObject<UsersDTO> obj = new()
                    {
                        Message = "성공",
                        Data = result.Select(e => new UsersDTO()
                        {
                            USERID = e.UserId,
                            PASSWORD = e.Password,
                            NAME = e.Name,
                            EMAIL = e.Email,
                            PHONE = e.Phone,
                            PERM_BUILDING = e.PermBuilding,
                            PERM_EQUIPMENT = e.PermBuilding,
                            PERM_MATERIAL = e.PermMaterial,
                            PERM_ENERGY = e.PermEnergy,
                            PERM_OFFICE = e.PermOffice,
                            PERM_COMP = e.PermComp,
                            PERM_CONST = e.PermConst,
                            PERM_CLAIM = e.PermClaim,
                            PERM_SYS = e.PermSys,
                            PERM_EMPLOYEE = e.PermEmployee,
                            PERM_LAW_CK = e.PermLawCk,
                            PERM_LAW_EDU = e.PermLawEdu,
                            ADMIN_YN = e.AdminYn,
                            ALARM_YN = e.AlarmYn,
                            STATUS = e.Status
                        }).ToList(),
                        
                        StatusCode = 200
                    };
                    return obj;
                }
                else
                {
                    ResponseObject<UsersDTO> obj = new()
                    {
                        StatusCode = 200
                    };
                    return obj;
                }
            }
            catch(Exception ex)
            {

                ResponseObject<UsersDTO> obj = new()
                {
                    StatusCode = 500
                };
                return obj;   
            }
            
        }

        // -- 차후 userid는 token 부분이 될것임.
        public async ValueTask<ResponseObject<UsersDTO>> AddUserService(UsersDTO dto, string userid, string placecd)
        {
            if(dto is not null)
            {
                /*
                    model  
                 */

                UsersTb? userchk = await UserInfoRepository.GetByUserIdAsync(userid); // A 사용자 --- E 사업장

                if(userchk == null)
                {
                    // 요청을 보낸 유저가 DB에 없음
                }
                else // 요청을 보낸 유저가 DB에 있음.
                {
                    
                }

                if (userchk != null)
                {
                    //if()


                    UsersTb usertb = new UsersTb()
                    {
                        UserId = dto.USERID,
                        Password = dto.PASSWORD,
                        Name = dto.NAME,
                        Email = dto.EMAIL,
                        Phone = dto.PHONE,
                        PermBuilding = dto.PERM_BUILDING,
                        PermEquipment = dto.PERM_EQUIPMENT,
                        PermMaterial = dto.PERM_MATERIAL,
                        PermEnergy = dto.PERM_ENERGY,
                        PermOffice = dto.PERM_OFFICE,
                        PermComp = dto.PERM_COMP,
                        PermConst = dto.PERM_CONST,
                        PermClaim = dto.PERM_CLAIM,
                        PermSys = dto.PERM_SYS,
                        PermEmployee = dto.PERM_EMPLOYEE,
                        PermLawCk = dto.PERM_LAW_CK,
                        PermLawEdu = dto.PERM_LAW_EDU,
                        AdminYn = dto.ADMIN_YN,
                        AlarmYn = dto.ALARM_YN,
                        Status = dto.STATUS,

                        //PlacecodeCd = searchPlaceCd.PlacecodeCd,
                        //CreateUser = searchPlaceCd.UserId,
                        CreateDt = DateTime.Now
                    };

                    var result = await UserInfoRepository.AddAsync(usertb);
                    
                    if(result is not null)
                    {
                        ResponseObject<UsersDTO> model = new()
                        {
                            Data = new List<UsersDTO>()
                            {
                                new UsersDTO()
                                {
                                    USERID = usertb.UserId,
                                    PASSWORD = usertb.Password,
                                    NAME = usertb.Name,
                                    EMAIL = usertb.Email,
                                    PHONE = usertb.Phone,
                                    PERM_BUILDING = usertb.PermBuilding,
                                    PERM_EQUIPMENT = usertb.PermEquipment,
                                    PERM_MATERIAL = usertb.PermMaterial,
                                    PERM_ENERGY = usertb.PermEnergy,
                                    PERM_OFFICE = usertb.PermOffice,

                                }
                            }
                        };
                    }


                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            else
            {
                throw new ArgumentNullException();
            }

            throw new NotImplementedException();
        }

  



        /*
        public async ValueTask<UsersTb> AddUserService(UsersTb model)
        {
            if(model is not null)
            {
                await UserInfoRepository.AddAsync(model);
            }
            
            return null;
        }
        */
    }
}
