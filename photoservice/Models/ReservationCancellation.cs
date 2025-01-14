using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace photoservice.Models;

[Table("reservation_cancellation")]
public partial class ReservationCancellation
{
    [Key]
    [Column("id_cancell")]
    public int IdCancell { get; set; }

    [Column("res_id")]
    public int ResId { get; set; }

    [Column("cancelled_by")]
    public int CancelledBy { get; set; }

    [Column("cancell_reason")]
    [StringLength(500)]
    [Unicode(false)]
    public string CancellReason { get; set; } = null!;

    [Column("cancell_date", TypeName = "datetime")]
    public DateTime? CancellDate { get; set; }

    [ForeignKey("CancelledBy")]
    [InverseProperty("ReservationCancellations")]
    public virtual User CancelledByNavigation { get; set; } = null!;

    [ForeignKey("ResId")]
    [InverseProperty("ReservationCancellations")]
    public virtual Reservation Res { get; set; } = null!;
}
