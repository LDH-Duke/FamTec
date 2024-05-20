using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("energy_usage_tb")]
[Index("MeterItemTbId", Name = "fk_ENERGY_USAGE_TB_METER_ITEM_TB1_idx")]
public partial class EnergyUsageTb
{
    [Key]
    [Column("ID", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("USAGE")]
    public float? Usage { get; set; }

    [Column("METER_DT", TypeName = "datetime")]
    public DateTime? MeterDt { get; set; }

    [Column("CREATE_DT", TypeName = "datetime")]
    public DateTime? CreateDt { get; set; }

    [Column("CREATE_USER")]
    [StringLength(45)]
    public string? CreateUser { get; set; }

    [Column("UPDATE_DT", TypeName = "datetime")]
    public DateTime? UpdateDt { get; set; }

    [Column("UPDATE_USER")]
    [StringLength(45)]
    public string? UpdateUser { get; set; }

    [Column("DEL_YN", TypeName = "tinyint(4)")]
    public sbyte? DelYn { get; set; }

    [Column("DEL_DT", TypeName = "datetime")]
    public DateTime? DelDt { get; set; }

    [Column("DEL_USER")]
    [StringLength(45)]
    public string? DelUser { get; set; }

    [Column("METER_ITEM_TB_ID", TypeName = "int(11)")]
    public int? MeterItemTbId { get; set; }

    [ForeignKey("MeterItemTbId")]
    [InverseProperty("EnergyUsageTbs")]
    public virtual MeterItemTb? MeterItemTb { get; set; }
}
