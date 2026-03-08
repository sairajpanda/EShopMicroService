namespace Basket.API.Basket.GetBaskets;

public record StoreBasketsRequest(ShoppingCart _shoppingCart);

public record StoreBasketsResponse(string UserName);


public class StoreBasketsEndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async(StoreBasketsRequest requst,ISender sender,IMapper mapper) =>
        {
            var command = mapper.Map<StoreBasketCommnad>(requst);
            var results = await sender.Send(command);
            var response = mapper.Map<StoreBasketsResponse>(results);
            return Results.Created($"/basket/{response.UserName}", response);
        }).
        WithName("StoreBaskets")
        .Produces<StoreBasketsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Store Baskets")
        .WithDescription("Store Baskets");
    }
}
