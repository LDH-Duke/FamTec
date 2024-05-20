using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("place_tb")]
public partial class PlaceTb
{
    [Key]
    [Column("ID", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("PLACE_CD")]
    [StringLength(255)]
    public string? PlaceCd { get; set; }

    [Column("CONTRACT_NUM")]
    [StringLength(255)]
    public string? ContractNum { get; set; }

    [Column("NAME")]
    [StringLength(255)]
    public string? Name { get; set; }

    [Column("NOTE")]
    [StringLength(255)]
    public string? Note { get; set; }

    [Column("CONTRACT_DT", TypeName = "datetime")]
    public DateTime? ContractDt { get; set; }

    [Column("CANCEL_DT", TypeName = "datetime")]
    public DateTime? CancelDt { get; set; }

    [Column("STATUS", TypeName = "tinyint(4)")]
    public sbyte? Status { get; set; }

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
    [StringLength(45)]
    public string? DelUser { get; set; }

    [InverseProperty("Place")]
    public virtual ICollection<AdminPlaceTb> AdminPlaceTbs { get; set; } = new List<AdminPlaceTb>();

    [InverseProperty("PlaceTb")]
    public virtual ICollection<BuildingTb> BuildingTbs { get; set; } = new List<BuildingTb>();

    [InverseProperty("PlaceTb")]
    public virtual ICollection<UnitTb> Units { get; set; } = new List<UnitTb>();

    [InverseProperty("PlaceTb")]
    public virtual ICollection<UserTb> UserTbs { get; set; } = new List<UserTb>();
}
