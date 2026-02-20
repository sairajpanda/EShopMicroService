
using AutoMapper;
using Catalog.API.Models;
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductsByID;
//public record getProductsByIdRequest()

public record GetProdcutsByIdResponse(Product _products);

public class GetProductsByIDHandler : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id,ISender sender, IMapper mapper) =>
        {
            var result = await sender.Send(new GetProdcutsByIDQuery(id));

            var response = mapper.Map<GetProdcutsByIdResponse>(result);

            return response;
        })
        .WithName("GetProductById")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products By Id")
        .WithDescription("Get Products By Id");
    }
}
