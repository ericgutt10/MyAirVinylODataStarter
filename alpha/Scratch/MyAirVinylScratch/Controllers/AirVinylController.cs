using AirVinyContext.Entities;
using AirVinylRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection.Metadata;

namespace MyAirVinylScratch.Controllers;

[Route("odata/[controller]")]
public class AirVinylController : ODataController, IDisposable
{
    private bool disposed;

    public ILogger<AirVinylController> Logger { get; }
    public IDbContextFactory<MyAirVinylCtx> Factory { get; }
    public MyAirVinylCtx Context { get; }

    public AirVinylController(ILogger<AirVinylController> logger, IDbContextFactory<MyAirVinylCtx> factory)
    {
        Logger = logger;
        Factory = factory;
        Context = Factory.CreateDbContext();
    }

    [EnableQuery(PageSize = 3)]
    [HttpGet]
    public IQueryable<Person> Get()
    {
        return Context.People.AsNoTracking().AsQueryable();
    }

    [EnableQuery]
    [HttpGet("{id}")]
    public SingleResult<Person> Get([FromODataUri] int key)
    {
        var qry = Context.People
            .Include(a => a.VinylRecords)
            .AsQueryable()
            .Where(c => c.PersonId == key);
        return SingleResult.Create(qry);
    }

    // Implement IDisposable.
    // Do not make this method virtual.
    // A derived class should not be able to override this method.
    public void Dispose()
    {
        Dispose(disposing: true);
        // This object will be cleaned up by the Dispose method.
        // Therefore, you should call GC.SuppressFinalize to
        // take this object off the finalization queue
        // and prevent finalization code for this object
        // from executing a second time.
        GC.SuppressFinalize(this);
    }

    // Dispose(bool disposing) executes in two distinct scenarios.
    // If disposing equals true, the method has been called directly
    // or indirectly by a user's code. Managed and unmanaged resources
    // can be disposed.
    // If disposing equals false, the method has been called by the
    // runtime from inside the finalizer and you should not reference
    // other objects. Only unmanaged resources can be disposed.
    protected virtual void Dispose(bool disposing)
    {
        // Check to see if Dispose has already been called.
        if (!disposed)
        {
            // If disposing equals true, dispose all managed
            // and unmanaged resources.
            if (disposing)
            {
                // Dispose managed resources.
                Context.Dispose();
            }

            // Note disposing has been done.
            disposed = true;
        }
    }

    // Use C# finalizer syntax for finalization code.
    // This finalizer will run only if the Dispose method
    // does not get called.
    // It gives your base class the opportunity to finalize.
    // Do not provide finalizer in types derived from this class.
    ~AirVinylController()
    {
        // Do not re-create Dispose clean-up code here.
        // Calling Dispose(disposing: false) is optimal in terms of
        // readability and maintainability.
        Dispose(disposing: false);
    }
}