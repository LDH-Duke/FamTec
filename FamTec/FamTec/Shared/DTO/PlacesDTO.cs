using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamTec.Shared.DTO
{
    /// <summary>
    /// 사업장 DTO
    /// </summary>
    public class PlacesDTO
    {
        /// <summary>
        /// 사업장 코드
        /// </summary>
        [MaxLength(25)]
        [NotNull]
        public string PlaceCd { get; set; } = null!;

        /// <summary>
        /// 사업장 명
        /// </summary>
        [NotNull]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 계약번호
        /// </summary>
        [NotNull]
        [MaxLength(45)]
        public string CONTRACT_NUM { get; set; } = null!;

        /// <summary>
        /// 비고
        /// </summary>
        [MaxLength(20)]
        public string NOTE { get; set; }
    }
}
