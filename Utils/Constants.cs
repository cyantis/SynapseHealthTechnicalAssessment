public static class Constants
{
    public static class ApiUrls
    {
        public const string OrdersApiUrl = "https://orders-api.com/orders";
        public const string AlertApiUrl = "https://alert-api.com/alerts";
        public const string UpdateApiUrl = "https://update-api.com/update";
    }

    public static class Messages
    {
        public const string FetchingOrders = "Fetching orders from API...";
        public const string FetchOrdersFailed = "Failed to fetch orders from API:";
        public const string UpdatingOrder = "Updating order...";
        public const string DeliveredItemAlert = "Alert for delivered item:";
        public const string DeliveredItemAlertSent = "Alert sent for delivered item:";
        public const string DeliveredItemAlertFailed = "Failed to send alert for delivered item:";
        public const string UpdatedOrderAlertSent = "Updated order sent for processing:";
        public const string UpdatedOrderAlertFailed = "";
    }

    public static class Statuses
    {
        public const string Delivered = "Delivered";
    }
}