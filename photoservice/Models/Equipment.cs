using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace photoservice.Models;

[Table("equipment")]
public partial class Equipment
{
    [Key]
    [Column("id_eq")]
    public int IdEq { get; set; }

    [Column("name")]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("eq_type_id")]
    public int EqTypeId { get; set; }

    [Column("eq_manufacturer_id")]
    public int EqManufacturerId { get; set; }

    [Column("description")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("condition")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Condition { get; set; }

    [Column("working")]
    public bool? Working { get; set; }

    [ForeignKey("EqManufacturerId")]
    [InverseProperty("Equipment")]
    public virtual EquipmentManufacturer EqManufacturer { get; set; } = null!;

    [ForeignKey("EqTypeId")]
    [InverseProperty("Equipment")]
    public virtual EquipmentType EqType { get; set; } = null!;

    [InverseProperty("CompatibleWith")]
    public virtual ICollection<EquipmentCompability> EquipmentCompabilityCompatibleWiths { get; set; } = new List<EquipmentCompability>();

    [InverseProperty("Eq")]
    public virtual ICollection<EquipmentCompability> EquipmentCompabilityEqs { get; set; } = new List<EquipmentCompability>();

    [InverseProperty("Eq")]
    public virtual ICollection<ReservationEquipment> ReservationEquipments { get; set; } = new List<ReservationEquipment>();
}
