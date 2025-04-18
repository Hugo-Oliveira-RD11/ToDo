namespace backend.Services.AuthServices;

public interface IRefreshTokenService
{
    Task<string> GetAsync(string key); 
    Task SetAsync(string key,string value);
}