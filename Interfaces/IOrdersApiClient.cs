public interface IOrdersApiClient
{
    // Async fetch to get an array of orders
    Task<IEnumerable<Order>> FetchOrdersAsync();

    // Async fetch to update an order
    Task UpdateOrderAsync(Order order);
}