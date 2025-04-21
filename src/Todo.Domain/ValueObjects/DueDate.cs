namespace Todo.Domain.ValueObjects;

public class DueDate
{
    public DateTime Value { get; }

    public DueDate(DateTime value)
    {
        Value = DateTime.SpecifyKind(value.Date, DateTimeKind.Utc);
    }

    public override string ToString() => Value.ToString("yyyy-MM-ddTHH:mm:ssZ");

    public static implicit operator DateTime(DueDate dueDate) => dueDate.Value;

    public static implicit operator DueDate(DateTime dateTime) => new DueDate(dateTime);
}