public class Order
{
    public required string OrderId { get; set; }
    public required List<Item> Items { get; set; }
}

public class Item
{
    public required string Description { get; set; }
    public required string Status { get; set; }
    public int DeliveryNotification { get; set; }

    public bool IsDelivered => Status.Equals(Constants.Statuses.Delivered, StringComparison.OrdinalIgnoreCase);

    public void IncrementDeliveryNotification()
    {
        DeliveryNotification++;
    }
}