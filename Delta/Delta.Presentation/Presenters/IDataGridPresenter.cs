using Blazr.OneWayStreet.Core;
using Microsoft.AspNetCore.Components.QuickGrid;

/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================
namespace Delta.Presentation.Presenters;

public interface IDataGridPresenter
{
    public IDataResult LastDataResult { get; }
    public int DefaultPageSize { get; set; }
    public List<FilterDefinition>? Filters { get; set; }

    public ValueTask<GridItemsProviderResult<TGridItem>> GetItemsAsync<TGridItem>(GridItemsProviderRequest<TGridItem> request)
        where TGridItem : class;
}
