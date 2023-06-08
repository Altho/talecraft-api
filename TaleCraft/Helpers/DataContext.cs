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
    public DbSet<Page> Pages { get; set; }
    public DbSet<PageLink> PageLinks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Story>()
            .HasOne(s => s.FirstPage)
            .WithOne()
            .HasForeignKey<Story>(s => s.FirstPageId);

        modelBuilder.Entity<PageLink>()
            .HasKey(pl => new { pl.SourcePageId, pl.TargetPageId });

        modelBuilder.Entity<PageLink>()
            .HasOne(pl => pl.SourcePage)
            .WithMany(p => p.PageLinks)
            .HasForeignKey(pl => pl.SourcePageId);

        modelBuilder.Entity<PageLink>()
            .HasOne(pl => pl.TargetPage)
            .WithMany()
            .HasForeignKey(pl => pl.TargetPageId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}