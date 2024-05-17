using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("ENERGY_MONTH_USAGE_TB")]
public partial class EnergyMonthUsageTb
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("JAN")]
    public double? Jan { get; set; }

    [Column("FEB")]
    public double? Feb { get; set; }

    [Column("MAR")]
    public double? Mar { get; set; }

    [Column("APR")]
    public double? Apr { get; set; }

    [Column("MAY")]
    public double? May { get; set; }

    [Column("JUN")]
    public double? Jun { get; set; }

    [Column("JUL")]
    public double? Jul { get; set; }

    [Column("AUG")]
    public double? Aug { get; set; }

    [Column("SEP")]
    public double? Sep { get; set; }

    [Column("OCT")]
    public double? Oct { get; set; }

    [Column("NOV")]
    public double? Nov { get; set; }

    [Column("DEC")]
    public double? Dec { get; set; }

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

    [Column("METER_READER_ID")]
    public int? MeterReaderId { get; set; }

    [Column("YEAR")]
    public int? Year { get; set; }

    [ForeignKey("MeterReaderId")]
    [InverseProperty("EnergyMonthUsageTbs")]
    public virtual MeterReadersTb? MeterReader { get; set; }
}
