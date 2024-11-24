public class OrderService
{
    private readonly IOrdersApiClient _ordersApiClient;
    private readonly IAlertsApiClient _alertsApiClient;

    public OrderService(IOrdersApiClient ordersApiClient, IAlertsApiClient alertsApiClient)
    {
        _ordersApiClient = ordersApiClient;
        _alertsApiClient = alertsApiClient;
    }

    public async Task ProcessOrdersAsync()
    {
        var orders = await _ordersApiClient.FetchOrdersAsync();
        foreach (var order in orders)
        {
            foreach (var item in order.Items)
            {
                if (item.IsDelivered)
                {
                    await _alertsApiClient.SendAlertAsync(item, order.OrderId);
                    item.IncrementDeliveryNotification();
                }
            }
            await _ordersApiClient.UpdateOrderAsync(order);
        }
    }
}