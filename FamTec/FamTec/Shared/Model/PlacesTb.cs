using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("PLACES_TB")]
public partial class PlacesTb
{
    [Key]
    [Column("PLACE_CD")]
    [StringLength(25)]
    [Unicode(false)]
    public string PlaceCd { get; set; } = null!;

    [Column("NAME")]
    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("CONTRACT_NUM")]
    [StringLength(45)]
    [Unicode(false)]
    public string ContractNum { get; set; } = null!;

    [Column("NOTE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Note { get; set; }

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

    [InverseProperty("PlacecodeCdNavigation")]
    public virtual ICollection<BuildingsTb> BuildingsTbs { get; set; } = new List<BuildingsTb>();

    [InverseProperty("PlacecodeCdNavigation")]
    public virtual ICollection<UnitTb> UnitTbs { get; set; } = new List<UnitTb>();

    [InverseProperty("PlacecodeCdNavigation")]
    public virtual ICollection<UsersTb> UsersTbs { get; set; } = new List<UsersTb>();
}
