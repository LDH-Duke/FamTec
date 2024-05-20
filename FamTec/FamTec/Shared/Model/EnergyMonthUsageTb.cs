using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("energy_month_usage_tb")]
[Index("MeterReaderTbId", Name = "fk_ENERGY_MONTH_USAGE_TB_METER_READER_TB1_idx")]
public partial class EnergyMonthUsageTb
{
    [Key]
    [Column("ID", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("JAN")]
    public float? Jan { get; set; }

    [Column("FEB")]
    public float? Feb { get; set; }

    [Column("MAR")]
    public float? Mar { get; set; }

    [Column("APR")]
    public float? Apr { get; set; }

    [Column("MAY")]
    public float? May { get; set; }

    [Column("JUN")]
    public float? Jun { get; set; }

    [Column("JUL")]
    public float? Jul { get; set; }

    [Column("AUG")]
    public float? Aug { get; set; }

    [Column("SEP")]
    public float? Sep { get; set; }

    [Column("OCT")]
    public float? Oct { get; set; }

    [Column("NOV")]
    public float? Nov { get; set; }

    [Column("DEV")]
    public float? Dev { get; set; }

    [Column("METER_READER_TB_ID", TypeName = "int(11)")]
    public int? MeterReaderTbId { get; set; }

    [ForeignKey("MeterReaderTbId")]
    [InverseProperty("EnergyMonthUsageTbs")]
    public virtual MeterReaderTb? MeterReaderTb { get; set; }
}
