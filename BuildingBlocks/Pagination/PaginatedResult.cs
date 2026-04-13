using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Pagination;

public class PaginatedResult<TEntity>
    (int pageIndex, int PageSize, long Count, IEnumerable<TEntity> Data) where TEntity : class
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = PageSize;
    public long Count { get; } = Count;
    public IEnumerable<TEntity> Data { get; } = Data;
}
