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
    public class UsersDTO
    {
        // 사용자 아이디
        [MaxLength(15)]
        [NotNull]
        [Required(ErrorMessage = "아이디를 입력해주세요.")]
        public string? USERID { get; set; }

        // 비밀번호
        [MaxLength(30)]
        [NotNull]
        [Required(ErrorMessage = "비밀번호는 공백을 사용하실 수 없습니다.")]
        public string? PASSWORD { get; set; }

        // 사용자 이름
        [MaxLength(15)]
        [NotNull]
        [Required(ErrorMessage = "사용자이름을 입력해주세요.")]
        public string? NAME { get; set; }

        // 이메일
        [MaxLength(30)]
        public string? EMAIL { get; set; }

        // 전화번호
        [MaxLength(20)]
        public string? PHONE { get; set; }

        // 건물정보 관리 권한
        public int PERM_BUILDING { get; set; } = 0;

        // 장비관리 권한
        public int PERM_EQUIPMENT { get; set; } = 0;

        // 자재관리 권한
        public int PERM_MATERIAL { get; set; } = 0;

        // 에너지관리 권한
        public int PERM_ENERGY { get; set; } = 0;

        // 행정관리 권한
        public int PERM_OFFICE { get; set; } = 0;

        // 업체관리 권한
        public int PERM_COMP { get; set; } = 0;

        // 공사관리 권한
        public int PERM_CONST { get; set; } = 0;

        // 민원관리 권한
        public int PERM_CLAIM { get; set; } = 0;

        // 시스템연동 권한
        public int PERM_SYS { get; set; } = 0;

        // 입퇴직 관리 권한
        public int PERM_EMPLOYEE { get; set; } = 0;

        // 법정점검 권한
        public int PERM_LAW_CK { get; set; } = 0;

        // 법정교육 권한
        public int PERM_LAW_EDU { get; set; } = 0;

        // 관리자 여부
        public bool? ADMIN_YN { get; set; } = false;

        // 알람수신 여부
        public bool? ALARM_YN { get; set; } = false;

        // 입-재직여부
        public bool? STATUS { get; set; } = true;

    }
}
