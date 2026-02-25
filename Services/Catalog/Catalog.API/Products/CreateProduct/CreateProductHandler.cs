
public record CreateProductCommand(string Name, string Description,decimal Price,List<string> Category,string ImageFile) 
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required.");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required.");
    }
}

internal class CreateProductCommandHandler  
    (CatalogDBContext _repository)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    /*private readonly CatalogDBContext _repository;
    public CreateProductCommandHandler(CatalogDBContext repository)
    {
        _repository = repository;
    }*/
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        /*var validationResult = 
            await _validator.ValidateAsync(command, cancellationToken);
        var errors = 
            validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        if (errors.Any())
        {
            throw new ValidationException(errors.FirstOrDefault());
        }*/

        var objproduct = new Product
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            Category = command.Category,
            ImageFile = command.ImageFile
        };

        _repository.Products.Add(objproduct);
        await _repository.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(objproduct.Id);
    }
}
