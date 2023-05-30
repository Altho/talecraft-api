namespace TaleCraft.Helpers;

using Microsoft.EntityFrameworkCore;
using TaleCraft.Models;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<Story> Stories { get; set; }
    public DbSet<User> Users { get; set; }
}