using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamTec.Shared.Client.DTO
{
    public class PlaceDetailDTO
    {
        public int Id { get; set; }
        [Display(Name = "사업장 코드")]
        public string? PlaceCd { get; set; }
        [Display(Name = "사업장명")]
        public string? Name { get; set; }
        [Display(Name = "전화번호")]
        public string? Tel { get; set; }
        [Display(Name = "계약번호")]
        public string? ContractNum { get; set; }
        [Display(Name = "계약일자")]
        public DateTime? ContractDt { get; set; }
        [Display(Name = "해약일자")]
        public DateTime? CancelDt { get; set; }
        [Display(Name = "비고")]
        public string? Note { get; set; }
        [Display(Name = "기계설비 권한")]
        public sbyte? PermMachine { get; set; } = 0;
        [Display(Name = "승강설비 권한")]
        public sbyte? PermLift { get; set; } = 0;
        [Display(Name = "소방설비 권한")]
        public sbyte? PermFire { get; set; } = 0;
        [Display(Name = "건축설비 권한")]
        public sbyte? PermConstruct { get; set; } = 0;
        [Display(Name = "통신설비 권한")]
        public sbyte? PermNetwork { get; set; } = 0;
        [Display(Name = "미화설비 권한")]
        public sbyte? PermBeauty { get; set; } = 0;
        [Display(Name = "보안설비 권한")]
        public sbyte? PermSecurity { get; set; } = 0;
        [Display(Name = "자재 권한")]
        public sbyte? PermMaterial { get; set; } = 0;
        [Display(Name = "에너지 권한")]
        public sbyte? PermEnergy { get; set; } = 0;
        [Display(Name = "민원 권한")]
        public sbyte? PermVoc { get; set; } = 0;
        public List<ManagerDTO> MnagerList { get; set; } = new List<ManagerDTO>();
    }
}
