namespace Catalog.API.Products.CreateProduct;

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



public class CreateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        app.MapPost("/products", async (CreateProductRequest request, IMapper mapper, ISender sender) =>
        {
            var command = mapper.Map<CreateProductCommand>(request);

            var result = await sender.Send(command);

            var resposne = mapper.Map<CreateProductResponse>(result);

            return Results.Created($"/products/{result.Id}", resposne);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product");
    }
}
