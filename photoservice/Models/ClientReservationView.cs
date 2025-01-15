using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace photoservice.Models;

[Keyless]
public partial class ClientReservationView
{
    [Column("id_res")]
    public int IdRes { get; set; }

    [Column("service_type")]
    [StringLength(50)]
    [Unicode(false)]
    public string ServiceType { get; set; } = null!;

    [Column("reservation_type")]
    [StringLength(50)]
    [Unicode(false)]
    public string ReservationType { get; set; } = null!;

    [Column("date", TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [Column("status")]
    [StringLength(50)]
    [Unicode(false)]
    public string Status { get; set; } = null!;

    [Column("price", TypeName = "money")]
    public decimal? Price { get; set; }

    [Column("other_info")]
    [StringLength(2000)]
    [Unicode(false)]
    public string? OtherInfo { get; set; }

    [Column("reservation_timestamp", TypeName = "datetime")]
    public DateTime? ReservationTimestamp { get; set; }
}
