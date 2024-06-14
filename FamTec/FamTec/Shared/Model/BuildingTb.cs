using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("building_tb")]
[Index("PlaceTbId", Name = "fk_BUILDING_TB_PLACE_TB1_idx")]
public partial class BuildingTb
{
    [Key]
    [Column("ID", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("BUILDING_CD")]
    [StringLength(255)]
    public string? BuildingCd { get; set; }

    [Column("NAME")]
    [StringLength(255)]
    public string? Name { get; set; }

    [Column("ADDRESS")]
    [StringLength(255)]
    public string? Address { get; set; }

    [Column("TEL")]
    [StringLength(255)]
    public string? Tel { get; set; }

    [Column("USAGE")]
    [StringLength(255)]
    public string? Usage { get; set; }

    [Column("CONST_COMP")]
    [StringLength(255)]
    public string? ConstComp { get; set; }

    [Column("COMPLETION_DT", TypeName = "datetime")]
    public DateTime? CompletionDt { get; set; }

    [Column("BUILDING_STRUCT")]
    [StringLength(255)]
    public string? BuildingStruct { get; set; }

    [Column("ROOF_STRUCT")]
    [StringLength(255)]
    public string? RoofStruct { get; set; }

    [Column("GROSS_FLOOR_AREA")]
    public float? GrossFloorArea { get; set; }

    [Column("LAND_AREA")]
    public float? LandArea { get; set; }

    [Column("BUILDING_AREA")]
    public float? BuildingArea { get; set; }

    [Column("FLOOR_NUM", TypeName = "int(11)")]
    public int? FloorNum { get; set; }

    [Column("GROUND_FLOOR_NUM", TypeName = "int(11)")]
    public int? GroundFloorNum { get; set; }

    [Column("BASEMENT_FLOOR_NUM", TypeName = "int(11)")]
    public int? BasementFloorNum { get; set; }

    [Column("BUILDING_HEIGHT")]
    public float? BuildingHeight { get; set; }

    [Column("GROUND_HEIGHT")]
    public float? GroundHeight { get; set; }

    [Column("BASEMENT_HEIGHT")]
    public float? BasementHeight { get; set; }

    [Column("PARKING_NUM", TypeName = "int(11)")]
    public int? ParkingNum { get; set; }

    [Column("INNER_PARKING_NUM", TypeName = "int(11)")]
    public int? InnerParkingNum { get; set; }

    [Column("OUTER_PARKING_NUM", TypeName = "int(11)")]
    public int? OuterParkingNum { get; set; }

    [Column("ELEC_CAPACITY")]
    public float? ElecCapacity { get; set; }

    [Column("FAUCET_CAPACITY")]
    public float? FaucetCapacity { get; set; }

    [Column("GENERATION_CAPACITY")]
    public float? GenerationCapacity { get; set; }

    [Column("WATER_CAPACITY")]
    public float? WaterCapacity { get; set; }

    [Column("ELEV_WATER_CAPACITY")]
    public float? ElevWaterCapacity { get; set; }

    [Column("WATER_TANK")]
    public float? WaterTank { get; set; }

    [Column("GAS_CAPACITY")]
    public float? GasCapacity { get; set; }

    [Column("BOILER")]
    public float? Boiler { get; set; }

    [Column("WATER_DISPENSER")]
    public float? WaterDispenser { get; set; }

    [Column("LIFT_NUM", TypeName = "int(11)")]
    public int? LiftNum { get; set; }

    [Column("PEOPLE_LIFT_NUM", TypeName = "int(11)")]
    public int? PeopleLiftNum { get; set; }

    [Column("CARGO_LIFT_NUM", TypeName = "int(11)")]
    public int? CargoLiftNum { get; set; }

    [Column("COOL_HEAT_CAPACITY")]
    public float? CoolHeatCapacity { get; set; }

    [Column("HEAT_CAPACITY")]
    public float? HeatCapacity { get; set; }

    [Column("COOL_CAPACITY")]
    public float? CoolCapacity { get; set; }

    [Column("LANDSCAPE_AREA")]
    public float? LandscapeArea { get; set; }

    [Column("GROUND_AREA")]
    public float? GroundArea { get; set; }

    [Column("ROOFTOP_AREA")]
    public float? RooftopArea { get; set; }

    [Column("TOILET_NUM", TypeName = "int(11)")]
    public int? ToiletNum { get; set; }

    [Column("MEN_TOILET_NUM", TypeName = "int(11)")]
    public int? MenToiletNum { get; set; }

    [Column("WOMEN_TOILET_NUM", TypeName = "int(11)")]
    public int? WomenToiletNum { get; set; }

    [Column("FIRE_RATING")]
    [StringLength(255)]
    public string? FireRating { get; set; }

    [Column("SEPTIC_TANK_CAPACITY")]
    public float? SepticTankCapacity { get; set; }

    [Column("CREATE_DT", TypeName = "datetime")]
    public DateTime? CreateDt { get; set; }

    [Column("CREATE_USER")]
    [StringLength(255)]
    public string? CreateUser { get; set; }

    [Column("UPDATE_DT", TypeName = "datetime")]
    public DateTime? UpdateDt { get; set; }

    [Column("UPDATE_USER")]
    [StringLength(255)]
    public string? UpdateUser { get; set; }

    [Column("DEL_YN", TypeName = "tinyint(4)")]
    public sbyte? DelYn { get; set; }

    [Column("DEL_DT", TypeName = "datetime")]
    public DateTime? DelDt { get; set; }

    [Column("DEL_USER")]
    [StringLength(255)]
    public string? DelUser { get; set; }

    [Column("PLACE_TB_ID", TypeName = "int(11)")]
    public int? PlaceTbId { get; set; }

    [InverseProperty("BuildingTb")]
    public virtual ICollection<FloorTb> FloorTbs { get; set; } = new List<FloorTb>();

    [InverseProperty("BuildingTb")]
    public virtual ICollection<MeterReaderTb> MeterReaderTbs { get; set; } = new List<MeterReaderTb>();

    [ForeignKey("PlaceTbId")]
    [InverseProperty("BuildingTbs")]
    public virtual PlaceTb? PlaceTb { get; set; }

    [InverseProperty("BuildingTb")]
    public virtual ICollection<StoreTb> StoreTbs { get; set; } = new List<StoreTb>();

    [InverseProperty("BuildingTb")]
    public virtual ICollection<VocTb> VocTbs { get; set; } = new List<VocTb>();
}
