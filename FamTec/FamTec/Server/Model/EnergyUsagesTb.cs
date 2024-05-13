using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Model;

[Table("ENERGY_USAGES_TB")]
public partial class EnergyUsagesTb
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("USAGE")]
    public double? Usage { get; set; }

    [Column("METER_DT", TypeName = "datetime")]
    public DateTime? MeterDt { get; set; }

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

    [InverseProperty("EnveryUsage")]
    public virtual ICollection<UnitPriceTb> UnitPriceTbs { get; set; } = new List<UnitPriceTb>();
}
