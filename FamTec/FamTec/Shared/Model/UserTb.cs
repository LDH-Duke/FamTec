using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("user_tb")]
[Index("PlaceTbId", Name = "fk_USER_TB_PLACE_TB1_idx")]
public partial class UserTb
{
    [Key]
    [Column("ID", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("USER_ID")]
    [StringLength(255)]
    public string? UserId { get; set; }

    [Column("PASSWORD")]
    [StringLength(255)]
    public string? Password { get; set; }

    [Column("NAME")]
    [StringLength(255)]
    public string? Name { get; set; }

    [Column("EMAIL")]
    [StringLength(255)]
    public string? Email { get; set; }

    [Column("PHONE")]
    [StringLength(255)]
    public string? Phone { get; set; }

    [Column("PERM_BUILDING", TypeName = "int(11)")]
    public int? PermBuilding { get; set; }

    [Column("PERM_EQUIPMENT", TypeName = "int(11)")]
    public int? PermEquipment { get; set; }

    [Column("PERM_MATERIAL", TypeName = "int(11)")]
    public int? PermMaterial { get; set; }

    [Column("PERM_ENERGY", TypeName = "int(11)")]
    public int? PermEnergy { get; set; }

    [Column("PERM_OFFICE", TypeName = "int(11)")]
    public int? PermOffice { get; set; }

    [Column("PERM_COMP", TypeName = "int(11)")]
    public int? PermComp { get; set; }

    [Column("PERM_CONST", TypeName = "int(11)")]
    public int? PermConst { get; set; }

    [Column("PERM_CLAIM", TypeName = "int(11)")]
    public int? PermClaim { get; set; }

    [Column("PERM_SYS", TypeName = "int(11)")]
    public int? PermSys { get; set; }

    [Column("PERM_EMPLOYEE", TypeName = "int(11)")]
    public int? PermEmployee { get; set; }

    [Column("PERM_LAW_CK", TypeName = "int(11)")]
    public int? PermLawCk { get; set; }

    [Column("PERM_LAW_EDU", TypeName = "int(11)")]
    public int? PermLawEdu { get; set; }

    [Column("ADMIN_YN", TypeName = "int(11)")]
    public int? AdminYn { get; set; }

    [Column("ALRAM_YN", TypeName = "tinyint(4)")]
    public sbyte? AlramYn { get; set; }

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
    [StringLength(255)]
    public string? DelUser { get; set; }

    [Column("PLACE_TB_ID", TypeName = "int(11)")]
    public int? PlaceTbId { get; set; }

    [InverseProperty("UserTb")]
    public virtual ICollection<AdminTb> AdminTbs { get; set; } = new List<AdminTb>();

    [ForeignKey("PlaceTbId")]
    [InverseProperty("UserTbs")]
    public virtual PlaceTb? PlaceTb { get; set; }
}
