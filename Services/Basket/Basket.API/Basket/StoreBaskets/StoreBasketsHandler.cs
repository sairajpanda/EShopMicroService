using Basket.API.Data;

namespace Basket.API.Basket.GetBaskets;

public record StoreBasketCommnad (
 string UserName,
 ICollection<ShoppingCartItem> Items,
 decimal TotalItemPrice) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommnad>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotNull().WithMessage("ShoppingCart UserName is required.");
    }
}

public class StoreBasketCommnadHandler(IBasketRepository _BasketRepository) : ICommandHandler<StoreBasketCommnad, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommnad request, CancellationToken cancellationToken)
    {
        ShoppingCart _objShoppingCart = new ShoppingCart();
        _objShoppingCart.UserName = request.UserName;
        _objShoppingCart.Items = request.Items; 
        _objShoppingCart.TotalItemPrice = request.TotalItemPrice;

        await _BasketRepository.StoreBasket(_objShoppingCart, cancellationToken);

        return new StoreBasketResult(request.UserName);
    }
}
