using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FamTec.Shared.DTO
{
    /// <summary>
    /// USER DTO
    /// </summary>
    public class UsersDTO
    {
        /// <summary>
        /// 사용자 PK 인덱스
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// 사용자 아이디
        /// </summary>
        
        public string? USERID { get; set; }

        /// <summary>
        /// 비밀번호
        /// </summary>
        [MaxLength(30)]
        public string? PASSWORD { get; set; }

        /// <summary>
        /// 사용자이름
        /// </summary>
        [MaxLength(15)]
        public string? NAME { get; set; }

        /// <summary>
        /// 이메일
        /// </summary>
        [MaxLength(30)]
        public string? EMAIL { get; set; }

        /// <summary>
        /// 전화번호
        /// </summary>
        [MaxLength(20)]
        public string? PHONE { get; set; }

        /// <summary>
        /// 건물정보 관리 권한
        /// </summary>
        public int? PERM_BUILDING { get; set; }

        /// <summary>
        /// 장비관리 권한
        /// </summary>
        public int? PERM_EQUIPMENT { get; set; }

        /// <summary>
        /// 자재관리 권한
        /// </summary>
        public int? PERM_MATERIAL { get; set; }

        /// <summary>
        /// 에너지관리 권한
        /// </summary>
        public int? PERM_ENERGY { get; set; }

        /// <summary>
        /// 행정관리 권한
        /// </summary>
        public int? PERM_OFFICE { get; set; }

        /// <summary>
        /// 업체관리 권한
        /// </summary>
        public int? PERM_COMP { get; set; }

        /// <summary>
        /// 공사관리 권한
        /// </summary>
        public int? PERM_CONST { get; set; } = 0;

        /// <summary>
        /// 민원관리 권한
        /// </summary>
        public int? PERM_CLAIM { get; set; } = 0;

        /// <summary>
        /// 시스템연동 권한
        /// </summary>
        public int? PERM_SYS { get; set; } = 0;

        /// <summary>
        /// 입퇴직 관리 권한
        /// </summary>
        public int? PERM_EMPLOYEE { get; set; } = 0;

        /// <summary>
        /// 법정점검 권한
        /// </summary>
        public int? PERM_LAW_CK { get; set; } = 0;

        /// <summary>
        /// 법정교육 권한
        /// </summary>
        public int? PERM_LAW_EDU { get; set; } = 0;

        /// <summary>
        /// 관리자 여부
        /// </summary>
        public int? ADMIN_YN { get; set; } = 0;

        /// <summary>
        /// 알람수신 여부
        /// </summary>
        public sbyte? ALARM_YN { get; set; } = 0;

        /// <summary>
        /// 입-재직여부
        /// </summary>
        public sbyte? STATUS { get; set; } = 1;

        /// <summary>
        /// 선택된 사업장정보
        /// </summary>
        public int? PLACEID { get; set; }
    }
}
