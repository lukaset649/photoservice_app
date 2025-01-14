using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace photoservice.Models;

[Table("details_other")]
public partial class DetailsOther
{
    [Key]
    [Column("id_det_oth")]
    public int IdDetOth { get; set; }

    [Column("localisation")]
    [StringLength(255)]
    [Unicode(false)]
    public string Localisation { get; set; } = null!;

    [Column("description")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Description { get; set; } = null!;
}
