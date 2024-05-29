using Microsoft.Identity.Client.TelemetryCore.TelemetryClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamTec.Shared.Server.DTO.Admin.Place
{
    public class ManagerListDTO
    {
        /// <summary>
        /// USER 테이블 INDEX
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 관리자 이름
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// ADMIN 테이블 INDEX
        /// </summary>
        public int? AdminID { get; set; }

        /// <summary>
        /// ADMIN 테이블 계정유형
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// 부서인덱스
        /// </summary>
        public int? DepartmentIdx { get; set; }

        /// <summary>
        /// 부서명
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// 전화번호
        /// </summary>
        public string? Tel { get; set; }
    }
}
