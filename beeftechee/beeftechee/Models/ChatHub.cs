using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace beeftechee.Models
{
    public class ChatHub : Hub
    {
        public void Send(string name,string message,string image)
        {
            var username = Context.User.Identity.Name;
            Clients.All.addNewMessageToPage(name, message,image);
        }
    }
}