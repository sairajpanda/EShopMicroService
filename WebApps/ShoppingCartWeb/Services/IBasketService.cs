using Refit;
using ShoppingCartWeb.Models.Basket;
using ShoppingCartWeb.Models.Catalog;
using ShoppingCartWeb.Models.Order;
namespace ShoppingCartWeb.Services;

public interface IBasketService
{
    [Get("/basket-service/basket/{userName}")]
    Task<GetBasketResponse> GetBasket(string userName);

    [Post("/basket-service/basket/checkout")]
    Task<CheckOutBasketResponse> BasketCheckOut(CheckOutBasketRequest request);

    [Post("/basket-service/basket")]
    Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);

    [Delete("/basket-service/basket/{userName}")]
    Task<DeleteBasketResponse> DeleteBasket(String UserName);

    public async Task<ShoppingCartModel> GetUserBaskets()
    {
        var response = await GetBasket("SairajPanda");
        return response._shoppingCart;
    }
}
