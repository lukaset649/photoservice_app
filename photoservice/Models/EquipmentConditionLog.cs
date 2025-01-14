using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace photoservice.Models;

[Table("equipment_condition_log")]
public partial class EquipmentConditionLog
{
    [Key]
    [Column("log_id")]
    public int LogId { get; set; }

    [Column("equipment_id")]
    public int? EquipmentId { get; set; }

    [Column("old_condition")]
    [StringLength(255)]
    [Unicode(false)]
    public string? OldCondition { get; set; }

    [Column("new_condition")]
    [StringLength(255)]
    [Unicode(false)]
    public string? NewCondition { get; set; }

    [Column("change_date", TypeName = "datetime")]
    public DateTime? ChangeDate { get; set; }
}
