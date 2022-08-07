using GamerBot.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace GamerBot.Model;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
}