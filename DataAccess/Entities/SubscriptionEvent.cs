using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public class SubscriptionEvent
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(127)]
    public string? StripeEventId { get; set; }

    [StringLength(127)]
    public string? Status { get; set; }
    
    public decimal AmountPaid { get; set; } 
    
    public bool IsRevenue { get; set; }

    [StringLength(127)]
    public string StripeCustomerId { get; set; } = "";

    [StringLength(127)]
    public string CustomerEmail { get; set; } = "";

    [StringLength(127)]
    public string CustomerCountry { get; set; } = "";

    [Column(TypeName = "nvarchar(max)")]
    public string NotificationJson { get; set; } = "";
}