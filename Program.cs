using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Serilog;

namespace Synapse.OrdersExample
{
    /// <summary>
    /// I Get a list of orders from the API
    /// I check if the order is in a delviered state, If yes then send a delivery alert and add one to deliveryNotification
    /// I then update the order.   
    /// </summary>
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            Log.Logger = LoggerSetup.ConfigureLogger();

            Log.Information("Start of App");

            var orderService = new OrdersService(new OrdersApiClient(), new AlertsApiClient());
            await orderService.ProcessOrdersAsync();

            Log.Information("Results sent to relevant APIs.");

            Log.CloseAndFlush();

            return 0;
        }
    }
}