using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace photoservice.Models;

[Table("reservation_details")]
public partial class ReservationDetail
{
    [Key]
    [Column("id_res_det")]
    public int IdResDet { get; set; }

    [Column("reservation_id")]
    public int ReservationId { get; set; }

    [Column("type_id")]
    public int TypeId { get; set; }

    [Column("details_id")]
    public int? DetailsId { get; set; }

    [ForeignKey("ReservationId")]
    [InverseProperty("ReservationDetails")]
    public virtual Reservation Reservation { get; set; } = null!;

    [ForeignKey("TypeId")]
    [InverseProperty("ReservationDetails")]
    public virtual ReservationType Type { get; set; } = null!;
}
