
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Catalog.API.Products.GetProductsByCategory;

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductsByCategoryQueryHandler(CatalogDBContext _repository) : IQueryHandler<GetProductsByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var products = await _repository.Products.AsNoTracking().Where (p => p.Category.Contains(request.Category)).ToListAsync(cancellationToken); 

        return new GetProductByCategoryResult(products);
    }
}
