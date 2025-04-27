using Microsoft.EntityFrameworkCore;
using Todo.Infrastructure.Data.Model;

namespace Todo.Infrastructure.Data;
public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options){}

    public DbSet<UserModel> Users { get; set; }

}