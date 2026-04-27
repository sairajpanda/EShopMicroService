using Basket.API.DBContext;
using Basket.API.Dto;
namespace Basket.API.Basket.CheckoutBasket;

public record CheckOutBasketRequest(BasketCheckoutDto basketCheckoutDto) : ICommand<CheckOutBasketResponse>;

public record CheckOutBasketResponse(bool IsSuccess);

public class CheckOutBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout", async (CheckOutBasketRequest request, BasketDbContext dbContext, ISender sender, IMapper mapper) =>
        {
            var command = mapper.Map<CheckOutBasketCommand>(request);
            var result = await sender.Send(command);
            var response = mapper.Map<CheckOutBasketResponse>(result);
            return Results.Ok(response);
        })
         .WithName("CheckOutBasket")
        .Produces<CheckOutBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("CheckOut Basket")
        .WithDescription("Checkout Basket of Orders");
    }
}