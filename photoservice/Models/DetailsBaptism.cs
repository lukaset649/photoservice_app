using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace photoservice.Models;

[Table("details_baptism")]
public partial class DetailsBaptism
{
    [Key]
    [Column("id_det_bap")]
    public int IdDetBap { get; set; }

    [Column("church_address")]
    [StringLength(255)]
    [Unicode(false)]
    public string ChurchAddress { get; set; } = null!;

    [Column("home_address")]
    [StringLength(255)]
    [Unicode(false)]
    public string? HomeAddress { get; set; }

    [Column("ceremony_time")]
    public TimeOnly CeremonyTime { get; set; }
}
