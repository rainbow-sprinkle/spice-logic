using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities;

public class ApplicationUser : IdentityUser
{
    public bool IsTrialUser { get; set; }
    
    public DateTime? SubscriptionExpiryDate { get; set; }
    
    public List<SerializedProject> Projects { get; set; } = [];
    
    public long? PlanId { get; set; }

    [ForeignKey(nameof(PlanId))]
    public SubscriptionPlan? SubscriptionPlan { get; set; }

    [StringLength(127)]
    public string? StripeSubscriptionId { get; set; }

    [StringLength(255)]
    public string? StripeSubscriptionDetails { get; set; }

    [StringLength(127)]
    public string? StripeSubscriptionPriceId { get; set; }

    [StringLength(127)]
    public string StripeCustomerId { get; set; } = "";

    [StringLength(127)]
    public string CustomerCountry { get; set; } = "";

    [StringLength(127)]
    public string CustomerPostalCode { get; set; } = "";
    
    public int NumberOfSeats { get; set; }
    
    public long? OrganizationId { get; set; }

    [ForeignKey(nameof(OrganizationId))]
    public Organization? Organization { get; set; }
}