namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest
    (
        string Name,
        string Description,
        decimal Price,
        List<string> Category,
        string ImageFile
    );

    public record CreateProductResponse
    (
        Guid Id
    );



    public class CreateProductEndPoint
    {


        
    }
}
