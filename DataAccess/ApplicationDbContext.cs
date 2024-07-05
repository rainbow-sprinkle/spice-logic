#nullable disable

using DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public DbSet<Organization> Organizations { get; set; }

    public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }

    public DbSet<GlobalPreferences> GlobalPreferences { get; set; }

    public DbSet<SerializedProject> SerializedProjects { get; set; }
    
    public DbSet<Invitee> Invitees { get; set; }
    
    public DbSet<SubscriptionEvent> SubscriptionEvents { get; set; }
}