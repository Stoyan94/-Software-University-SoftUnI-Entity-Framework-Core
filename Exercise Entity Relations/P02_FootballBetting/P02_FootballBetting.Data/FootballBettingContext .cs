using Microsoft.EntityFrameworkCore;
using P02_FootballBetting.Data.Common;
using P02_FootballBetting.Data.Models;

namespace P02_FootballBetting.Data;

public class FootballBettingContext : DbContext
{
    public FootballBettingContext()
    {
        
    }

    public FootballBettingContext(DbContextOptions options) 
        :base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(DbConfig.ConnectionString);
        }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PlayerStatistic>(entity =>
        {
            entity.HasKey(ps => new { ps.GameId, ps.PlayerId });
        });
    }
}

