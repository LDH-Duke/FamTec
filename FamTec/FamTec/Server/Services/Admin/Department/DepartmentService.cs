﻿using FamTec.Server.Repository.Admin.Departmnet;
using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Model;
using FamTec.Shared.Server.DTO.Admin;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FamTec.Server.Services.Admin.Department
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentInfoRepository DepartmentInfoRepository;

        ResponseOBJ<DepartmentDTO> Response;
        Func<string, DepartmentDTO, int, ResponseModel<DepartmentDTO>> FuncResponseOBJ;
        Func<string, List<DepartmentDTO>, int, ResponseModel<DepartmentDTO>> FuncResponseList;

        ResponseOBJ<string> strResponse;
        Func<string, string, int, ResponseModel<string>> FuncResponseSTR;

        public DepartmentService(IDepartmentInfoRepository _departmentinforepository)
        {
            this.DepartmentInfoRepository = _departmentinforepository;

            this.Response = new ResponseOBJ<DepartmentDTO>();
            this.FuncResponseOBJ = Response.RESPMessage;
            this.FuncResponseList = Response.RESPMessageList;

            this.strResponse = new ResponseOBJ<string>();
            this.FuncResponseSTR = strResponse.RESPMessage;
        }

        /// <summary>
        /// 부서추가
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<DepartmentDTO>> AddDepartmentService(DepartmentDTO? dto, SessionInfo session)
        {
            try
            {
                if(dto is not null && !String.IsNullOrWhiteSpace(session.Name))
                {
                    DepartmentTb? model = await DepartmentInfoRepository.GetDepartmentInfo(dto.Name);

                    if(model is null)
                    {
                        DepartmentTb? tb = new DepartmentTb
                        {
                            Name = dto.Name,
                            CreateDt = DateTime.Now,
                            CreateUser = session.Name,
                            UpdateDt = DateTime.Now,
                            UpdateUser = session.Name
                        };

                        DepartmentTb? result = await DepartmentInfoRepository.AddAsync(tb);

                        if(result is not null)
                        {
                            return FuncResponseOBJ("요청이 정상 처리되었습니다.", new DepartmentDTO
                            {
                                ID = result.Id,
                                Name = result.Name
                            }, 200);
                        }
                        else
                        {
                            return FuncResponseOBJ("요청이 처리되지 않았습니다.", null, 200);
                        }
                    }
                    else
                    {
                        return FuncResponseOBJ("이미 해당 부서가 존재합니다.", null, 200);
                    }
                }
                else
                {
                    return FuncResponseOBJ("요청이 잘못되었습니다.", null, 404);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseOBJ(ex.Message, null, 500);
            }
        }

   

        /// <summary>
        /// 부서 전체조회
        /// </summary>
        /// <returns></returns>
        public async ValueTask<ResponseModel<DepartmentDTO>> GetAllDepartmentService()
        {
            try
            {
                List<DepartmentTb>? model = await DepartmentInfoRepository.GetAllList();
                if(model is [_, ..])
                {
                    return FuncResponseList("요청이 정상처리 되었습니다.", model.Select(e => new DepartmentDTO
                    {
                        ID = e.Id,
                        Name = e.Name
                    }).ToList(), 200);
                }
                else
                {
                    return FuncResponseList("데이터가 존재하지 않습니다.", null, 200);
                }
            }
            catch(Exception ex)
            {
                return FuncResponseOBJ(ex.Message, null, 500);
            }
        }

        /// <summary>
        /// 부서삭제
        /// </summary>
        /// <param name="index"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<string>?> DeleteDepartmentService(List<int>? index, SessionInfo? session)
        {
            try
            {
                if(index is [_, ..] && session is not null)
                {
                    int count = 0;

                    for (int i = 0; i < index.Count; i++)
                    {
                        DepartmentTb? model = await DepartmentInfoRepository.GetDepartmentInfo(index[i]);

                        if(model is not null)
                        {
                            model.DelYn = 1;
                            model.DelDt = DateTime.Now;
                            model.DelUser = session.Name;

                            bool? result = await DepartmentInfoRepository.DeleteDepartmentInfo(model);

                            if (result == true)
                            {
                                count++;
                            }
                        }
                    }
                    return FuncResponseSTR("데이터 삭제완료", count.ToString(), 200);
                }
                else
                {
                    return FuncResponseSTR("요청이 잘못되었습니다.", null, 404);
                }

            }
            catch(Exception ex)
            {
                return FuncResponseSTR("서버에서 요청을 처리하지 못하였습니다.", null, 500);
            }
        }

        /// <summary>
        /// 부서수정
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public async ValueTask<ResponseModel<DepartmentDTO>?> UpdateDepartmentService(DepartmentDTO? dto, SessionInfo? session)
        {
            try
            {
                if(dto is not null && session is not null)
                {
                    DepartmentTb? model = await DepartmentInfoRepository.GetDepartmentInfo(dto.ID);
                    
                    if(model is not null)
                    {
                        DepartmentTb? duplechk = await DepartmentInfoRepository.GetDepartmentInfo(dto.Name);
                        
                        if(duplechk is null)
                        {
                            model.Name = dto.Name;
                            model.UpdateDt = DateTime.Now;
                            model.UpdateUser = session.Name;

                            bool? result = await DepartmentInfoRepository.UpdateDepartmentInfo(model);
                            if (result == true)
                            {
                                return FuncResponseOBJ("데이터 수정이 처리되었습니다.", new DepartmentDTO
                                {
                                    ID = model.Id,
                                    Name = dto.Name
                                }, 200);
                            }
                            else
                            {
                                return FuncResponseOBJ("데이터 수정을 처리하지 못하였습니다.", null, 200);
                            }
                        }
                        else
                        {
                            return FuncResponseOBJ("해당 부서명이 존재합니다.", null, 200);
                        }   
                    }
                    else
                    {
                        return FuncResponseOBJ("해당 인덱스가 존재하지 않습니다.", null, 404);
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
