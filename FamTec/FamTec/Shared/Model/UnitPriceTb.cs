using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("UNIT_PRICE_TB")]
public partial class UnitPriceTb
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("PRICE")]
    public double? Price { get; set; }

    [Column("METER_READER_ID")]
    public int? MeterReaderId { get; set; }

    [Column("ENVERY_USAGE_ID")]
    public int? EnveryUsageId { get; set; }

    [ForeignKey("EnveryUsageId")]
    [InverseProperty("UnitPriceTbs")]
    public virtual EnergyUsagesTb? EnveryUsage { get; set; }

    [ForeignKey("MeterReaderId")]
    [InverseProperty("UnitPriceTbs")]
    public virtual MeterReadersTb? MeterReader { get; set; }
}
