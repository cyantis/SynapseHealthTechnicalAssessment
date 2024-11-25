using Synapse.OrdersExample;
using Moq;

public class OrdersServiceTests
{
    private readonly Mock<IOrdersApiClient> _mockOrdersApiClient;
    private readonly Mock<IAlertsApiClient> _mockAlertsApiClient;
    private readonly OrdersService _ordersService;

    public OrdersServiceTests()
    {
        _mockOrdersApiClient = new Mock<IOrdersApiClient>();
        _mockAlertsApiClient = new Mock<IAlertsApiClient>();
        _ordersService = new OrdersService(_mockOrdersApiClient.Object, _mockAlertsApiClient.Object);
    }
    [Test]
    public async Task ProcessOrdersAsync_SendsAlert_WhenItemIsDelivered()
    {
        var order = new Order
        {
            OrderId = "123",
            Items = new List<Item>
            {
                new Item { Status = Constants.Statuses.Delivered, Description = "Item 1" },
                new Item { Status = Constants.Statuses.Pending, Description = "Item 1" }
            }
        };

        _mockOrdersApiClient.Setup(client => client.FetchOrdersAsync())
            .ReturnsAsync(new List<Order> { order });

        await _ordersService.ProcessOrdersAsync();

        _mockAlertsApiClient.Verify(client => client.SendAlertAsync(It.IsAny<Item>(), It.IsAny<string>()), Times.Once);
    }
}
