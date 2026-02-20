
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Products.UpdateProducts;

public record UpdateProductCommand(Guid Id,string Name, string Description, decimal Price, List<string> Category, string ImageFile): ICommand<UpdateProductResults>;

public record UpdateProductResults(bool success);

internal class UpdateProductsCommandHandlers(CatalogDBContext _repository)
    : ICommandHandler<UpdateProductCommand, UpdateProductResults>
{
    public async  Task<UpdateProductResults> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null) return new UpdateProductResults(false);

        product.Description = request.Description;
        product.Category= request.Category;
        product.Price= request.Price;
        product.ImageFile = request.ImageFile;
        product.Name = request.Name;

        await _repository.SaveChangesAsync(cancellationToken);
        return new UpdateProductResults(true);
    }
}
