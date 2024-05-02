using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("VOC_TB")]
public partial class VocTb
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("TITLE")]
    [StringLength(30)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [Column("CONTENT")]
    [Unicode(false)]
    public string Content { get; set; } = null!;

    [Column("STATUS")]
    public int? Status { get; set; }

    [Column("PHONE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [Column("REPLY_YN")]
    public bool? ReplyYn { get; set; }

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

    [Column("BUILDING_CD")]
    [StringLength(25)]
    [Unicode(false)]
    public string? BuildingCd { get; set; }

    [InverseProperty("Voc")]
    public virtual ICollection<AlarmsTb> AlarmsTbs { get; set; } = new List<AlarmsTb>();

    [ForeignKey("BuildingCd")]
    [InverseProperty("VocTbs")]
    public virtual BuildingsTb? BuildingCdNavigation { get; set; }

    [InverseProperty("Voc")]
    public virtual ICollection<VocCommentsTb> VocCommentsTbs { get; set; } = new List<VocCommentsTb>();
}
