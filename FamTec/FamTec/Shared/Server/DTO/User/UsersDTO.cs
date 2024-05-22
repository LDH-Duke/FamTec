using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FamTec.Shared.Server.DTO.User
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
        /// 기본정보등록 권한
        /// </summary>
        public int? PERM_BASIC { get; set; }

        /// <summary>
        /// 설비 권한
        /// </summary>
        public int? PERM_MACHINE { get; set; }

        /// <summary>
        /// 승강 권한
        /// </summary>
        public int? PERM_LIFT { get; set; }

        /// <summary>
        /// 소방 권한
        /// </summary>
        public int? PERM_FIRE { get; set; }
        
        /// <summary>
        /// 건축 권한
        /// </summary>
        public int? PERM_CONSTRUCT { get; set; }
        
        /// <summary>
        /// 통신 권한
        /// </summary>
        public int? PERM_NETWORK { get; set; }
        
        /// <summary>
        /// 미화 권한
        /// </summary>
        public int? PERM_BEAUTY { get; set; }

        /// <summary>
        /// 보안 권한
        /// </summary>
        public int? PERM_SECURITY { get; set; }

        /// <summary>
        /// 자재 권한
        /// </summary>
        public int? PERM_MATERIAL { get; set; }

        /// <summary>
        /// 에너지 권한
        /// </summary>
        public int? PERM_ENERGY { get; set; }

        /// <summary>
        /// 사용자 설정 권한
        /// </summary>
        public int? PERM_USER { get; set; }

        /// <summary>
        /// VOC 권한
        /// </summary>
        public int? PERM_VOC { get; set; }

        /// <summary>
        /// 관리자유무
        /// </summary>
        public sbyte? ADMIN_YN { get; set; }
        
        /// <summary>
        /// 알람유무
        /// </summary>
        public sbyte? ALRAM_YN { get; set; }

        /// <summary>
        /// 재직여부
        /// </summary>
        public sbyte? STATUS { get; set; }
        
        /// <summary>
        /// 직책
        /// </summary>
        public string? JOB { get; set; }

        /// <summary>
        /// 선택된 사업장정보
        /// </summary>
        public int? PLACEID { get; set; }
    }
}
