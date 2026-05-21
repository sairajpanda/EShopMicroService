using Azure.Core;
using Basket.API.Data;
using Discount.Grpc;
using Grpc.Core;

namespace Basket.API.Basket.GetBaskets;

public record StoreBasketCommnad (string UserName,ICollection<ShoppingCartItem> Items,decimal TotalItemPrice) 
    : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommnad>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotNull().WithMessage("ShoppingCart UserName is required.");
    }
}

public class StoreBasketCommnadHandler(IBasketRepository _BasketRepository, DiscountService.DiscountServiceClient _discount) 
    : ICommandHandler<StoreBasketCommnad, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommnad request, CancellationToken cancellationToken)
    {
        StoreBasketCommnad _request=  await DeductDiscount(request, cancellationToken);
        ShoppingCart _objShoppingCart = new ShoppingCart();
        if (_request is not null)
        {
            _objShoppingCart.UserName = _request.UserName;
            _objShoppingCart.ShoppingCartId = Guid.NewGuid();
            _objShoppingCart.Items = _request.Items;
            _objShoppingCart.TotalItemPrice = _request.Items.Sum(x => x.Price * x.Quantity);
        }
        else
        {
            _objShoppingCart.UserName = request.UserName;
            _objShoppingCart.ShoppingCartId = Guid.NewGuid();
            _objShoppingCart.Items = request.Items;
            _objShoppingCart.TotalItemPrice = request.Items.Sum(x => x.Price * x.Quantity);
        }
        await _BasketRepository.StoreBasket(_objShoppingCart, cancellationToken);

        return new StoreBasketResult(request.UserName);
    }

    public async Task<StoreBasketCommnad> DeductDiscount(StoreBasketCommnad shoppingCart, CancellationToken cancellationToken)
    {
        foreach (var item in shoppingCart.Items)
        {
            GetDiscountRequest GetDiscountRequestObj = new GetDiscountRequest();
            GetDiscountRequestObj.ProductName = item.ProductName;
            var coupon = await _discount.GetDiscountAsync(GetDiscountRequestObj, cancellationToken: cancellationToken);
            item.Price -= (int)coupon.Amount;
        }
        return shoppingCart;
    }
}
