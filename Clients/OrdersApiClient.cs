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
        Log.Information("Fetching orders from API...");

        try
        {
            var response = await _httpClient.GetAsync("https://api.example.com/orders");
            var ordersData = await response.Content.ReadAsStringAsync();

            return JArray.Parse(ordersData)?.ToObject<List<Order>>() ?? new List<Order>();
        }
        catch (HttpRequestException ex)
        {
            Log.Error($"Failed to fetch orders from API: {ex.Message}");
            return [];  // Return an empty list if fetch fails
        }
    }

    public async Task UpdateOrderAsync(Order order)
    {
        Log.Information("Updating order...");
        string updateApiUrl = "https://update-api.com/update";
        var content = new StringContent(Newtonsoft.Json.Linq.JObject.FromObject(order).ToString(), System.Text.Encoding.UTF8, "application/json");
        try
        {
            var response = await _httpClient.PostAsync(updateApiUrl, content);
            response.EnsureSuccessStatusCode();
            Log.Information($"Updated order sent for processing: OrderId {order.OrderId}");
        }
        catch (HttpRequestException ex)
        {
            Log.Error($"Failed to send updated order for processing: OrderId {order.OrderId} {ex.Message}");
        }
    }
}