using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace photoservice.Models;

[Table("messages")]
public partial class Message
{
    [Key]
    [Column("id_mess")]
    public int IdMess { get; set; }

    [Column("sender_id")]
    public int SenderId { get; set; }

    [Column("recipient_id")]
    public int RecipientId { get; set; }

    [Column("mess_content")]
    [StringLength(500)]
    [Unicode(false)]
    public string? MessContent { get; set; }

    [Column("mess_date", TypeName = "datetime")]
    public DateTime? MessDate { get; set; }

    [ForeignKey("RecipientId")]
    [InverseProperty("MessageRecipients")]
    public virtual User Recipient { get; set; } = null!;

    [ForeignKey("SenderId")]
    [InverseProperty("MessageSenders")]
    public virtual User Sender { get; set; } = null!;
}
