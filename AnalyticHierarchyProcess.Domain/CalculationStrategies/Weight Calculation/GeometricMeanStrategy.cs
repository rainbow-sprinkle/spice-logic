using AnalyticHierarchyProcess.Domain.Models;

namespace AnalyticHierarchyProcess.Domain.CalculationStrategies.Weight_Calculation;

public class GeometricMeanStrategy : IAhpCalculationStrategy
{
    public ItemWithWeight[] CalculatePriorities(ItemWithWeight[,] matrix)
    {
        int rowCount = matrix.GetLength(0);
        int colCount = matrix.GetLength(1);
        ItemWithWeight[] priorities = new ItemWithWeight[rowCount];
        for (int i = 0; i < rowCount; i++)
        {
            double product = 1;
            for (int j = 0; j < colCount; j++)
            {
                product *= matrix[i, j].Weight;
            }
            double geometricMean = Math.Pow(product, 1.0 / colCount);
            priorities[i] = new ItemWithWeight(matrix[i, 0].ItemName, geometricMean);
        }
        return NormalizePriorities(priorities);
    }
    private ItemWithWeight[] NormalizePriorities(ItemWithWeight[] priorities)
    {
        double sum = priorities.Sum(item => item.Weight);
        for (int i = 0; i < priorities.Length; i++)
        {
            priorities[i] = new ItemWithWeight(priorities[i].ItemName, priorities[i].Weight / sum);
        }
        return priorities;
    }
    
    public double CalculateConsistencyRatio(ItemWithWeight[,] matrix)
    {
        int rowCount = matrix.GetLength(0);
        int colCount = matrix.GetLength(1);
        double consistencyRatio = 0.0;
        for (int i = 0; i < rowCount; i++)
        {
            double rowSum = 0;
            for (int j = 0; j < colCount; j++)
            {
                rowSum += matrix[i, j].Weight;
            }
            consistencyRatio += rowSum / (colCount * matrix[i, 0].Weight);
        }
        consistencyRatio /= rowCount;
        return consistencyRatio;
    }
}