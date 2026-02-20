using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Products.UpdateProducts;

public record DeleteProductCommand (Guid Id) : ICommand<DeleteProductResults>;

public record DeleteProductResults(bool success);

public class DeleteProductHandlers(CatalogDBContext _repository) : ICommandHandler<DeleteProductCommand, DeleteProductResults>
{
    public async Task<DeleteProductResults> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product != null) 
        _repository.Products.Remove(product);

        await _repository.SaveChangesAsync(cancellationToken);

        return new DeleteProductResults(true);
    }
}
