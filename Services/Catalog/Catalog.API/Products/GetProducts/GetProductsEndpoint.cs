using Azure.Core;
using Catalog.API.Products.CreateProduct;
using Catalog.API.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Catalog.API.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender, IMapper mapper) =>
        {
            var result = await sender.Send(new GetProductsQuery());

            var response = mapper.Map<GetProductsResponse>(result);

            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}
