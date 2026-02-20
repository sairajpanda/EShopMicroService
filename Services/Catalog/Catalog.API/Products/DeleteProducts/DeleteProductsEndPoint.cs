using Azure;
using Catalog.API.Products.UpdateProducts;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.API.Products.DeleteProducts;

public record DeleteProductRequest
(
    Guid Id
);

public record DeleteProductResponse
(
    bool success
);


public class DeleteProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id:guid}", async (Guid id, IMapper mapper, ISender sender) =>
        {
            var command = mapper.Map<DeleteProductCommand>(id);

            var result = await sender.Send(command);

            return Results.Ok(result);
        })
        .WithName("DeleteProduct")
       .Produces<DeleteProductResponse>(StatusCodes.Status201Created)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .WithSummary("Delete Product")
       .WithDescription("Delete Product");
    }
}
