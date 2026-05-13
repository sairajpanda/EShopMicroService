namespace ShoppingCartWeb.Models.Order;

public class PaginatedResult<TEntity>
    (int pageIndex, int PageSize, long Count, IEnumerable<TEntity> Data) where TEntity : class
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = PageSize;
    public long Count { get; } = Count;
    public IEnumerable<TEntity> Data { get; } = Data;
}
