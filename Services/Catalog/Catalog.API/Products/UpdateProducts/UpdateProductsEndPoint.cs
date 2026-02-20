
namespace Catalog.API.Products.UpdateProducts;


public record UpdateProductRequest
(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    List<string> Category,
    string ImageFile

);

public record UpdateProductResponse
(
    bool success
);

public class UpdateProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, IMapper mapper, ISender sender) =>
        {
            var command = mapper.Map<UpdateProductCommand>(request);

            var result = await sender.Send(command);

            var resposne = mapper.Map<UpdateProductResponse>(result);

            return Results.Ok(resposne);
        })
       .WithName("UpdateProduct")
       .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .WithSummary("Update Product")
       .WithDescription("Update Product");
    }
}
