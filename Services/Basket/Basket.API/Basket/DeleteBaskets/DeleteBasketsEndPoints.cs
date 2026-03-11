namespace Basket.API.Basket.GetBaskets;

public record DeleteBasketRequest(String Username);

public record DeleteBasketResponse(bool IsSuccess);


public class DeleteBasketsEndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
       app.MapDelete("/basket/{UserName}", async (String UserName, ISender sender, IMapper mapper) =>
        {
            DeleteBasketsCommand command = new DeleteBasketsCommand(UserName);
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
