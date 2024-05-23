using FamTec.Server.Repository.Admin.Departmnet;
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

        public DepartmentService(IDepartmentInfoRepository _departmentinforepository)
        {
            this.DepartmentInfoRepository = _departmentinforepository;

            this.Response = new ResponseOBJ<DepartmentDTO>();
            this.FuncResponseOBJ = Response.RESPMessage;
            this.FuncResponseList = Response.RESPMessageList;
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
                            Id = dto.ID,
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
    }
}
