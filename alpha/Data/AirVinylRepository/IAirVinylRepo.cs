using AirVinyContext.Entities;

namespace AirVinylRepository;

public interface IAirVinylRepo
{
    public IQueryable<Person> GetAll();
    public IQueryable<Person> GetById(int id);
    public void Create(Person person);
    public void Update(Person person);
    public void Delete(Person person);
}