using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("ALARMS_TB")]
public partial class AlarmsTb
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("CONTENT")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Content { get; set; }

    [Column("STATUS")]
    public int? Status { get; set; }

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

    [Column("VOC_ID")]
    public int? VocId { get; set; }

    [Column("USERS_USERID")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UsersUserid { get; set; }

    [Column("SOCKET_ROOMS_ID")]
    public int? SocketRoomsId { get; set; }

    [ForeignKey("SocketRoomsId")]
    [InverseProperty("AlarmsTbs")]
    public virtual SocketRoomsTb? SocketRooms { get; set; }

    [ForeignKey("UsersUserid")]
    [InverseProperty("AlarmsTbs")]
    public virtual UsersTb? UsersUser { get; set; }

    [ForeignKey("VocId")]
    [InverseProperty("AlarmsTbs")]
    public virtual VocTb? Voc { get; set; }
}
