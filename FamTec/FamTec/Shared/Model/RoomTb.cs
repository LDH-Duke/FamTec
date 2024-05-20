﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

[Table("room_tb")]
[Index("FloorTbId", Name = "fk_ROOM_TB_FLOOR_TB1_idx")]
public partial class RoomTb
{
    [Key]
    [Column("ID", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("NAME")]
    [StringLength(255)]
    public string? Name { get; set; }

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

    [Column("FLOOR_TB_ID", TypeName = "int(11)")]
    public int? FloorTbId { get; set; }

    [ForeignKey("FloorTbId")]
    [InverseProperty("RoomTbs")]
    public virtual FloorTb? FloorTb { get; set; }

    [InverseProperty("RoomTb")]
    public virtual ICollection<StoreTb> StoreTbs { get; set; } = new List<StoreTb>();
}