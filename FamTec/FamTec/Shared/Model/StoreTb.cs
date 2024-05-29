using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("store_tb")]
[Index("BuildingTbId", Name = "fk_STORE_TB_BUILDING_TB1_idx")]
[Index("MaterialTbId", Name = "fk_STORE_TB_MATERIAL_TB1_idx")]
[Index("RoomTbId", Name = "fk_STORE_TB_ROOM_TB1_idx")]
public partial class StoreTb
{
    [Key]
    [Column("ID", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("CATEGORY")]
    [StringLength(255)]
    public string? Category { get; set; }

    [Column("NUM", TypeName = "int(11)")]
    public int? Num { get; set; }

    [Column("UNIT_PRICE")]
    public float? UnitPrice { get; set; }


    [Column("PRICE")]
    public float? Price { get; set; }

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

    [Column("MATERIAL_TB_ID", TypeName = "int(11)")]
    public int? MaterialTbId { get; set; }

    [Column("ROOM_TB_ID", TypeName = "int(11)")]
    public int? RoomTbId { get; set; }

    [Column("BUILDING_TB_ID", TypeName = "int(11)")]
    public int? BuildingTbId { get; set; }

    [ForeignKey("BuildingTbId")]
    [InverseProperty("StoreTbs")]
    public virtual BuildingTb? BuildingTb { get; set; }

    [ForeignKey("MaterialTbId")]
    [InverseProperty("StoreTbs")]
    public virtual MaterialTb? MaterialTb { get; set; }

    [ForeignKey("RoomTbId")]
    [InverseProperty("StoreTbs")]
    public virtual RoomTb? RoomTb { get; set; }
}
