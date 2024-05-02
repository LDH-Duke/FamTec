using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("KAKAO_LOGS_TB")]
public partial class KakaoLogsTb
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("RESULT")]
    [StringLength(255)]
    [Unicode(false)]
    public string Result { get; set; } = null!;

    [Column("RECEIVER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Receiver { get; set; }

    [Column("SENDER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Sender { get; set; }

    [Column("CONTENT")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Content { get; set; }

    [Column("CREATE_DT", TypeName = "datetime")]
    public DateTime? CreateDt { get; set; }

    [Column("CREATE_USER")]
    [StringLength(15)]
    [Unicode(false)]
    public string? CreateUser { get; set; }

    [Column("UPDATE_DT", TypeName = "datetime")]
    public DateTime? UpdateDt { get; set; }

    [Column("UPDATE_USER")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UpdateUser { get; set; }

    [Column("DEL_DT", TypeName = "datetime")]
    public DateTime? DelDt { get; set; }

    [Column("DEL_USER")]
    [StringLength(15)]
    [Unicode(false)]
    public string? DelUser { get; set; }

    [Column("DEL_YN")]
    public bool? DelYn { get; set; }
}
