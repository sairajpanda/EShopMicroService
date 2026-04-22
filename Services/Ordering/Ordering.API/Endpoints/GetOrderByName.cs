using Ordering.Application.Orders.Queries;

namespace Ordering.API.Endpoints;

public record GetProductsByNameResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{Name}", async (string Name, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersByNameQuery(Name));
            var response = result.Adapt<GetProductsByNameResponse>();
            return Results.Ok(response);
        })
        .WithName("GetOrdersByName")
        .Produces<GetProductsByNameResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Searches for orders by name")
        .WithDescription("Searches for orders that contain the specified name in their order name.");
    }
}