using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamTec.Shared.Client.DTO
{
    public class PlaceDTO
    {
        public bool IsSelect { get; set; } = false;
        public string? 사업장코드 {  get; set; }
        public string? 사업장명 { get; set; } = null;
        public string? 비고 { get; set; } = null;
        public string? 계약번호 { get; set; } = null;
        public DateTime 계약일자 { get; set; }
        public string? 계약상태 { get; set; }
        

    }
}
