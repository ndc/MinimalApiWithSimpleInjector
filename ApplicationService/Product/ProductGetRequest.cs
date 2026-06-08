using OneOf;

namespace ApplicationService.Product;

public class ProductGetRequest : ICommand<OneOf<ProductGetResponse, ProductGetError>>
{
    public string Category { get; set; }
}
