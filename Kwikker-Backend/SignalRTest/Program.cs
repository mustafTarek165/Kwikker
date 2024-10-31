using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRTest
{
    public class Program
    {
        static async Task  Main(string[] args)
        {
            // Connect to the SignalR hub from the ASP.NET API
            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7246/notificationHub")  // Use your actual API URL
                .Build();

            // Register the method to handle incoming notifications
            connection.On<string>("ReceiveNotification", (message) =>
            {
                Console.WriteLine($"Notification received: {message}");
            });

            // Start the connection to the SignalR hub
           await  connection.StartAsync();
            Console.WriteLine("Connected to SignalR Hub");

            // Keep the console app running to receive notifications
            Console.WriteLine("dmdfmfm");
        }
    }
}
