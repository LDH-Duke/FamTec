using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamTec.Shared.Server.DTO.Admin.Place
{
    /// <summary>
    /// 사업장 등록 화면 DTO
    /// </summary>
    public class AddPlaceDTO
    {
        /// <summary>
        /// 사업장 인덱스
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// 사업장코드
        /// </summary>
        public string? PlaceCd { get; set; }

        /// <summary>
        /// 계약번호
        /// </summary>
        public string? ContractNum { get; set; }

        /// <summary>
        /// 사업장 명
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 비고값
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// 사업장 주소
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// 계약일자
        /// </summary>
        public DateTime? ContractDT { get; set; }

        /// <summary>
        /// 설비메뉴 권한
        /// </summary>
        public sbyte? PermMachine { get; set; }

        /// <summary>
        /// 승강메뉴 권한
        /// </summary>
        public sbyte? PermLift { get; set; }

        /// <summary>
        /// 소방메뉴 권한
        /// </summary>
        public sbyte? PermFire { get; set; }

        /// <summary>
        /// 건축메뉴 권한
        /// </summary>
        public sbyte? PermConstruct { get; set; }

        /// <summary>
        /// 통신메뉴 권한
        /// </summary>
        public sbyte? PermNetwork { get; set; }

        /// <summary>
        /// 미화 권한
        /// </summary>
        public sbyte? PermBeauty { get; set; }
        
        /// <summary>
        /// 보안메뉴 권한
        /// </summary>
        public sbyte? PermSecurity { get; set; }

        /// <summary>
        /// 자재메뉴 권한
        /// </summary>
        public sbyte? PermMaterial { get; set; }

        /// <summary>
        /// 에너지메뉴 권한
        /// </summary>
        public sbyte? PermEnergy { get; set; }
        
        /// <summary>
        /// 해약일자
        /// </summary>
        public DateTime? CancelDT { get; set; }

        /// <summary>
        /// 상태
        /// </summary>
        public sbyte? Status { get; set; }

        /// <summary>
        /// 관리자리스트
        /// </summary>
        public List<ManagerListDTO> AdminList { get; set; } =  new List<ManagerListDTO>();
    }
}