using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fleck;
using Microsoft.AspNetCore.SignalR;
using System.Drawing;

namespace BlazorWebAssemblySignalRApp.Server.Hubs
{
    public class SignalRHub : Hub
    {
        public SignalRHub()
        {
        }


        //private async Action<Task<string>> onMessageRecievedAsync(string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", "python-user", message);
        //    return new Action();
        //}

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, user, message);
        }
        public async Task SendImage(string imagenum, Image img)
        {
            await Clients.All.SendAsync("ReceiveImage", imagenum, "usr", img);
        }
        public async Task SendStatus(string status)
        {
            await Clients.All.SendAsync("ReceiveStatus", status);
        }
        public async Task SendModStatus(int module, int status)
        {
            await Clients.All.SendAsync("ReceiveModStatus", module,status);
        }
    }
}