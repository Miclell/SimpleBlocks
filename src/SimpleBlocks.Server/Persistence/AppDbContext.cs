using Microsoft.EntityFrameworkCore;
using SimpleBlocks.Server.Domain.Entities;

namespace SimpleBlocks.Server.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<LanguageFileSet> LanguageFileSets { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LanguageFileSet>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.LanguageKey).IsRequired();
            entity.Property(e => e.BlocksJson).IsRequired();
            entity.Property(e => e.SemanticsJson).IsRequired();
            
            entity.HasIndex(e => e.LanguageKey).IsUnique();
        });
    }
}