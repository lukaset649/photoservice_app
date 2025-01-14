using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace photoservice.Models;

[Table("equipment_type")]
public partial class EquipmentType
{
    [Key]
    [Column("id_eq_type")]
    public int IdEqType { get; set; }

    [Column("name")]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("description")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Description { get; set; }

    [InverseProperty("EqType")]
    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
}
