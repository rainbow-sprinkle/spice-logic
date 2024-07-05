using AnalyticHierarchyProcess.Domain.Models;

namespace AnalyticHierarchyProcess.Domain.CalculationStrategies.Weight_Calculation;

public interface IAhpCalculationStrategy
{
    ItemWithWeight[] CalculatePriorities(ItemWithWeight[,] matrix);
    double CalculateConsistencyRatio(ItemWithWeight[,] matrix);
}