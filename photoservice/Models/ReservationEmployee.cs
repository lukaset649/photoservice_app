using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace photoservice.Models;

[Table("reservation_employee")]
public partial class ReservationEmployee
{
    [Key]
    [Column("id_res_emp")]
    public int IdResEmp { get; set; }

    [Column("reservation_id")]
    public int ReservationId { get; set; }

    [Column("employee_id")]
    public int EmployeeId { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("ReservationEmployees")]
    public virtual User Employee { get; set; } = null!;

    [ForeignKey("ReservationId")]
    [InverseProperty("ReservationEmployees")]
    public virtual Reservation Reservation { get; set; } = null!;
}
