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

<<<<<<< HEAD
    [Column("JOB")]
    [StringLength(255)]
    public string? Job { get; set; }

=======
>>>>>>> Server
    [Column("EMAIL")]
    [StringLength(255)]
    public string? Email { get; set; }

    [Column("PHONE")]
    [StringLength(255)]
    public string? Phone { get; set; }

    [Column("PERM_BASIC", TypeName = "int(11)")]
    public int? PermBasic { get; set; }

    [Column("PERM_MACHINE", TypeName = "int(11)")]
    public int? PermMachine { get; set; }

    [Column("PERM_LIFT", TypeName = "int(11)")]
    public int? PermLift { get; set; }

    [Column("PERM_FIRE", TypeName = "int(11)")]
    public int? PermFire { get; set; }

    [Column("PERM_CONSTRUCT", TypeName = "int(11)")]
    public int? PermConstruct { get; set; }

    [Column("PERM_NETWORK", TypeName = "int(11)")]
    public int? PermNetwork { get; set; }

    [Column("PERM_BEAUTY", TypeName = "int(11)")]
    public int? PermBeauty { get; set; }

    [Column("PERM_SECURITY", TypeName = "int(11)")]
    public int? PermSecurity { get; set; }

    [Column("PERM_MATERIAL", TypeName = "int(11)")]
    public int? PermMaterial { get; set; }

    [Column("PERM_ENERGY", TypeName = "int(11)")]
    public int? PermEnergy { get; set; }

    [Column("PERM_USER", TypeName = "int(11)")]
    public int? PermUser { get; set; }

    [Column("PERM_VOC", TypeName = "int(11)")]
    public int? PermVoc { get; set; }

    [Column("ADMIN_YN", TypeName = "tinyint(4)")]
    public sbyte? AdminYn { get; set; }

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

<<<<<<< HEAD
=======
    [Column("JOB")]
    [StringLength(255)]
    public string? Job { get; set; }

>>>>>>> Server
    [InverseProperty("UserTb")]
    public virtual ICollection<AdminTb> AdminTbs { get; set; } = new List<AdminTb>();

    [ForeignKey("PlaceTbId")]
    [InverseProperty("UserTbs")]
    public virtual PlaceTb? PlaceTb { get; set; }
}
