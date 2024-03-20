namespace SEORankTracker.App.Server.Extensions;

public static class MinimalatrExtensions
{
    public static WebApplication MediateGet<TRequest>(this WebApplication app, string route, Type[] filter_types = default!) where TRequest : IRequest
    {
        app.MapGet(route, async (IMediator mediator, [AsParameters] TRequest request) => await mediator.Send(request)).AddFilters(filter_types);
        return app;
    }

    public static WebApplication MediateGet<TRequest, TResponse>(this WebApplication app, string route, Type[] filter_types = default!) where TRequest : IRequest<TResponse>
    {
        app.MapGet(route, async (IMediator mediator, [AsParameters] TRequest request) => await mediator.Send(request)).AddFilters(filter_types);
        return app;
    }

    public static WebApplication MediatePost<TRequest>(this WebApplication app, string route, Type[] filter_types = default!) where TRequest : IRequest
    {
        app.MapPost(route, async (IMediator mediator, TRequest request) => await mediator.Send(request)).AddFilters(filter_types);
        return app;
    }

    public static WebApplication MediatePost<TRequest, TResponse>(this WebApplication app, string route, Type[] filter_types = default!) where TRequest : IRequest<TResponse>
    {
        app.MapPost(route, async (IMediator mediator, TRequest request) => await mediator.Send(request)).AddFilters(filter_types);
        return app;
    }

    private static RouteHandlerBuilder AddFilters(this RouteHandlerBuilder route_builder, Type[] filter_types)
    {
        if (filter_types is null || filter_types.Length < 1)
        {
            return route_builder;
        }

        foreach (Type type in filter_types)
        {
            if (typeof(IEndpointFilter).IsAssignableFrom(type) is false)
            {
                continue;
            }

            MethodInfo? mi = typeof(EndpointFilterExtensions)
                .GetMethods()
                .FirstOrDefault(m => m.Name == nameof(EndpointFilterExtensions.AddEndpointFilter)
                    && m.IsGenericMethod
                    && m.GetParameters().Length == 1
                    && m.GetParameters()[0].ParameterType == typeof(RouteHandlerBuilder));

            if (mi is null) { continue; }

            MethodInfo generic = mi.MakeGenericMethod(type);
            generic.Invoke(null, [route_builder]);

        }

        return route_builder;
    }
}
