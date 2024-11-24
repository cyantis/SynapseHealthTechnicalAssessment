public interface IAlertsApiClient
{
    // Sends an async alert
    Task SendAlertAsync(Item item, string orderId);
}