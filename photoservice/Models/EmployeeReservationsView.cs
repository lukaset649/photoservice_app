using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace photoservice.Models;

[Keyless]
public partial class EmployeeReservationsView
{
    [Column("reservation_id")]
    public int ReservationId { get; set; }

    [Column("client_name")]
    [StringLength(101)]
    [Unicode(false)]
    public string ClientName { get; set; } = null!;

    [Column("service_type")]
    [StringLength(50)]
    [Unicode(false)]
    public string ServiceType { get; set; } = null!;

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

    [Column("reservation_date", TypeName = "datetime")]
    public DateTime? ReservationDate { get; set; }

    [Column("employee_name")]
    [StringLength(101)]
    [Unicode(false)]
    public string EmployeeName { get; set; } = null!;
}
