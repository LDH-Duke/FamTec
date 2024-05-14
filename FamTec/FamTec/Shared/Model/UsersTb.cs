using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("USERS_TB")]
public partial class UsersTb
{
    [Key]
    [Column("USER_ID")]
    [StringLength(15)]
    [Unicode(false)]
    public string UserId { get; set; } = null!;

    [Column("PASSWORD")]
    [StringLength(30)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [Column("NAME")]
    [StringLength(15)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("EMAIL")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("PHONE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [Column("PERM_BUILDING")]
    public int PermBuilding { get; set; }

    [Column("PERM_EQUIPMENT")]
    public int PermEquipment { get; set; }

    [Column("PERM_MATERIAL")]
    public int PermMaterial { get; set; }

    [Column("PERM_ENERGY")]
    public int PermEnergy { get; set; }

    [Column("PERM_OFFICE")]
    public int PermOffice { get; set; }

    [Column("PERM_COMP")]
    public int PermComp { get; set; }

    [Column("PERM_CONST")]
    public int PermConst { get; set; }

    [Column("PERM_CLAIM")]
    public int PermClaim { get; set; }

    [Column("PERM_SYS")]
    public int PermSys { get; set; }

    [Column("PERM_EMPLOYEE")]
    public int PermEmployee { get; set; }

    [Column("PERM_LAW_CK")]
    public int PermLawCk { get; set; }

    [Column("PERM_LAW_EDU")]
    public int PermLawEdu { get; set; }

    [Column("ADMIN_YN")]
    public bool? AdminYn { get; set; }

    [Column("ALARM_YN")]
    public bool? AlarmYn { get; set; }

    [Column("STATUS")]
    public bool? Status { get; set; }

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

    [InverseProperty("UsersUser")]
    public virtual ICollection<AdminPlacesTb> AdminPlacesTbs { get; set; } = new List<AdminPlacesTb>();

    [InverseProperty("UsersUser")]
    public virtual ICollection<AlarmsTb> AlarmsTbs { get; set; } = new List<AlarmsTb>();

    [ForeignKey("PlacecodeCd")]
    [InverseProperty("UsersTbs")]
    public virtual PlacesTb? PlacecodeCdNavigation { get; set; }

    [InverseProperty("UsersUser")]
    public virtual ICollection<VocCommentsTb> VocCommentsTbs { get; set; } = new List<VocCommentsTb>();
}
