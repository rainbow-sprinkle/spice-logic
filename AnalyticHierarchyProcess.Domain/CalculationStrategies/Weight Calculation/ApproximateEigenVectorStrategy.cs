using AnalyticHierarchyProcess.Domain.Models;

namespace AnalyticHierarchyProcess.Domain.CalculationStrategies.Weight_Calculation;

public class ApproximateEigenVectorStrategy : IAhpCalculationStrategy
{
    public ItemWithWeight[] CalculatePriorities(ItemWithWeight[,] matrix)
    {
        int rowCount = matrix.GetLength(0);
        int colCount = matrix.GetLength(1);
        ItemWithWeight[] priorities = new ItemWithWeight[rowCount];
        for (int i = 0; i < rowCount; i++)
        {
            double sum = 0;
            for (int j = 0; j < colCount; j++)
            {
                sum += matrix[i, j].Weight;
            }
            priorities[i] = new ItemWithWeight(matrix[i, 0].ItemName, sum / colCount);
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