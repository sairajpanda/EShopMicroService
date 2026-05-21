using Basket.API.Data;
using Basket.API.Models;

namespace Basket.API.Basket.GetBaskets;

public record GetbasketQuery(string UserName): IQuery<GetBasketResults>;

public record GetBasketResults(ShoppingCart _shoppingCart);

public class GetBasketsHandler(IBasketRepository _BasketRepository) : IQueryHandler<GetbasketQuery, GetBasketResults>
{
    public async Task<GetBasketResults> Handle(GetbasketQuery request, CancellationToken cancellationToken)
    {
		try
		{
            var results = await _BasketRepository.GetBasket(request.UserName, cancellationToken);
            return new GetBasketResults(results);
        }
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
			throw;
		}
       
    }
}
