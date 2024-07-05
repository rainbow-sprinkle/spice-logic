using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities;

public class SubscriptionPlan
{
    [Key]
    public long Id { get; set; }

    [StringLength(127)]
    public string Name { get; set; } = "";

    public double MonthlyCost { get; set; }

    public double AnnualCost { get; set; }

    [StringLength(127)] 
    public string StripePriceId { get; set; } = "";

    public ICollection<ApplicationUser> Users { get; set; } = [];
}
