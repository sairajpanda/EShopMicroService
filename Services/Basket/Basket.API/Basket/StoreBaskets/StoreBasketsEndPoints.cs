namespace Basket.API.Basket.GetBaskets;

public record StoreBasketsRequest(
 string UserName,
 ICollection<ShoppingCartItem> Items,
 decimal TotalItemPrice);

public record StoreBasketsResponse(string UserName);

public class StoreBasketsEndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async(StoreBasketsRequest request, IMapper mapper, ISender sender) =>
        {
            var command = mapper.Map<StoreBasketCommnad>(request);
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
