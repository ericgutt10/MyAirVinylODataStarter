using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomersODataApp.Controllers;

public record Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
}

public class PeopleController : ODataController
{
    private static readonly List<Person> _people =
    [
        new Person { Id = 1, Name = "Alice", Age = 30 },
        new Person { Id = 2, Name = "Bob", Age = 25 },
        new Person { Id = 3, Name = "Charlie", Age = 35 }
    ];

    public async Task<IActionResult> Get()
    {
        return Ok(await Task.FromResult(_people));
    }
}
