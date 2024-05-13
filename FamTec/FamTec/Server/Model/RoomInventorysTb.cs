using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Model;

[Table("ROOM_INVENTORYS_TB")]
public partial class RoomInventorysTb
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ROOM_MATERIAL_NUM")]
    public int RoomMaterialNum { get; set; }

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

    [Column("ROOMS_ID")]
    public int? RoomsId { get; set; }

    [Column("BUILDING_CD")]
    [StringLength(25)]
    [Unicode(false)]
    public string? BuildingCd { get; set; }

    [ForeignKey("BuildingCd")]
    [InverseProperty("RoomInventorysTbs")]
    public virtual BuildingsTb? BuildingCdNavigation { get; set; }

    [ForeignKey("RoomsId")]
    [InverseProperty("RoomInventorysTbs")]
    public virtual RoomsTb? Rooms { get; set; }
}
