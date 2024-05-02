using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("FACILITYS_TB")]
public partial class FacilitysTb
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("NAME")]
    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("CATEGORY")]
    [StringLength(10)]
    [Unicode(false)]
    public string Category { get; set; } = null!;

    [Column("TYPE")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Type { get; set; }

    [Column("STANDARD_CAPACITY")]
    public double? StandardCapacity { get; set; }

    [Column("STANDARD_CAPACITY_UNIT")]
    [StringLength(10)]
    [Unicode(false)]
    public string? StandardCapacityUnit { get; set; }

    [Column("FAC_CREATE_DT", TypeName = "datetime")]
    public DateTime? FacCreateDt { get; set; }

    [Column("LIFESPAN")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Lifespan { get; set; }

    [Column("FAC_UPDATE_DT", TypeName = "datetime")]
    public DateTime? FacUpdateDt { get; set; }

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

    [ForeignKey("RoomsId")]
    [InverseProperty("FacilitysTbs")]
    public virtual RoomsTb? Rooms { get; set; }

    [InverseProperty("Facility")]
    public virtual ICollection<SubItemsTb> SubItemsTbs { get; set; } = new List<SubItemsTb>();
}
