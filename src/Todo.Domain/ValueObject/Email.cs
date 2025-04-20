using Todo.Domain.Validators;

namespace Todo.Domain.ValueObject;

public class Email
{
    public string Value { get; }


    public Email(){}

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email não pode ser vazio.");

        if (!Regex.IsMatch(value, RegexPatterns.Email))
            throw new ArgumentException("Email inválido.");

        Value = value;
    }

    private Email() => Value = string.Empty;

    public static Email Empty => new Email();

    public override bool Equals(object? obj) =>
        obj is Email other && Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);

    public bool Equals(Email? other) => other is not null && Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);

    public override int GetHashCode() => Value.ToLowerInvariant().GetHashCode();

    public override string ToString() => Value;

    public static bool operator ==(Email? left, Email? right) => Equals(left, right);
    public static bool operator !=(Email? left, Email? right) => !Equals(left, right);
}