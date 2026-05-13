using Refit;
using ShoppingCartWeb.Models.Order;

namespace ShoppingCartWeb.Services;

public interface IOrderService
{
    [Get("/ordering-service/orders?pageIndex={pageIndex}&pageSize={pageSize}\"")]
    Task<GetOrdersResponse> GetOrders(int? pageIndex =1, int? pageSize = 10);

    [Get("/ordering-service/orders/{orderName}")]
    Task<GetProductsByNameResponse> GetOrderByName(string orderName);

    [Get("/ordering-service/orders/customer/{customerId}")]
    Task<GetOrdersCustomerResponse> GetOrdersByCustomer(Guid customerId);
}
