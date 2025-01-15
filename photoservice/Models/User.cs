using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace photoservice.Models;

[Table("users")]
[Index("PhoneNumber", Name = "UQ__users__A1936A6B728D23BA", IsUnique = true)]
[Index("Email", Name = "UQ__users__AB6E6164E2AC179F", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("id_user")]
    public int IdUser { get; set; }

    [Column("f_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string FName { get; set; } = null!;

    [Column("l_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string LName { get; set; } = null!;

    [Column("phone_number")]
    [StringLength(20)]
    [Unicode(false)]
    public string PhoneNumber { get; set; } = null!;

    [Column("email")]
    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("password_hash")]
    [StringLength(255)]
    [Unicode(false)]
    public string PasswordHash { get; set; } = null!;

    [Column("registration_date", TypeName = "datetime")]
    public DateTime? RegistrationDate { get; set; }

    [Column("is_deleted")]
    public bool? IsDeleted { get; set; }

    [InverseProperty("Recipient")]
    public virtual ICollection<Message> MessageRecipients { get; set; } = new List<Message>();

    [InverseProperty("Sender")]
    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();

    [InverseProperty("CancelledByNavigation")]
    public virtual ICollection<ReservationCancellation> ReservationCancellations { get; set; } = new List<ReservationCancellation>();

    [InverseProperty("Employee")]
    public virtual ICollection<ReservationEmployee> ReservationEmployees { get; set; } = new List<ReservationEmployee>();

    [InverseProperty("Client")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    [InverseProperty("User")]
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
