using Basket.API.Data;
using Basket.API.Dto;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using MassTransit.Transports;
namespace Basket.API.Basket.CheckoutBasket;

public record CheckOutBasketCommand(BasketCheckoutDto basketCheckoutDto) : ICommand<CheckOutBasketResult>;

public record CheckOutBasketResult(bool IsSuccess);

public record TestMessage(string Text);

public class CheckOutBasketCommandValidator : AbstractValidator<CheckOutBasketCommand>
{
    public CheckOutBasketCommandValidator()
    {
        RuleFor(x => x.basketCheckoutDto).NotNull().WithMessage("basketCheckoutDto cann't be null");
        RuleFor(x => x.basketCheckoutDto.UserName).NotEmpty().WithMessage("UserName cann't be null");
    }
}


public class CheckOutBasketCommandHandler (IBasketRepository repository,IPublishEndpoint publishEndpoint, IMapper mapper)
    : ICommandHandler<CheckOutBasketCommand, CheckOutBasketResult>
{
    public async Task<CheckOutBasketResult> Handle(CheckOutBasketCommand command, CancellationToken cancellationToken)
    {
        await publishEndpoint.Publish(new TestMessage("Hello"));
        return new CheckOutBasketResult(true);

        /*  try
          {
              var basket = await repository.GetBasket(command.basketCheckoutDto.UserName, cancellationToken);

              if (basket == null)
              {
                  return new CheckOutBasketResult(false);
              }

              command.basketCheckoutDto.TotalPrice = basket.TotalItemPrice;
              await publishEndpoint.Publish(command.basketCheckoutDto, cancellationToken);
              await repository.DeleteBasket(command.basketCheckoutDto.UserName, cancellationToken);
              return new CheckOutBasketResult(true);
          }
          catch (Exception ex)
          {
              throw ex;
          }*/
    }
}
