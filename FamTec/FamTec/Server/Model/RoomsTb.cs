using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Model;

[Table("ROOMS_TB")]
public partial class RoomsTb
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("NAME")]
    [StringLength(35)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

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

    [Column("FLOOR_ID")]
    public int? FloorId { get; set; }

    [InverseProperty("Rooms")]
    public virtual ICollection<FacilitysTb> FacilitysTbs { get; set; } = new List<FacilitysTb>();

    [ForeignKey("FloorId")]
    [InverseProperty("RoomsTbs")]
    public virtual FloorsTb? Floor { get; set; }

    [InverseProperty("Rooms")]
    public virtual ICollection<RoomInventorysTb> RoomInventorysTbs { get; set; } = new List<RoomInventorysTb>();
}
