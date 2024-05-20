using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("meter_item_tb")]
[Index("MeterReaderTbId", Name = "fk_METER_ITEM_TB_METER_READER_TB1_idx")]
public partial class MeterItemTb
{
    [Key]
    [Column("ID", TypeName = "int(11)")]
    public int Id { get; set; }

    /// <summary>
    /// 검침항목
    /// </summary>
    [Column("METER_ITEM")]
    [StringLength(45)]
    public string? MeterItem { get; set; }

    [Column("ACCUM_USAGE")]
    public float? AccumUsage { get; set; }

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

    [Column("METER_READER_TB_ID", TypeName = "int(11)")]
    public int? MeterReaderTbId { get; set; }

    [InverseProperty("MeterItemTb")]
    public virtual ICollection<EnergyUsageTb> EnergyUsageTbs { get; set; } = new List<EnergyUsageTb>();

    [ForeignKey("MeterReaderTbId")]
    [InverseProperty("MeterItemTbs")]
    public virtual MeterReaderTb? MeterReaderTb { get; set; }
}
