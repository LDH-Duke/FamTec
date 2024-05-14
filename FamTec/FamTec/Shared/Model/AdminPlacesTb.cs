using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("ADMIN_PLACES_TB")]
public partial class AdminPlacesTb
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("CREATE_DT", TypeName = "datetime")]
    public DateTime? CreateDt { get; set; }

    [Column("UPDATE_DT", TypeName = "datetime")]
    public DateTime? UpdateDt { get; set; }

    [Column("DEL_DT", TypeName = "datetime")]
    public DateTime? DelDt { get; set; }

    [Column("DEL_YN")]
    public bool? DelYn { get; set; }

    [Column("USERS_USERID")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UsersUserid { get; set; }

    [Column("PLACECODE_CD")]
    [StringLength(25)]
    [Unicode(false)]
    public string? PlacecodeCd { get; set; }

    [ForeignKey("PlacecodeCd")]
    [InverseProperty("AdminPlacesTbs")]
    public virtual PlacesTb? PlacecodeCdNavigation { get; set; }

    [ForeignKey("UsersUserid")]
    [InverseProperty("AdminPlacesTbs")]
    public virtual UsersTb? UsersUser { get; set; }
}
