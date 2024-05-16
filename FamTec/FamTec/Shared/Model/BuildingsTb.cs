using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("BUILDINGS_TB")]
public partial class BuildingsTb
{
    [Key]
    [Column("BUILDING_CD")]
    [StringLength(25)]
    [Unicode(false)]
    public string BuildingCd { get; set; } = null!;

    [Column("NAME")]
    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("ADDRESS")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Address { get; set; }

    [Column("TEL")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Tel { get; set; }

    [Column("USAGE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Usage { get; set; }

    [Column("CONST_COMP")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ConstComp { get; set; }

    [Column("COMPLETION_DATE", TypeName = "datetime")]
    public DateTime? CompletionDate { get; set; }

    [Column("BUILDING_STRUCT")]
    [StringLength(30)]
    [Unicode(false)]
    public string? BuildingStruct { get; set; }

    [Column("ROOF_STRUCT")]
    [StringLength(30)]
    [Unicode(false)]
    public string? RoofStruct { get; set; }

    [Column("GROSS_FLOOR_AREA")]
    public double? GrossFloorArea { get; set; }

    [Column("LAND_AREA")]
    public double? LandArea { get; set; }

    [Column("BUILDING_AREA")]
    public double? BuildingArea { get; set; }

    [Column("FLOOR_NUM")]
    public int? FloorNum { get; set; }

    [Column("GROUND_FLOOR_NUM")]
    public int? GroundFloorNum { get; set; }

    [Column("BASEMENT_FLOOR_NUM")]
    public int? BasementFloorNum { get; set; }

    [Column("BUILDING_HEIGHT")]
    public double? BuildingHeight { get; set; }

    [Column("GROUND_HEIGHT")]
    public double? GroundHeight { get; set; }

    [Column("BASEMENT_HEIGHT")]
    public double? BasementHeight { get; set; }

    [Column("PACKING_NUM")]
    public int? PackingNum { get; set; }

    [Column("INNER_PACKING_NUM")]
    public int? InnerPackingNum { get; set; }

    [Column("OUTER_PACKING_NUM")]
    public int? OuterPackingNum { get; set; }

    [Column("ELEC_CAPACITY")]
    public double? ElecCapacity { get; set; }

    [Column("FAUCET_CAPACITY")]
    public double? FaucetCapacity { get; set; }

    [Column("GENERATION_CAPACITY")]
    public double? GenerationCapacity { get; set; }

    [Column("WATER_CAPACITY")]
    public double? WaterCapacity { get; set; }

    [Column("ELEV_WATER_TANK")]
    public double? ElevWaterTank { get; set; }

    [Column("WATER_TANK")]
    public double? WaterTank { get; set; }

    [Column("GAS_CAPACITY")]
    public double? GasCapacity { get; set; }

    [Column("BOILER")]
    public double? Boiler { get; set; }

    [Column("WATER_DISPENSER")]
    public double? WaterDispenser { get; set; }

    [Column("LIFT_NUM")]
    public int? LiftNum { get; set; }

    [Column("PEOPLE_LIFT_NUM")]
    public int? PeopleLiftNum { get; set; }

    [Column("CARGO_LIFT_NUM")]
    public int? CargoLiftNum { get; set; }

    [Column("COOL_HEAT_CAPACITY")]
    public double? CoolHeatCapacity { get; set; }

    [Column("HEAT_CAPACITY")]
    public double? HeatCapacity { get; set; }

    [Column("COOL_CAPACITY")]
    public double? CoolCapacity { get; set; }

    [Column("LANDSCAPE_AREA")]
    public double? LandscapeArea { get; set; }

    [Column("GROUND_AREA")]
    public double? GroundArea { get; set; }

    [Column("ROOFTOP_AREA")]
    public double? RooftopArea { get; set; }

    [Column("TOILET_NUM")]
    public int? ToiletNum { get; set; }

    [Column("MEN_TOILET_NUM")]
    public int? MenToiletNum { get; set; }

    [Column("WOMEN_TOILET_NUM")]
    public int? WomenToiletNum { get; set; }

    [Column("FIRE_RATING")]
    [StringLength(10)]
    [Unicode(false)]
    public string? FireRating { get; set; }

    [Column("SEPTIC_TANK_CAPACITY")]
    public double? SepticTankCapacity { get; set; }

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

    [Column("PLACECODE_CD")]
    [StringLength(25)]
    [Unicode(false)]
    public string? PlacecodeCd { get; set; }

    [InverseProperty("BuildingCdNavigation")]
    public virtual ICollection<FloorsTb> FloorsTbs { get; set; } = new List<FloorsTb>();

    [InverseProperty("BuildingCdNavigation")]
    public virtual ICollection<MeterReadersTb> MeterReadersTbs { get; set; } = new List<MeterReadersTb>();

    [ForeignKey("PlacecodeCd")]
    [InverseProperty("BuildingsTbs")]
    public virtual PlacesTb? PlacecodeCdNavigation { get; set; }

    [InverseProperty("BuildingCdNavigation")]
    public virtual ICollection<RoomInventorysTb> RoomInventorysTbs { get; set; } = new List<RoomInventorysTb>();

    [InverseProperty("BuildingCdNavigation")]
    public virtual ICollection<SubItemsTb> SubItemsTbs { get; set; } = new List<SubItemsTb>();

    [InverseProperty("BuildingCdNavigation")]
    public virtual ICollection<TotalInventorysTb> TotalInventorysTbs { get; set; } = new List<TotalInventorysTb>();

    [InverseProperty("BuildingCdNavigation")]
    public virtual ICollection<VocTb> VocTbs { get; set; } = new List<VocTb>();
}
