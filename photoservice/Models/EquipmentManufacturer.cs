using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace photoservice.Models;

[Table("equipment_manufacturer")]
public partial class EquipmentManufacturer
{
    [Key]
    [Column("id_eq_man")]
    public int IdEqMan { get; set; }

    [Column("name")]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("description")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Description { get; set; }

    [InverseProperty("EqManufacturer")]
    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
}
