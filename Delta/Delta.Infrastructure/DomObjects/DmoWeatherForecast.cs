using Blazr.OneWayStreet.Core;
using System.ComponentModel.DataAnnotations;

/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================
namespace Delta.Infrastructure.DomObjects;

public sealed record DmoWeatherForecast : ICommandEntity, IKeyedEntity
{
    [Key] public Guid Id { get; init; } = Guid.Empty;
    public DateTime Date { get; init; }
    public decimal Temperature { get; set; }
    public string? Summary { get; set; }

    public object KeyValue => Id;
}
