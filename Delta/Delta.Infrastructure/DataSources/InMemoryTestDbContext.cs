using Delta.Infrastructure.DomObjects;
using Microsoft.EntityFrameworkCore;

/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================
namespace Delta.Infrastructure.DataSources;

public sealed class InMemoryTestDbContext : DbContext
{
    public DbSet<DmoWeatherForecast> WeatherForecasts { get; set; } = default!;

    public InMemoryTestDbContext(DbContextOptions<InMemoryTestDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DmoWeatherForecast>().ToTable("WeatherForecasts");
    }
}
