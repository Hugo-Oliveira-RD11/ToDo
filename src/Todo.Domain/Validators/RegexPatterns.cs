namespace Todo.Domain.Validators;

public static class RegexPatterns
{
    public const string Email = @"^((?!\.)[\w\-_\.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$";
}