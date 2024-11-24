using Newtonsoft.Json.Linq;
using Serilog;

public class AlertsApiClient : IAlertsApiClient
{
    private readonly HttpClient _httpClient;

    public AlertsApiClient()
    {
        _httpClient = new HttpClient();
    }

    public async Task SendAlertAsync(Item item, string orderId)
    {
        string alertApiUrl = "https://alert-api.com/alerts";
        var alertData = new
        {
            Message = $"Alert for delivered item: Order {orderId}, Item: {item.Description}, Delivery Notifications: {item.DeliveryNotification}"
        };
        var content = new StringContent(JObject.FromObject(alertData).ToString(), System.Text.Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync(alertApiUrl, content);
            response.EnsureSuccessStatusCode();  // This throws an exception if the status code is not 2xx

            Log.Information($"Alert sent for delivered item: {item.Description}");
        }
        catch (HttpRequestException ex)
        {
            Log.Error($"Failed to send alert for delivered item: {item.Description} {ex.Message}");
        }
    }
}