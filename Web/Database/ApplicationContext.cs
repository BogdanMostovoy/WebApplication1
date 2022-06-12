using Microsoft.EntityFrameworkCore;
using Web.Models;
 
namespace Web.Database;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Announce> Announces { get; set; }
    public DbSet<News> News { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    { }
}

