namespace Catalog.API.Products.CreateProduct;
using Catalog.API.Models;
using BuildingBlocks.CQRS;

public record CreateProductCommand(string Name, string Description,decimal Price,List<string> Category,string ImageFile) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler  : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    //private readonly IProductRepository _repository;
    //public CreateProductCommandHandler(IProductRepository repository)
    //{
    //    _repository = repository;
    //}
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {

        var objproduct = new Product
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            Category = command.Category,
            ImageFile = command.ImageFile
        };

        //await _repository.AddAsync(objproduct, cancellationToken);
        return new CreateProductResult(objproduct.Id);
    }
}
