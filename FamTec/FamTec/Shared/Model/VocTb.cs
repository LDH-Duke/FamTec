using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("voc_tb")]
[Index("BuildingTbId", Name = "FK_BULDING_202406141619")]
public partial class VocTb
{
    [Key]
    [Column("ID", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("NAME")]
    [StringLength(255)]
    public string? Name { get; set; }

    [Column("TITLE")]
    [StringLength(255)]
    public string? Title { get; set; }

    [Column("CONTENT")]
    [StringLength(255)]
    public string? Content { get; set; }

    [Column("PHONE")]
    [StringLength(255)]
    public string? Phone { get; set; }

    [Column("STATUS", TypeName = "int(11)")]
    public int? Status { get; set; }

    [Column("TYPE", TypeName = "int(11)")]
    public int? Type { get; set; }

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

    [Column("DEL_YN", TypeName = "tinyint(4)")]
    public sbyte? DelYn { get; set; }

    [StringLength(255)]
    public string? Image1 { get; set; }

    [StringLength(255)]
    public string? Image2 { get; set; }

    [StringLength(255)]
    public string? Image3 { get; set; }

    [Column(TypeName = "int(11)")]
    public int? BuildingTbId { get; set; }

    [Column("COMPLETE_TIME", TypeName = "datetime")]
    public DateTime? CompleteTime { get; set; }

    [Column("TOTAL_TIME", TypeName = "datetime")]
    public DateTime? TotalTime { get; set; }

    [InverseProperty("VocTb")]
    public virtual ICollection<AlarmTb> AlarmTbs { get; set; } = new List<AlarmTb>();

    [ForeignKey("BuildingTbId")]
    [InverseProperty("VocTbs")]
    public virtual BuildingTb? BuildingTb { get; set; }
}
