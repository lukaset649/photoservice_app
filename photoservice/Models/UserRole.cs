using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace photoservice.Models;

[Table("user_role")]
public partial class UserRole
{
    [Key]
    [Column("id_ur")]
    public int IdUr { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("role_id")]
    public int? RoleId { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("UserRoles")]
    public virtual Role? Role { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserRoles")]
    public virtual User? User { get; set; }
}
