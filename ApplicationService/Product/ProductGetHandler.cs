using OneOf;

namespace ApplicationService.Product;

public class ProductGetHandler : ICommandHandler<ProductGetRequest, OneOf<ProductGetResponse, ProductGetError>>
{
    public async Task<OneOf<ProductGetResponse, ProductGetError>> HandleAsync(ProductGetRequest command)
    {
        var result = new ProductGetResponse
        {
            Code = Guid.NewGuid().ToString()
        };
        await Task.Delay(1);
        return result;
    }
}
