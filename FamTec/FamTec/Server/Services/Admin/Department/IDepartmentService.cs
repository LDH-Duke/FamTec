using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO.Admin;

namespace FamTec.Server.Services.Admin.Department
{
    public interface IDepartmentService
    {
        public ValueTask<ResponseModel<AddDepartmentDTO>> AddDepartmentService(AddDepartmentDTO? dto, SessionInfo name);

        /* 부서전체조회 */
        public ValueTask<ResponseModel<DepartmentDTO>> GetAllDepartmentService();

        /// <summary>
        /// 부서삭제
        /// </summary>
        /// <param name="index"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<DepartmentDTO>?> DeleteDepartmentService(List<int?> index, SessionInfo? session);
        
        /// <summary>
        /// 부서수정
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public ValueTask<ResponseModel<DepartmentDTO>?> UpdateDepartmentService(DepartmentDTO? dto, SessionInfo? session);

    }
}
