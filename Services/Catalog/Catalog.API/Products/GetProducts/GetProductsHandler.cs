namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);
internal class GetProductsQueryHandler  (ILogger<GetProductsQueryHandler> logger, CatalogDBContext _repository, IMapper mapper) 
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetProductsQuery {@Query}", query);

        var products =await _repository.Products.AsNoTracking().ToListAsync(cancellationToken);

        return new GetProductsResult(products);
    }
}

