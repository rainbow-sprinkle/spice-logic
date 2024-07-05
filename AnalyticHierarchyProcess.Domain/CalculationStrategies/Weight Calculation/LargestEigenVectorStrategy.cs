using AnalyticHierarchyProcess.Domain.Models;
using FrameworkUtilities;

namespace AnalyticHierarchyProcess.Domain.CalculationStrategies.Weight_Calculation;

public class LargestEigenVectorStrategy : IAhpCalculationStrategy
{
    public ItemWithWeight[] CalculatePriorities(ItemWithWeight[,] matrix)
    {
        int rowCount = matrix.GetLength(0);
        int colCount = matrix.GetLength(1);
        // Initialize the vector with 1s
        double[] vector = new double[rowCount];
        for (int i = 0; i < rowCount; i++)
        {
            vector[i] = 1;
        }
        double[] newVector;
        do
        {
            newVector = new double[rowCount];
            // Multiply the matrix by the vector
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    newVector[i] += matrix[i, j].Weight * vector[j];
                }
            }
            // Normalize the new vector
            double length = Math.Sqrt(newVector.Sum(x => x * x));
            for (int i = 0; i < rowCount; i++)
            {
                newVector[i] /= length;
            }
            // Check if the vector has stabilized
        } while (!vector.SequenceEqual(newVector, DoubleEqualityComparer.Instance)); // Or some other small number
        // Convert the vector to ItemWithWeight[]
        ItemWithWeight[] priorities = new ItemWithWeight[rowCount];
        for (int i = 0; i < rowCount; i++)
        {
            priorities[i] = new ItemWithWeight(matrix[i, 0].ItemName, newVector[i]);
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