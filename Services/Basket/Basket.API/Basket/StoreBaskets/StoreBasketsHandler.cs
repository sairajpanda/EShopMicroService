namespace Basket.API.Basket.GetBaskets;

public record StoreBasketCommnad (ShoppingCart _ShoppingCart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommnad>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x._ShoppingCart).NotNull().WithMessage("ShoppingCart is required.");
    }
}

public class StoreBasketCommnadHandler : ICommandHandler<StoreBasketCommnad, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommnad request, CancellationToken cancellationToken)
    {
        ShoppingCart _cart = request._ShoppingCart;
        return new StoreBasketResult("Sai");
    }
}
