using Microsoft.FluentUI.AspNetCore.Components;


/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================
namespace Beta.UI.Components;

public interface IFluentGridPresenter<TGridItem>
{
    public ValueTask<GridItemsProviderResult<TGridItem>> GetItemsAsync(GridItemsProviderRequest<TGridItem> request);
}
