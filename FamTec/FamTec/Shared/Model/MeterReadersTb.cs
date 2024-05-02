using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("METER_READERS_TB")]
public partial class MeterReadersTb
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("CATEGORY")]
    [StringLength(10)]
    [Unicode(false)]
    public string Category { get; set; } = null!;

    [Column("TYPE")]
    [StringLength(30)]
    [Unicode(false)]
    public string Type { get; set; } = null!;

    [Column("METER_ITEM")]
    [StringLength(30)]
    [Unicode(false)]
    public string? MeterItem { get; set; }

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

    [Column("BUILDING_CD")]
    [StringLength(25)]
    [Unicode(false)]
    public string? BuildingCd { get; set; }

    [ForeignKey("BuildingCd")]
    [InverseProperty("MeterReadersTbs")]
    public virtual BuildingsTb? BuildingCdNavigation { get; set; }

    [InverseProperty("MeterReader")]
    public virtual ICollection<UnitPriceTb> UnitPriceTbs { get; set; } = new List<UnitPriceTb>();
}
