using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamTec.Shared.Server.DTO.Admin
{
    public class AdminPlaceDTO
    {
        /// <summary>
        /// ADMIN PLACE 테이블의 ID
        /// </summary>
        public int AdminPlaceTBID { get; set; }

        /// <summary>
        /// ADMIN PLACE 테이블의 ADMIN USER ID
        /// </summary>
        public int? AdminPlaceUserTBID { get; set; }

        /// <summary>
        /// ADMIN PLACE 테이블의 PLACE ID
        /// </summary>
        public int? PlaceTBID { get; set; }

        /// <summary>
        /// 사업장CODE
        /// </summary>
        public string? PlaceCD { get; set; }

        /// <summary>
        /// 사업장명
        /// </summary>
        public string? Name { get; set; }

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
        public sbyte? status { get; set; }
    }
}
