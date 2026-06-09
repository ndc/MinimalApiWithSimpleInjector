using Microsoft.AspNetCore.Mvc;
using MinimalApiWithSimpleInjector.Endpoint;

namespace MinimalApiWithSimpleInjector;

public static class EndpointRegistration
{
    public static void RegisterEndpoints(WebApplication app, SimpleInjector.Container container)
    {
        // this results in 200 but empty response
        app.MapGet("/product", async (
            HttpContext context
            ) => await container.GetInstance<ProductGet>().ExecuteAsync(context));

        // with code block but same everything else: response is populated
        app.MapGet("/succcessfulproduct", async (
            HttpContext context
            ) =>
        {
            var result = await container.GetInstance<ProductGet>().ExecuteAsync(context);
            return result;
        });

        // other dependencies are not producing this issue
        app.MapGet("/withouthttpcontext", async (
            HttpClient httpClient
            ) => await container.GetInstance<ProductGet>().WithoutHttpContext(httpClient));

        // if SimpleInjector is registered in services, response is fine
        app.MapGet("/withcontainerindi", async (
            HttpContext context,
            SimpleInjector.Container containerInner
            ) => await containerInner.GetInstance<ProductGet>().ExecuteAsync(context));
    }
}
