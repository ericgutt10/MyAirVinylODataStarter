using AirVinyContext.Entities;
using Ardalis.Specification;

namespace AirVinylRepository.People;

public class PeopleSpecification : Specification<Person>
{
    public PeopleSpecification()
    {
        Query.AsNoTracking();
    }

    //public IngredientSpecification(IngredientFilter? filter)
    //{
    //    if (filter == null) { Query.AsNoTracking(); }

    //    Query.AsNoTracking()
    //        .Include(d => d.Dishes)
    //        .Where(d => d.Name.ToLower().Contains(filter!.Name!.ToLower()), filter?.Name is not null)
    //        .Where(d => d.Dishes.Any(i => i.Id == filter!.DishId), filter?.DishId is not null)
    //        ;
    //}
}