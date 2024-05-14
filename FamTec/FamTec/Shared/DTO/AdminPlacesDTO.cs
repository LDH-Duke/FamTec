using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamTec.Shared.DTO
{
    public class AdminPlacesDTO
    {
        /// <summary>
        /// 관리자 ID
        /// </summary>
        [MaxLength(15)]
        public string UserID { get; set; }

        /// <summary>
        /// 사업장 CODE
        /// </summary>
        [MaxLength(25)]
        public string PlaceCode { get; set; }
    }
}
