using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Threading.Tasks;

namespace MyAirVinyl.Controllers;

public class PersonController : ODataController
{
    public PersonController()
    {
        
    }

    [EnableQuery]
    public IActionResult Get()
    {
        return Ok();
    }
}
