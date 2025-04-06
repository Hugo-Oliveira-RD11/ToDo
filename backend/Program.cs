using backend.Data;
using backend.Models;
using backend.Services.TaskServices;
using backend.Services.UserServices;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<UserContext>(
    op => op.UseNpgsql(builder.Configuration["ConnectionsDB:UserConnection"]));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddSingleton<IPasswordService, PasswordService>();

builder.Services.Configure<TasksUsersDatabaseSettings>(
    builder.Configuration.GetSection("ConnectionsDB:TasksUsersDatabase"));




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

app.UseAuthorization();

app.MapControllers();

app.Run();