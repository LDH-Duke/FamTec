using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("SUB_ITEMS_TB")]
public partial class SubItemsTb
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("NAME")]
    [StringLength(25)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("VALUE")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Value { get; set; }

    [Column("UNIT")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Unit { get; set; }

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

    [Column("BUILDING_CD")]
    [StringLength(25)]
    [Unicode(false)]
    public string? BuildingCd { get; set; }

    [Column("FACILITY_ID")]
    public int? FacilityId { get; set; }

    [ForeignKey("BuildingCd")]
    [InverseProperty("SubItemsTbs")]
    public virtual BuildingsTb? BuildingCdNavigation { get; set; }

    [ForeignKey("FacilityId")]
    [InverseProperty("SubItemsTbs")]
    public virtual FacilitysTb? Facility { get; set; }
}
