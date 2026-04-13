namespace Ordering.API.Endpoints;

public record UpdateOrderRequest(OrderDto order) : ICommand<UpdateOrderResponse>;

public record UpdateOrderResponse(bool IsSuccess);


public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders/{id}", async (Guid id, UpdateOrderRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateOrderCommand>();
            command.Id = id;
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateOrderResponse>();
            return Results.Ok(response);
        })
        .WithName("UpdateOrder")
        .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Updates an existing order")
        .WithDescription("Updates an existing order with the provided details.");
    }
}