using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace photoservice.Models;

[Table("details_photoshoot")]
public partial class DetailsPhotoshoot
{
    [Key]
    [Column("id_det_ps")]
    public int IdDetPs { get; set; }

    [Column("localisation")]
    [StringLength(255)]
    [Unicode(false)]
    public string Localisation { get; set; } = null!;

    [Column("transport")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Transport { get; set; }

    [Column("num_of_participants")]
    public int NumOfParticipants { get; set; }
}
