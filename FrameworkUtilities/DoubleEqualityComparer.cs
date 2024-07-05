namespace FrameworkUtilities;

public class DoubleEqualityComparer : IEqualityComparer<double>
{
    public static DoubleEqualityComparer Instance = new();
    
    private const double Epsilon = 0.0001;
    
    public bool Equals(double x, double y)
    {
        return Math.Abs(x - y) < Epsilon;
    }
    public int GetHashCode(double obj)
    {
        return obj.GetHashCode();
    }
}