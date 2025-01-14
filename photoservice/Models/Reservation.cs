using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace photoservice.Models;

[Table("reservation")]
public partial class Reservation
{
    [Key]
    [Column("id_res")]
    public int IdRes { get; set; }

    [Column("client_id")]
    public int ClientId { get; set; }

    [Column("service_id")]
    public int ServiceId { get; set; }

    [Column("status_id")]
    public int StatusId { get; set; }

    [Column("date", TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [Column("deadline")]
    public DateOnly? Deadline { get; set; }

    [Column("finished_date")]
    public DateOnly? FinishedDate { get; set; }

    [Column("price", TypeName = "money")]
    public decimal? Price { get; set; }

    [Column("other_info")]
    [StringLength(2000)]
    [Unicode(false)]
    public string? OtherInfo { get; set; }

    [Column("reservation_date", TypeName = "datetime")]
    public DateTime? ReservationDate { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("Reservations")]
    public virtual User Client { get; set; } = null!;

    [InverseProperty("Res")]
    public virtual ICollection<ReservationCancellation> ReservationCancellations { get; set; } = new List<ReservationCancellation>();

    [InverseProperty("Reservation")]
    public virtual ICollection<ReservationDetail> ReservationDetails { get; set; } = new List<ReservationDetail>();

    [InverseProperty("Reservation")]
    public virtual ICollection<ReservationEmployee> ReservationEmployees { get; set; } = new List<ReservationEmployee>();

    [InverseProperty("Res")]
    public virtual ICollection<ReservationEquipment> ReservationEquipments { get; set; } = new List<ReservationEquipment>();

    [ForeignKey("ServiceId")]
    [InverseProperty("Reservations")]
    public virtual ServiceType Service { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("Reservations")]
    public virtual Status Status { get; set; } = null!;
}
