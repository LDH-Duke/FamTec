using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamTec.Shared.Server.DTO.Admin.Place
{
    public class AllPlaceDTO
    {
        /// <summary>
        /// 사업장ID
        /// </summary>
        public int PlaceID { get; set; }

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
