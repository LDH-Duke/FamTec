﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamTec.Shared
{
    public class SessionInfo
    {
        /// <summary>
        /// 로그인 후 인덱스
        /// </summary>
        public int? UserIdx { get; set; } = 3;

        /// <summary>
        /// 로그인아이디
        /// </summary>
        public string? UserId { get; set; } = "Admin";

        /// <summary>
        /// 사용자이름
        /// </summary>
        public string? Name { get; set; } = "시스템개발파트";

        /// <summary>
        /// 관리자 여부
        /// </summary>
        public sbyte? AdminYN { get; set; } = 1;

        /// <summary>
        /// 알람여부
        /// </summary>
        public sbyte? AlarmYN { get; set; } = 1;

        /// <summary>
        /// 재직여부
        /// </summary>
        public sbyte? Status { get; set; } = 1;

        /// <summary>
        /// 계정유형
        /// </summary>
        public string? Type { get; set; } = "시스템관리자";

        /// <summary>
        /// 부서명
        /// </summary>
        public string? DepartMentName { get; set; } = "에스텍시스템";

        /// <summary>
        /// 직책
        /// </summary>
        public string? Job { get; set; } = "시스템관리자";
       
    }
}
