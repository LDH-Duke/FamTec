﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamTec.Shared.DTO
{
    /// <summary>
    /// 관리자 DTO
    /// </summary>
    public class AdminsDTO
    {
        /// <summary>
        /// 관리자 아이디
        /// </summary>
        [MaxLength(15)]
        [NotNull]
        [Required(ErrorMessage = "아이디를 입력해주세요.")]
        public string? USERID { get; set; }

        /// <summary>
        /// 관리자 비밀번호
        /// </summary>
        [MaxLength(30)]
        public string? PASSWORD { get; set; }

        /// <summary>
        /// 관리자 이름
        /// </summary>
        [MaxLength(15)]
        public string? NAME { get; set; }

        /// <summary>
        /// 관리자 이메일
        /// </summary>
        [MaxLength(30)]
        public string? EMAIL { get; set; }
    }
}
