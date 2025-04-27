namespace backend.Services.UserServices;

public class GuidService
{
    public static Guid GuidGenerate() =>
        Guid.NewGuid();
}