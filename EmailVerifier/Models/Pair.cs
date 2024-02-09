namespace EmailVerifier.Models;

public class Pair<T1, T2>(T1 first, T2 second)
{
    public T1 First { get; } = first;
    public T2 Second { get; } = second;

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var other = (Pair<T1, T2>)obj;
        return EqualityComparer<T1>.Default.Equals(First, other.First) &&
               EqualityComparer<T2>.Default.Equals(Second, other.Second);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(First, Second);
    }

    public override string ToString()
    {
        return $"({First}, {Second})";
    }
}