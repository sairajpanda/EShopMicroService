namespace Basket.API.Basket.GetBaskets;

public record DeleteBasketRequest(string Username);

public record DeleteBasketResponse(bool IsSuccess);


public class DeleteBasketsEndPoints : ICarterModule
{
    public async void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket", async (DeleteBasketRequest request, ISender sender, IMapper mapper) =>
        {
            var command = mapper.Map<DeleteBasketsCommand>(request);
            var results = await sender.Send(command);
            var response = mapper.Map<DeleteBasketResponse>(results);
            return Results.Ok(response);
        })
        .WithName("DeleteBaskets")
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Baskets")
        .WithDescription("Delete Baskets");
    }
}
