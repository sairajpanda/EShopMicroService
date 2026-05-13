using Refit;
using ShoppingCartWeb.Models.Catalog;
using ShoppingCartWeb.Models.Order;

namespace ShoppingCartWeb.Services;

public interface ICatalogService
{
    [Get("/catalog-service/products")]
    Task<GetProductsResponse> GetProducts();

    [Get("/catalog-service/products/category/{category}")]
    Task<GetProductByCategoryResponse> GetProductsByCategory(string category);

    [Get("/catalog-service/products/{id}")]
    Task<GetProductsByIdResponse> GetProduct(Guid id);
}
