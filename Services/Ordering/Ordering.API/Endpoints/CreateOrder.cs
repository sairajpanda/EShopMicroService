using Microsoft.AspNetCore.Mvc;

namespace Ordering.API.Endpoints;

public record CreateOrderRequest(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResponse(Guid Id);


public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async ([FromBody] CreateOrderRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateOrderCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateOrderResponse>();
            return Results.Created($"/orders/{result.Id}", response);
        })
        .WithName("CreateOrder")
        .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Creates a new order")
        .WithDescription("Creates a new order with the provided details.");

    }
}
