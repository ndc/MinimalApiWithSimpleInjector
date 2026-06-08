using ApplicationService;
using ApplicationService.Product;
using Microsoft.AspNetCore.Http.HttpResults;
using OneOf;

namespace MinimalApiWithSimpleInjector.Endpoint;

public class ProductGet(
    ICommandHandler<ProductGetRequest, OneOf<ProductGetResponse, ProductGetError>> handler
    )
{
    public async Task<Results<Ok<ProductGetResponse>, BadRequest>> ExecuteAsync(string Category)
    {
        var command = new ProductGetRequest
        {
            Category = Category
        };
        var result = await handler.HandleAsync(command);

        if (result.TryPickT0(out var product, out var error))
        {
            return TypedResults.Ok(product);
        }
        else
        {
            return TypedResults.BadRequest();
        }
    }
}
