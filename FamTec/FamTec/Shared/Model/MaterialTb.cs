using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("material_tb")]
public partial class MaterialTb
{
    /// <summary>
    /// 자재 인덱스
    /// </summary>
    [Key]
    [Column("ID", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("CATEGORY")]
    [StringLength(255)]
    public string? Category { get; set; }

    [Column("TYPE")]
    [StringLength(255)]
    public string? Type { get; set; }

    [Column("MATERIAL_CODE")]
    [StringLength(255)]
    public string? MaterialCode { get; set; }

    [Column("NAME")]
    [StringLength(255)]
    public string? Name { get; set; }

    [Column("UNIT")]
    [StringLength(255)]
    public string? Unit { get; set; }

    [Column("STANDARD")]
    [StringLength(255)]
    public string? Standard { get; set; }

    /// <summary>
    /// 제조사
    /// </summary>
    [Column("MFR")]
    [StringLength(255)]
    public string? Mfr { get; set; }

    [Column("SAFE_NUM", TypeName = "int(11)")]
    public int? SafeNum { get; set; }

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

    [InverseProperty("MaterialTb")]
    public virtual ICollection<StoreTb> StoreTbs { get; set; } = new List<StoreTb>();
}
