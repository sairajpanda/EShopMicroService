using System.Reflection;

namespace Basket.API.Basket.GetBaskets;

//public record GetbasketRequest(string UserName);

public record GetbasketResponse(ShoppingCart _shoppingCart);

public class GetBasketsEndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender,IMapper mapper) =>
        {
            var result = await sender.Send(new GetbasketQuery(userName));
            var response = mapper.Map<GetbasketResponse>(result);
            return Results.Ok(response);
        }).
        WithName("GetBaskets")
        .Produces<GetbasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Baskets")
        .WithDescription("Get Baskets");
    }
}
