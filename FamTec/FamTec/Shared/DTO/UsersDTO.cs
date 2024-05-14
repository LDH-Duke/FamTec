using FamTec.Shared.Model;
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
    /// USER DTO
    /// </summary>
    public class UsersDTO
    {
        /// <summary>
        /// 사용자 아이디
        /// </summary>
        [MaxLength(15)]
        [NotNull]
        [Required(ErrorMessage = "아이디를 입력해주세요.")]
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
        public int? PERM_CONST { get; set; }

        /// <summary>
        /// 민원관리 권한
        /// </summary>
        public int? PERM_CLAIM { get; set; }

        /// <summary>
        /// 시스템연동 권한
        /// </summary>
        public int? PERM_SYS { get; set; }

        /// <summary>
        /// 입퇴직 관리 권한
        /// </summary>
        public int? PERM_EMPLOYEE { get; set; }

        /// <summary>
        /// 법정점검 권한
        /// </summary>
        public int? PERM_LAW_CK { get; set; }

        /// <summary>
        /// 법정교육 권한
        /// </summary>
        public int? PERM_LAW_EDU { get; set; }

        /// <summary>
        /// 관리자 여부
        /// </summary>
        public bool? ADMIN_YN { get; set; } = false;

        /// <summary>
        /// 알람수신 여부
        /// </summary>
        public bool? ALARM_YN { get; set; } = false;

        /// <summary>
        /// 입-재직여부
        /// </summary>
        public bool? STATUS { get; set; } = true;

        /// <summary>
        /// 선택된 사업장정보
        /// </summary>
        public string? PLACECODE { get; set; }
    }
}
