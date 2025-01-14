using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace photoservice.Models;

[Keyless]
public partial class ActiveUsersWithRole
{
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

    [Column("email")]
    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("role_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string RoleName { get; set; } = null!;
}
