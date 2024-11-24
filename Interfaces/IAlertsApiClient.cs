public interface IAlertsApiClient
{
    // Sends an async alert
    Task SendAlertAsync(string message);
}