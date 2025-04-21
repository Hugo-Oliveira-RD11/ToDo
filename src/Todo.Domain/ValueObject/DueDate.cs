namespace Todo.Domain.ValueObjects;

public class DueDate
{
    public DateTime Value { get; }

    private DueDate() => Value = DateTime.MinValue;

    public DueDate(DateTime value)
    {
        if (value.Date < DateTime.UtcNow.Date)
            throw new ArgumentException("A data de conclusão não pode ser no passado.");

        Value = value;
    }

    public static DueDate Empty => new DueDate();

    public override bool Equals(object? obj) =>
        obj is DueDate other && Value.Equals(other.Value);

    public bool Equals(DueDate? other) => other is not null && Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value.ToString("yyyy-MM-dd");

    public static bool operator ==(DueDate? left, DueDate? right) => Equals(left, right);
    public static bool operator !=(DueDate? left, DueDate? right) => !Equals(left, right);
}