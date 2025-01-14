using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace photoservice.Models;

[Table("reservation_equipment")]
public partial class ReservationEquipment
{
    [Key]
    [Column("id_res_eq")]
    public int IdResEq { get; set; }

    [Column("res_id")]
    public int ResId { get; set; }

    [Column("eq_id")]
    public int EqId { get; set; }

    [ForeignKey("EqId")]
    [InverseProperty("ReservationEquipments")]
    public virtual Equipment Eq { get; set; } = null!;

    [ForeignKey("ResId")]
    [InverseProperty("ReservationEquipments")]
    public virtual Reservation Res { get; set; } = null!;
}
