namespace AnalyticHierarchyProcess.Domain.Models;

public class AhpProject
{
    public Guid ProjectGuid { get; set; } = Guid.NewGuid();

    public long OrganizationId { get; set; }

    public string ProjectName { get; set; } = "";

    public string OwnerUserName { get; set; } = "";

    public List<AhpCriterion> Criteria { get; set; } = [];

    public List<AhpOption> Options { get; set; } = [];
}