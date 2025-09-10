using AirVinyContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AirVinylRepository;

public class AirVinylRepo(ILogger<AirVinylRepo> logger, IDbContextFactory<MyAirVinylCtx> factory)
    : IAirVinylRepo
{
    public ILogger<AirVinylRepo> Logger { get; } = logger;
    public IDbContextFactory<MyAirVinylCtx> Factory { get; } = factory;

    public void Create(Person person)
    {
        throw new NotImplementedException();
    }

    public void Delete(Person person)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Person> GetAll()
    {
        using var context = Factory.CreateDbContext();
        return context.People.AsNoTracking().AsQueryable();
    }

    public IQueryable<Person> GetById(int id)
    {
        using var context = Factory.CreateDbContext();

        return context.People
            .Include(a => a.VinylRecords)
            .AsQueryable()
            .Where(c => c.PersonId == id);
    }

    public void Update(Person person)
    {
        throw new NotImplementedException();
    }
}
