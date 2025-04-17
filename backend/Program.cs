using System.Text;
using backend.Data;
using backend.Models;
using backend.Services.AuthServices;
using backend.Services.TaskServices;
using backend.Services.UserServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<UserContext>(
    op => op.UseNpgsql(builder.Configuration["ConnectionsDB:UserConnection"]));

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IJwtConstProvider, JwtConstProvider>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddSingleton<IPasswordService, PasswordService>();

builder.Services.Configure<TasksUsersDatabaseSettings>(
    builder.Configuration.GetSection("ConnectionsDB:TasksUsersDatabase"));

builder.Services.AddAuthentication(options => {
    }).AddJwtBearer(op => {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"]!) ?? throw new NullReferenceException("JWT:key nao esta definido no appsettings");
        var issuer = builder.Configuration["JWT:issuer"]! ?? throw new NullReferenceException("JWT:issuer nao esta definido no appsettings");
        var audience = builder.Configuration["JWT:audience"]! ?? throw new NullReferenceException("JWT:audience nao esta definido no appsettings");
        op.TokenValidationParameters = new TokenValidationParameters
        {
            ValidAudience = audience,
            ValidIssuer = issuer,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        });

builder.Services.AddStackExchangeRedisCache(op =>
{
    op.InstanceName = builder.Configuration["ConnectionsDB:RefreshTokenDB:InstanceName"] ?? "ToDo";
    op.Configuration = builder.Configuration["ConnectionsDB:RefreshTokenDB:ConnectionString"] ?? throw new NullReferenceException($"ConnectionsDB:RefreshTokenDB:ConnectionString is null");
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    int maxRetries = 10, delayMilliseconds=5000;

    var db = scope.ServiceProvider.GetRequiredService<UserContext>();
    for(int i=0; i< maxRetries;i++ ){
        try{
            db.Database.Migrate();
            break;
        }
        catch(Exception e){
            Console.WriteLine($"error: {e}");
            Thread.Sleep(delayMilliseconds);
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();