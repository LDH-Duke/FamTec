using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamTec.Shared.Server.DTO.Admin
{
    public class DepartmentDTO
    {
        /// <summary>
        /// 부서 인덱스
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 부서명
        /// </summary>
        public string? Name { get; set; }

    }
}
