using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("UNIT_TB")]
public partial class UnitTb
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("UNIT")]
    [StringLength(10)]
    [Unicode(false)]
    public string Unit { get; set; } = null!;

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

    [ForeignKey("PlacecodeCd")]
    [InverseProperty("UnitTbs")]
    public virtual PlacesTb? PlacecodeCdNavigation { get; set; }
}
