using Fleck;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWebAssemblySignalRApp.Server.Controllers
{
    public class WebsocketsController
    {
        private IHubContext<Hubs.SignalRHub> _hubContext;
        public WebsocketsController(IHubContext<Hubs.SignalRHub> _hubContext)
        {
            this._hubContext = _hubContext;
            var server = new WebSocketServer("ws://0.0.0.0:8765");
            server.Start(socket =>
            {
                socket.OnOpen = () => Console.WriteLine("Open!");
                socket.OnClose = () => Console.WriteLine("Close!");
                socket.OnMessage = onMessageRecievedAsync;

            });
        }

        private async void onMessageRecievedAsync(string message)
        {
            var splitMsg = message.Split("||");
            if (splitMsg[0]=="imgs")
            {
                await _hubContext.Clients.All.SendAsync("RecieveImage", "python-user", splitMsg[0], splitMsg[1]);
            }
            else
            {
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", "python-user", splitMsg[0], splitMsg[1]);
            }
        }
    }
}
