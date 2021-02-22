using Fleck;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWebAssemblySignalRApp.Server
{
    public class Program
    {
        private static IHubContext<Hubs.ChatHub> _hubContext;
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            _hubContext = (IHubContext<Hubs.ChatHub>)host.Services.GetService(typeof(IHubContext<Hubs.ChatHub>));
            var server = new WebSocketServer("ws://0.0.0.0:8765");
            server.Start(socket =>
            {
                socket.OnOpen = () => Console.WriteLine("Open!");
                socket.OnClose = () => Console.WriteLine("Close!");
                socket.OnMessage = onMessageRecievedAsync;

            });
            host.Run();
        }

        private static async void onMessageRecievedAsync(string message)
        {
            var splitMsg = message.Split("||");
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "python-user", splitMsg[0], splitMsg[1]);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
