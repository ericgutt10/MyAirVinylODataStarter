/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

using Delta.Core.Libraries.Toasts;
using Delta.UI.Libs;
using Microsoft.Extensions.DependencyInjection;

namespace Blazr.App.UI.FluentUI;

public static class ApplicationFluentUIServices
{
    public static void AddAppFluentUIServices(this IServiceCollection services)
    {
        services.AddScoped<IAppToastService, FluentUIToastService>();
    }
}