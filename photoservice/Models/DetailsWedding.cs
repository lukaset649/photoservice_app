using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace photoservice.Models;

[Table("details_wedding")]
public partial class DetailsWedding
{
    [Key]
    [Column("id_det_wed")]
    public int IdDetWed { get; set; }

    [Column("groom_address")]
    [StringLength(255)]
    [Unicode(false)]
    public string GroomAddress { get; set; } = null!;

    [Column("groom_prep_time")]
    public TimeOnly? GroomPrepTime { get; set; }

    [Column("bride_address")]
    [StringLength(255)]
    [Unicode(false)]
    public string BrideAddress { get; set; } = null!;

    [Column("bride_prep_time")]
    public TimeOnly? BridePrepTime { get; set; }

    [Column("ceremony_address")]
    [StringLength(255)]
    [Unicode(false)]
    public string CeremonyAddress { get; set; } = null!;

    [Column("ceremony_time")]
    public TimeOnly CeremonyTime { get; set; }

    [Column("church_entry_info")]
    [StringLength(255)]
    [Unicode(false)]
    public string? ChurchEntryInfo { get; set; }

    [Column("documments_signing_info")]
    [StringLength(255)]
    [Unicode(false)]
    public string? DocummentsSigningInfo { get; set; }

    [Column("church_exit_info")]
    [StringLength(255)]
    [Unicode(false)]
    public string? ChurchExitInfo { get; set; }

    [Column("compliments_info")]
    [StringLength(255)]
    [Unicode(false)]
    public string? ComplimentsInfo { get; set; }

    [Column("wedding_hall_address")]
    [StringLength(255)]
    [Unicode(false)]
    public string WeddingHallAddress { get; set; } = null!;

    [Column("musical_band_info")]
    [StringLength(255)]
    [Unicode(false)]
    public string? MusicalBandInfo { get; set; }

    [Column("additional_attractions")]
    [StringLength(500)]
    [Unicode(false)]
    public string? AdditionalAttractions { get; set; }
}
