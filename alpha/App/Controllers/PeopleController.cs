using AirVinyContext.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MyAirVinyl.Controllers;

[ApiController]
public class PeopleController(ILogger<PeopleController> logger, IDbContextFactory<MyAirVinylCtx> factory) : ODataController
{
    public ILogger<PeopleController> Logger { get; } = logger;
    public IDbContextFactory<MyAirVinylCtx> Factory { get; } = factory;


    // GET: odata/Customers
    [EnableQuery(PageSize = 10)]
    [HttpGet("odata/People")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
                 nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> AllPeople()
    {
        using var context = Factory.CreateDbContext();


        return Ok(await context.People.AsNoTracking().ToListAsync());
    }
}
