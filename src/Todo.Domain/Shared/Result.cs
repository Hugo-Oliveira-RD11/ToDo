namespace Todo.Domain.Shared;

public class Result<T>
{
    public bool Success { get; private set; }
    public string Error { get; private set; }
    public T Data { get; private set; }

    public static Result<T> Ok(T data) => new Result<T> { Success = true, Data = data };
    public static Result<T> Ok() => new Result<T> { Success = true };
    public static Result<T> Fail(string error) => new Result<T> { Success = false, Error = error };
}
