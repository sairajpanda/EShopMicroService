namespace Basket.API.Basket.GetBaskets;

public record DeleteBasketsCommand(string Username) : ICommand<DeleteBasketResults>;

public record DeleteBasketResults(bool IsSuccess);


public class DeleteBasketsCommandValidator : AbstractValidator<DeleteBasketsCommand>
{
    public DeleteBasketsCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
    }
}

public class DeleteBasketsCommandHandler : ICommandHandler<DeleteBasketsCommand, DeleteBasketResults>
{
    public async Task<DeleteBasketResults> Handle(DeleteBasketsCommand request, CancellationToken cancellationToken)
    {
        return new DeleteBasketResults(true);
    }
}
