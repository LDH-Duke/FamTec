using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamTec.Shared.Server.DTO.Room
{
    public class RoomDTO
    {
        /// <summary>
        /// 공간 테이블 INDEX
        /// </summary>
        public int RoomID { get; set; }

        /// <summary>
        /// 공간 명칭
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 층테이블 인덱스
        /// </summary>
        public int? FloorTBID { get; set; }
    }
}
