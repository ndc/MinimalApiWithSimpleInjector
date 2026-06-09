using Microsoft.AspNetCore.Http.HttpResults;

namespace MinimalApiWithSimpleInjector.Endpoint;

public class ProductGet()
{
    public async Task<Results<Ok<ProductGetResponse>, BadRequest>> ExecuteAsync(HttpContext context)
    {
        await Task.Delay(1);
        return TypedResults.Ok(new ProductGetResponse { Code = Guid.NewGuid().ToString() });
    }

    public async Task<Results<Ok<ProductGetResponse>, BadRequest>> WithoutHttpContext(HttpClient httpClient)
    {
        await Task.Delay(1);
        return TypedResults.Ok(new ProductGetResponse { Code = Guid.NewGuid().ToString() });
    }
}

public class ProductGetResponse
{
    public string? Code { get; set; }
}