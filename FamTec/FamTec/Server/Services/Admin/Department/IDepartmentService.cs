﻿using FamTec.Shared;
using FamTec.Shared.DTO;
using FamTec.Shared.Server.DTO.Admin;

namespace FamTec.Server.Services.Admin.Department
{
    public interface IDepartmentService
    {
        public ValueTask<ResponseModel<DepartmentDTO>> AddDepartmentService(DepartmentDTO? dto, SessionInfo name);

        /* 부서전체조회 */
        public ValueTask<ResponseModel<DepartmentDTO>> GetAllDepartmentService();
    }
}
