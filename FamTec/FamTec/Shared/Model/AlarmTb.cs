using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("alarm_tb")]
[Index("UserTbId", Name = "FK_USER_202406141623")]
[Index("VocTbId", Name = "FK_VOC_202406141624")]
public partial class AlarmTb
{
    [Key]
    [Column("ID", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("CREATE_DT", TypeName = "datetime")]
    public DateTime? CreateDt { get; set; }

    [Column("CREATE_USER")]
    [StringLength(15)]
    public string? CreateUser { get; set; }

    [Column("UPDATE_DT", TypeName = "datetime")]
    public DateTime? UpdateDt { get; set; }

    [Column("UPDATE_USER")]
    [StringLength(15)]
    public string? UpdateUser { get; set; }

    [Column("DEL_DT", TypeName = "datetime")]
    public DateTime? DelDt { get; set; }

    [Column("DEL_USER")]
    [StringLength(15)]
    public string? DelUser { get; set; }

    [Column("DEL_YN")]
    public bool? DelYn { get; set; }

    [Column(TypeName = "int(11)")]
    public int? UserTbId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? VocTbId { get; set; }

    [ForeignKey("UserTbId")]
    [InverseProperty("AlarmTbs")]
    public virtual UserTb? UserTb { get; set; }

    [ForeignKey("VocTbId")]
    [InverseProperty("AlarmTbs")]
    public virtual VocTb? VocTb { get; set; }
}
