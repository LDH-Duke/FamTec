using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("TOTAL_INVENTORYS_TB")]
public partial class TotalInventorysTb
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("INCOMING_NUM")]
    public int IncomingNum { get; set; }

    [Column("PRICE")]
    public int Price { get; set; }

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

    [Column("MATERIAL_ID")]
    public int? MaterialId { get; set; }

    [Column("BUILDING_CD")]
    [StringLength(25)]
    [Unicode(false)]
    public string? BuildingCd { get; set; }

    [ForeignKey("BuildingCd")]
    [InverseProperty("TotalInventorysTbs")]
    public virtual BuildingsTb? BuildingCdNavigation { get; set; }

    [ForeignKey("MaterialId")]
    [InverseProperty("TotalInventorysTbs")]
    public virtual MaterialsTb? Material { get; set; }
}
