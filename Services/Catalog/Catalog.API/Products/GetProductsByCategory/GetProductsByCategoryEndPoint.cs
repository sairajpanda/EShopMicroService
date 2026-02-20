

using Catalog.API.Products.GetProducts;
using Catalog.API.Products.GetProductsByID;

namespace Catalog.API.Products.GetProductsByCategory;

//Public record GetProductsByCategoryRequest();

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductsByCategoryEndPoint() : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender, IMapper mapper) =>
        {
            var result = await sender.Send(new GetProductsByCategoryQuery(category));

            var response = mapper.Map<GetProductByCategoryResponse>(result);

            return Results.Ok(response);
        })
        .WithName("GetProductsByCategory")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products By Category")
        .WithDescription("Get Products By Category");
    }
}