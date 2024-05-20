using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("meter_reader_tb")]
[Index("BuildingTbId", Name = "fk_METER_READER_TB_BUILDING_TB1_idx")]
public partial class MeterReaderTb
{
    [Key]
    [Column("ID", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("CATEGORY")]
    [StringLength(45)]
    public string? Category { get; set; }

    /// <summary>
    /// 계약종별
    /// </summary>
    [Column("TYPE")]
    [StringLength(45)]
    public string? Type { get; set; }

    [Column("METER_ITEM")]
    [StringLength(45)]
    public string? MeterItem { get; set; }

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

    [Column("BUILDING_TB_ID", TypeName = "int(11)")]
    public int? BuildingTbId { get; set; }

    [ForeignKey("BuildingTbId")]
    [InverseProperty("MeterReaderTbs")]
    public virtual BuildingTb? BuildingTb { get; set; }

    [InverseProperty("MeterReaderTb")]
    public virtual ICollection<EnergyMonthUsageTb> EnergyMonthUsageTbs { get; set; } = new List<EnergyMonthUsageTb>();

    [InverseProperty("MeterReaderTb")]
    public virtual ICollection<MeterItemTb> MeterItemTbs { get; set; } = new List<MeterItemTb>();
}
