namespace AnalyticHierarchyProcess.Domain.Models;

public class AhpCriterion
{
    public Guid Id = Guid.NewGuid();

    public string Name { get; set; } = "";

    public double CalculatedWeight { get; set; }

    public List<AhpCriterion> SubCriteria { get; set; } = [];
}