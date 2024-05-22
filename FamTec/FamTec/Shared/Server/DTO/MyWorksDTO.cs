using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamTec.Shared.DTO
{
    public class MyWorksDTO
    {
        /// <summary>
        /// 사업장인덱스
        /// </summary>
        public string? PlaceIndex { get; set; }
        
        /// <summary>
        /// 사업장 이름
        /// </summary>
        public string? PlaceName { get; set; }

        /// <summary>
        /// 계약번호
        /// </summary>
        public string? ContractNum { get; set; }

        /// <summary>
        /// 계약일자
        /// </summary>
        public DateTime? ContractDT { get; set; }

        /// <summary>
        /// 해약일자
        /// </summary>
        public DateTime? CancelDT { get; set; }

        /// <summary>
        /// 상태
        /// </summary>
        public sbyte? Status { get; set; }
        
  
    }
}
