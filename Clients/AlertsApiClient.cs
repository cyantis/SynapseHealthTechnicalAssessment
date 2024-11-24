using Newtonsoft.Json.Linq;

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
        var response = await _httpClient.PostAsync(alertApiUrl, content);
        response.EnsureSuccessStatusCode();
    }
}