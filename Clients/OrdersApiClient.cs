using Newtonsoft.Json.Linq;
using Serilog;

public class OrdersApiClient : IOrdersApiClient
{
    private readonly HttpClient _httpClient;

    public OrdersApiClient()
    {
        _httpClient = new HttpClient();
    }

    public async Task<IEnumerable<Order>> FetchOrdersAsync()
    {
        Log.Information($"{Constants.Messages.FetchingOrders}");

        try
        {
            var response = await _httpClient.GetAsync(Constants.ApiUrls.OrdersApiUrl);
            var ordersData = await response.Content.ReadAsStringAsync();

            return JArray.Parse(ordersData)?.ToObject<List<Order>>() ?? new List<Order>();
        }
        catch (HttpRequestException ex)
        {
            Log.Error($"{Constants.Messages.FetchOrdersFailed} {ex.Message}");
            return [];
        }
    }

    public async Task UpdateOrderAsync(Order order)
    {
        Log.Information($"{Constants.Messages.UpdatingOrder}");
        var content = new StringContent(Newtonsoft.Json.Linq.JObject.FromObject(order).ToString(), System.Text.Encoding.UTF8, "application/json");
        try
        {
            var response = await _httpClient.PostAsync(Constants.ApiUrls.UpdateApiUrl, content);
            response.EnsureSuccessStatusCode();
            Log.Information($"{Constants.Messages.UpdatedOrderAlertSent} {order.OrderId}");
        }
        catch (HttpRequestException ex)
        {
            Log.Error($"{Constants.Messages.UpdatedOrderAlertFailed} {order.OrderId} {ex.Message}");
        }
    }
}