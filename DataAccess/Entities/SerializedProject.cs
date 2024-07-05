using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public class SerializedProject
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(127)]
    public string Name { get; set; } = "";

    [Column(TypeName = "nvarchar(max)")]
    public string ProjectJson { get; set; } = "";

    public Guid? ReferenceProjectId { get; set; }
}