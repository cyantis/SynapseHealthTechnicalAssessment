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
        var alertData = new
        {
            Message = $"{Constants.Messages.DeliveredItemAlert} Order {orderId}, Item: {item.Description}, Delivery Notifications: {item.DeliveryNotification}"
        };
        var content = new StringContent(JObject.FromObject(alertData).ToString(), System.Text.Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync(Constants.ApiUrls.AlertApiUrl, content);
            response.EnsureSuccessStatusCode();

            Log.Information($"{Constants.Messages.DeliveredItemAlertSent} {item.Description}");
        }
        catch (HttpRequestException ex)
        {
            Log.Error($"{Constants.Messages.DeliveredItemAlertFailed} {item.Description} {ex.Message}");
        }
    }
}