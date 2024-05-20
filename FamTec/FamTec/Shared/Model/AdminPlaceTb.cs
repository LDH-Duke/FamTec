using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("admin_place_tb")]
[Index("AdminTbId", Name = "fk_ADMIN_TB_has_PLACE_ADMIN_TB1_idx")]
[Index("PlaceId", Name = "fk_ADMIN_TB_has_PLACE_PLACE1_idx")]
public partial class AdminPlaceTb
{
    [Key]
    [Column("ID", TypeName = "int(11)")]
    public int Id { get; set; }

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

    [Column("ADMIN_TB_ID", TypeName = "int(11)")]
    public int? AdminTbId { get; set; }

    [Column("PLACE_ID", TypeName = "int(11)")]
    public int? PlaceId { get; set; }

    [ForeignKey("AdminTbId")]
    [InverseProperty("AdminPlaceTbs")]
    public virtual AdminTb? AdminTb { get; set; }

    [ForeignKey("PlaceId")]
    [InverseProperty("AdminPlaceTbs")]
    public virtual PlaceTb? Place { get; set; }
}
