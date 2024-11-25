using Moq;

public class OrdersServiceMockApis
{
    public async Task OrdersServiceMocks()
    {
        var ordersApiClientMock = new Mock<IOrdersApiClient>();
        ordersApiClientMock
            .Setup(client => client.FetchOrdersAsync())
            .ReturnsAsync(new List<Order>
            {
                new Order
                {
                    OrderId = "123",
                    Items = new List<Item>
                    {
                        new Item { Description = "Item1", Status = Constants.Statuses.Delivered },
                        new Item { Description = "Item2", Status = Constants.Statuses.Pending }
                    }
                }
            });

        ordersApiClientMock
            .Setup(client => client.UpdateOrderAsync(It.IsAny<Order>()))
            .Returns(Task.CompletedTask);

        var alertsApiClientMock = new Mock<IAlertsApiClient>();
        alertsApiClientMock
            .Setup(client => client.SendAlertAsync(It.IsAny<Item>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        var ordersService = new OrdersService(ordersApiClientMock.Object, alertsApiClientMock.Object);
        await ordersService.ProcessOrdersAsync();
    }
}
