namespace MusicHub.Data;

using Microsoft.EntityFrameworkCore;
using MusicHub.Data.Models;

public class MusicHubDbContext : DbContext
{
    public MusicHubDbContext()
    {
    }

    public MusicHubDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseSqlServer(Configuration.ConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Song>(entity =>
        {
            entity
                .Property(s => s.CreatedOn)
                .HasColumnType("date");
        });

        builder.Entity<Album>(entity =>
        {
            entity
                .Property(a => a.ReleaseDate)
                .HasColumnType("date");
        });

        builder.Entity<SongPerformer>(entity =>
        {
            entity
                .HasKey(pk => new { pk.PerformerId, pk.SongId});
        });
    }
}
