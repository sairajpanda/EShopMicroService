using Microsoft.EntityFrameworkCore;

namespace Catalog.API.DBContext
{
    public class CatalogDBContext : DbContext
    {

        public CatalogDBContext(DbContextOptions<CatalogDBContext> options) : base(options)
        {
        }
        public DbSet<Models.Product> Products { get; set; }
    }
}
