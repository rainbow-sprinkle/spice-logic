using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public class GlobalPreferences
{
    [Key]
    public long Id { get; set; }

    [StringLength(450)]
    public string UserId { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; } = null!;
}