using BlazorWebAssemblySignalRApp.Server.Controllers;
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
        private static IHubContext<Hubs.SignalRHub> _hubContext;
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            _hubContext = (IHubContext<Hubs.SignalRHub>)host.Services.GetService(typeof(IHubContext<Hubs.SignalRHub>));
            WebsocketsController wsc = new WebsocketsController(_hubContext);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
