public class OrdersService
{
    private readonly IOrdersApiClient _ordersApiClient;
    private readonly IAlertsApiClient _alertsApiClient;

    public OrdersService(IOrdersApiClient ordersApiClient, IAlertsApiClient alertsApiClient)
    {
        _ordersApiClient = ordersApiClient;
        _alertsApiClient = alertsApiClient;
    }

    public async Task ProcessOrdersAsync()
    {
        var orders = await _ordersApiClient.FetchOrdersAsync();

        foreach (var (item, order) in orders.SelectMany(order => order.Items, (order, item) => (item, order)))
        {
            if (item.Status == Constants.Statuses.Delivered)
            {
                await _alertsApiClient.SendAlertAsync(item, order.OrderId);
                item.IncrementDeliveryNotification();
            }
            await _ordersApiClient.UpdateOrderAsync(order);
        }
    }
}
