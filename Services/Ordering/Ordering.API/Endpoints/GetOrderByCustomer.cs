namespace Ordering.API.Endpoints;
using Ordering.Application.Orders.Queries;


public record GetOrdersCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetOrderByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId:guid}", async (Guid customerId, ISender sender) =>
        {
            var query = new GetOrdersCustomerQuery(customerId);
            var result = await sender.Send(query);
            var response = new GetOrdersCustomerResponse(result.Orders);
            return Results.Ok(response);
        })
        .WithName("GetOrdersByCustomer")
        .Produces<GetOrdersCustomerResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Gets orders by customer")
        .WithDescription("Gets all orders for a specific customer by their ID.");
    }
}