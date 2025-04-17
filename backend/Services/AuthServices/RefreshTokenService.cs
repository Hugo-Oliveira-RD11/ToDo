using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.DTO;
using backend.Models;
using backend.Services.UserServices;
using Microsoft.Extensions.Caching.Distributed;

namespace backend.Services.AuthServices;

public class RefreshTokenService : IRefreshTokenService 
{
    private readonly IDistributedCache _cache;
    private readonly IConfiguration _config;
    private readonly DistributedCacheEntryOptions _options;

    public RefreshTokenService(IDistributedCache cache, IConfiguration config)
    {
        _config = config;
        string absolute = _config["ConnectionsDB:RefreshTokenDB:ExpireAbsolute"] ?? "3";
        string sliding = _config["ConnectionsDB:RefreshTokenDB:ExpireSliding"] ?? "1";

        _cache = cache;
        _options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(Int32.Parse(absolute)),
            SlidingExpiration = TimeSpan.FromDays(Int32.Parse(sliding))
        };
    }

    public async Task<string> GetAsync(string key)
    {
        return  await _cache.GetStringAsync(key) ?? string.Empty;
    }

    public async Task SetAsync(string key, string value)
    {
        await _cache.SetStringAsync(key, value, _options);
    }

}