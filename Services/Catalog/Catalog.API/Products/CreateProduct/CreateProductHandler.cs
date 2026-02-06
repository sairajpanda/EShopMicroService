namespace Catalog.API.Products.CreateProduct;
using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Catalog.API.DBContext;


public record CreateProductCommand(string Name, string Description,decimal Price,List<string> Category,string ImageFile) 
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);



internal class CreateProductCommandHandler  : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    private readonly CatalogDBContext _repository;
    public CreateProductCommandHandler(CatalogDBContext repository)
    {
        _repository = repository;
    }
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

        _repository.Products.Add(objproduct);
        await _repository.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(objproduct.Id);
    }
}
