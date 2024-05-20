using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("admin_tb")]
[Index("DepartmentTbId", Name = "fk_ADMIN_TB_DEPARTMENT_TB1_idx")]
[Index("UserTbId", Name = "fk_ADMIN_TB_USER_TB_idx")]
public partial class AdminTb
{
    [Key]
    [Column("ID", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("TYPE")]
    [StringLength(255)]
    public string? Type { get; set; }

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

    [Column("USER_TB_ID", TypeName = "int(11)")]
    public int? UserTbId { get; set; }

    [Column("DEPARTMENT_TB_ID", TypeName = "int(11)")]
    public int? DepartmentTbId { get; set; }

    [InverseProperty("AdminTb")]
    public virtual ICollection<AdminPlaceTb> AdminPlaceTbs { get; set; } = new List<AdminPlaceTb>();

    [ForeignKey("DepartmentTbId")]
    [InverseProperty("AdminTbs")]
    public virtual DepartmentTb? DepartmentTb { get; set; }

    [ForeignKey("UserTbId")]
    [InverseProperty("AdminTbs")]
    public virtual UserTb? UserTb { get; set; }
}
