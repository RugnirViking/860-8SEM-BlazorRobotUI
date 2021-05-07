using Fleck;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            if (splitMsg[0] == "imgs")
            {
                await _hubContext.Clients.All.SendAsync("ReceiveImage", "1", splitMsg[1]);
                await _hubContext.Clients.All.SendAsync("ReceiveImage", "2", splitMsg[1]);
            }
            else if (splitMsg[0] == "img1")
            {
                await _hubContext.Clients.All.SendAsync("ReceiveImage", "1", splitMsg[1]);
            }
            else if (splitMsg[0] == "img2")
            {
                await _hubContext.Clients.All.SendAsync("ReceiveImage", "2", splitMsg[1]);
            }
            else if (splitMsg[0] == "status")
            {
                await _hubContext.Clients.All.SendAsync("ReceiveStatus", splitMsg[1]);
            }
            else if (splitMsg[0] == "modStatus")
            {
                int mod=0;
                int col=0;
                bool valid = Int32.TryParse(splitMsg[1], out mod) && Int32.TryParse(splitMsg[2], out col);
                if (valid)
                {
                    await _hubContext.Clients.All.SendAsync("ReceiveStatus", mod, col);
                }
            }
            else
            {
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", "python-user", splitMsg[0], splitMsg[1]);
            }
        }
        private Image GetImage(string s)
        {

            Image image;
            byte[] bytes = Convert.FromBase64String(s);

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                try
                {
                    image = Image.FromStream(ms);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                image = Image.FromStream(ms);
            }
            return image;
        }
    }
}
