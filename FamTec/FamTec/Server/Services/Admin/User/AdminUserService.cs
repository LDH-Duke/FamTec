using Azure;
using FamTec.Server.Repository.Admin.AdminUser;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace FamTec.Server.Services.Admin.User
{
    public class AdminUserService : IAdminUserService
    {
        private readonly IAdminUserInfoRepository AdminUserInfoRepository;
        
        //delegate ResponseModel<AdminsDTO> DelResponse(string message, AdminsDTO dto, int code);
        //DelResponse ReturnOBJ;
        ResponseOBJ<AdminsDTO> Response;
        Func<string, AdminsDTO, int, ResponseModel<AdminsDTO>> FuncResponseOBJ;
        Func<string, List<AdminsDTO>, int, ResponseModel<AdminsDTO>> FuncResponseList;

        public AdminUserService(IAdminUserInfoRepository _adminuserinforepository)
        {
            this.AdminUserInfoRepository = _adminuserinforepository;
            
            Response = new ResponseOBJ<AdminsDTO>();
            //ReturnOBJ = new DelResponse(Response.RESPMessage);
            FuncResponseOBJ = Response.RESPMessage;
            FuncResponseList = Response.RESPMessageList;
        }

        /// <summary>
        /// ADMIN 전체조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<ResponseModel<AdminsDTO>> GetAllUserListService()
        {
            List<AdminsTb>? result = await AdminUserInfoRepository.GetAllAsync();

            if(result is [_, ..])
            {
                return FuncResponseList("전체데이터 조회 성공", result.Select(e => new AdminsDTO()
                {
                    USERID = e.UserId,
                    PASSWORD = e.Password,
                    NAME = e.Name,
                    EMAIL = e.Email
                }).ToList(), 200);
            }
            else
            {
                //return ReturnOBJ("데이터가 존재하지 않습니다.", null, 200);
                return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
            }
        }

        public async ValueTask<ResponseModel<AdminsDTO>> GetAdminUserService(string adminid)
        {
            if(adminid is not null)
            {
                AdminsTb? result = await AdminUserInfoRepository.GetByAdminInfo(adminid);

                if(result is not null)
                {
                    return FuncResponseOBJ(
                        "데이터 검색 성공.", new AdminsDTO()
                        {
                            NAME = result.Name,
                            PASSWORD = result.Password,
                            USERID = result.UserId,
                            EMAIL = result.Email
                        }, 200);
                }
                else
                {
                    return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
                    //return ResponseOBJ("데이터가 존재하지 않습니다.", null, 200);
                }
            }
            else
            {
                return FuncResponseOBJ("데이터가 비어있습니다.", null, 200);
            }
        }

  
        /// <summary>
        /// 관리자 추가
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<AdminsDTO>> AddAdminUserSerivce(AdminsDTO dto)
        {
            if(dto is not null)
            {
                AdminsTb? model = await AdminUserInfoRepository.GetByAdminInfo(dto.USERID);

                if(model == null) // 없음
                {
                    AdminsTb admintb = new AdminsTb()
                    {
                        UserId = dto.USERID,
                        Password = dto.PASSWORD,
                        Name = dto.NAME,
                        Email = dto.EMAIL,

                        CreateUser = "토큰USER", // 토큰주인
                        CreateDt = DateTime.Now
                    };

                    var result = await AdminUserInfoRepository.AddAsync(admintb);

                    if(result == null)
                    {
                        return FuncResponseOBJ("데이터 추가에 실패하였습니다.", null, 404);
                    }
                    else
                    {
                        return FuncResponseOBJ("관리자 추가에 성공하였습니다.", new AdminsDTO()
                        {
                            USERID = result.UserId,
                            PASSWORD = result.Password,
                            NAME = result.Name,
                            EMAIL = result.Email
                        }, 200);
                    }
                }
                else
                {
                    return FuncResponseOBJ("이미 해당 아이디의 관리자가 존재합니다.", null, 200);
                }
            }
            else
            {
                return FuncResponseOBJ("데이터가 비어있습니다.", null, 404);
            }
        }

        /// <summary>
        /// 관리자 수정
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<AdminsDTO>> UpdateAdminUserService(AdminsDTO dto)
        {
            if(dto is not null) // 넘어온 DTO가 NULL이 아니어야 함.
            {
                AdminsTb? model = await AdminUserInfoRepository.GetByAdminInfo(dto.USERID); // 해당 USERID로 사용자가 있는지 조회

                if(model == null) // 없음
                {
                    return FuncResponseOBJ("데이터가 존재하지 않습니다", null, 404);
                }
                else
                {
                    if (!String.IsNullOrEmpty(dto.USERID))
                        model.UserId = dto.USERID;
                    if (!String.IsNullOrWhiteSpace(dto.PASSWORD))
                        model.Password = dto.PASSWORD;
                    if (!String.IsNullOrWhiteSpace(dto.NAME))
                        model.Name = dto.NAME;
                    if (!String.IsNullOrWhiteSpace(dto.EMAIL))
                        model.Email = dto.EMAIL;

                    model.UpdateDt = DateTime.Now;
                    model.UpdateUser = "토큰USER";

                    bool result = await AdminUserInfoRepository.EditAsync(model);

                    if(result) // 수정성공
                    {
                        return FuncResponseOBJ("데이터 수정 성공", new AdminsDTO()
                        {
                            USERID = model.UserId,
                            PASSWORD = model.Password,
                            NAME = model.Name,
                            EMAIL = model.Email
                        }, 200);
                    }
                    else
                    {
                        return FuncResponseOBJ("데이터 수정 실패", null, 404);
                    }
                }
            }
            else
            {
                return FuncResponseOBJ("데이터가 비어있습니다.", null, 404);
            }
        }

        /// <summary>
        /// 관리자 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<AdminsDTO>> DeleteAdminUserService(AdminsDTO dto)
        {
            if(dto is not null) // 넘어온 DTO가 NULL이 아니어야 함.
            {
                AdminsTb? model = await AdminUserInfoRepository.GetByAdminInfo(dto.USERID); // 해당 USERID로 사용자가 있는지 조회

                if(model == null) // 없음
                {
                    return FuncResponseOBJ("데이터가 존재하지 않습니다.", null, 404);
                }
                else
                {
                    model.DelDt = DateTime.Now;
                    model.DelUser = "토큰USER";
                    model.DelYn = true;

                    bool result = await AdminUserInfoRepository.DeleteAdminIdAsync(model);

                    if(result) // 삭제성공
                    {
                        return FuncResponseOBJ("데이터 삭제 성공",new AdminsDTO()
                                {
                                    USERID = model.UserId,
                                    PASSWORD = model.Password,
                                    NAME = model.Name,
                                    EMAIL = model.Email
                                }
                        ,200);
                    }
                    else
                    {
                        return FuncResponseOBJ("데이터 삭제 실패", null, 404);
                    }
                }
            }
            else
            {
                return FuncResponseOBJ("데이터가 비어있습니다.", null, 404);
            }
        }
    }
}
