using Microsoft.AspNetCore.Mvc;
using MinimalApiWithSimpleInjector.Endpoint;

namespace MinimalApiWithSimpleInjector;

public static class EndpointRegistration
{
    public static void RegisterEndpoints(WebApplication app, SimpleInjector.Container container)
    {
        app.MapGet("/product", async (
            [FromQuery] string Category
            ) => await container.GetInstance<ProductGet>().ExecuteAsync(Category));
    }
}
