using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public class Invitee
{
    [Key]
    public long Id { get; set; }
    
    public DateTime InviteDateTime { get; set; }

    [StringLength(127)] 
    public string Email { get; set; } = "";
     
    public long OrganizationId { get; set; }

    [ForeignKey(nameof(OrganizationId))] 
    public Organization Organization { get; set; } = null!;
    
    public bool IsAccepted { get; set; }
}