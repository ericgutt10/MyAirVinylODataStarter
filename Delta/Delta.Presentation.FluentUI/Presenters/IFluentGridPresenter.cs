/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================
using Blazr.OneWayStreet.Core;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Delta.Presentation.FluentUI.Presenters;

public interface IFluentGridPresenter
{
    public IDataResult LastDataResult { get; }
    public int DefaultPageSize { get; set; }
    public List<FilterDefinition>? Filters { get; set; }

    public ValueTask<GridItemsProviderResult<TGridItem>> GetItemsAsync<TGridItem>(GridItemsProviderRequest<TGridItem> request)
    where TGridItem : class, new ();
}
