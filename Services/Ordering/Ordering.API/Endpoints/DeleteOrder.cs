using Azure.Core;

namespace Ordering.API.Endpoints;

public record DeleteOrderRequest(OrderDto Order) : ICommand<DeleteOrderResponse>;

public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}",async(Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteOrderCommand(id));
            return result;
        })
        .WithName("DeleteOrder")
        .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Deletes an existing order")
        .WithDescription("Deletes an existing order with the provided details.");
    }
}
