using Catalog.API.Models;

namespace Catalog.API.Products.GetProductsByID;


public record GetProdcutsByIDQuery(Guid id) : IQuery<GetProdcutsByIDResult>;

public record GetProdcutsByIDResult(Product _products);
internal class GetProductsByIDQueryHandler (CatalogDBContext _repository)
    : IQueryHandler<GetProdcutsByIDQuery, GetProdcutsByIDResult>
{
    public async Task<GetProdcutsByIDResult> Handle(GetProdcutsByIDQuery query, CancellationToken cancellationToken)
    {
        var products = await _repository.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == query.id, cancellationToken);

        return new GetProdcutsByIDResult(products);

    }
}
