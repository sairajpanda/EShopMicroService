using Basket.API.Models;

namespace Basket.API.Basket.GetBaskets;

public record GetbasketQuery(string UserName): IQuery<GetBasketResults>;

public record GetBasketResults(ShoppingCart _shoppingCart);

public class GetBasketsHandler : IQueryHandler<GetbasketQuery, GetBasketResults>
{
    public async Task<GetBasketResults> Handle(GetbasketQuery request, CancellationToken cancellationToken)
    {
        return new GetBasketResults (new ShoppingCart("Sai"));
    }
}
