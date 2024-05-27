using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamTec.Shared.Server.Unit
{
    public class UnitsDTO
    {
        /// <summary>
        /// 인덱스
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 단위명
        /// </summary>
        public string? Unit { get; set; }

        /// <summary>
        /// 사업장코드
        /// </summary>
        public int? PlaceCode { get; set; }


    }
}
