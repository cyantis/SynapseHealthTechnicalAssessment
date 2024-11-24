using Newtonsoft.Json.Linq;

public class OrdersApiClient : IOrdersApiClient
{
    private readonly HttpClient _httpClient;

    public OrdersApiClient()
    {
        _httpClient = new HttpClient();
    }

    public async Task<IEnumerable<Order>> FetchOrdersAsync()
    {
        var response = await _httpClient.GetAsync("https://api.example.com/orders");

        if (response.IsSuccessStatusCode)
        {
            var ordersData = await response.Content.ReadAsStringAsync();

            // Use null-conditional operator (?.) to ensure no null reference exception
            return JArray.Parse(ordersData)?.ToObject<List<Order>>() ?? new List<Order>();
        }
        else
        {
            // Handle error (log, throw, etc.)
            return [];  // Return an empty list if fetch fails
        }
    }

    public async Task UpdateOrderAsync(Order order)
    {
        string updateApiUrl = "https://update-api.com/update";
        var content = new StringContent(Newtonsoft.Json.Linq.JObject.FromObject(order).ToString(), System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(updateApiUrl, content);
        response.EnsureSuccessStatusCode();
    }
}