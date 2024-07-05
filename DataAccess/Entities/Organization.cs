using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public class Organization
{
    [Key]
    public long Id { get; set; }

    [StringLength(127)]
    public string Name { get; set; } = "";

    [StringLength(127)]
    public string? SuperAdminUserName { get; set; } = "";

    [StringLength(450)]
    public string? SuperAdminUserId { get; set; }

    [StringLength(127)]
    public string? SuperAdminStripeId { get; set; }
    
    public int NumberOfSeats { get; set; }
    
    public ICollection<SerializedProject> Projects { get; set; } = [];

    public ICollection<ApplicationUser> Members { get; set; } = [];
    
    public ICollection<Invitee> Invitees { get; set; } = [];

    public long? PlanId { get; set; }

    [ForeignKey(nameof(PlanId))]
    public SubscriptionPlan? SubscriptionPlan { get; set; }

    [StringLength(127)]
    public string? StripeSubscriptionId { get; set; }

    [StringLength(255)]
    public string? StripeSubscriptionDetails { get; set; }

    [StringLength(127)]
    public string? StripeSubscriptionPriceId { get; set; }
}