using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fleck;
using Microsoft.AspNetCore.SignalR;

namespace BlazorWebAssemblySignalRApp.Server.Hubs
{
    public class ChatHub : Hub
    {
        public ChatHub()
        {
        }


        //private async Action<Task<string>> onMessageRecievedAsync(string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", "python-user", message);
        //    return new Action();
        //}

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user,"usr", message);
        }
    }
}