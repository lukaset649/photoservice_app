using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace photoservice.Models;

[Table("equipment_compability")]
public partial class EquipmentCompability
{
    [Key]
    [Column("id_compability")]
    public int IdCompability { get; set; }

    [Column("eq_id")]
    public int EqId { get; set; }

    [Column("compatible_with_id")]
    public int CompatibleWithId { get; set; }

    [ForeignKey("CompatibleWithId")]
    [InverseProperty("EquipmentCompabilityCompatibleWiths")]
    public virtual Equipment CompatibleWith { get; set; } = null!;

    [ForeignKey("EqId")]
    [InverseProperty("EquipmentCompabilityEqs")]
    public virtual Equipment Eq { get; set; } = null!;
}
