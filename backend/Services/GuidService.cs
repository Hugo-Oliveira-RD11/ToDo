namespace backend.Services;

public class GuidService
{
    public static Guid GuidGenerate() =>
        Guid.NewGuid();
}