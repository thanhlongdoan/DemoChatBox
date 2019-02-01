using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatBox_SignalR.Models
{
    public class User
    {
        public string ConnectionId { get; set; }
        public string Msg { get; set; }
    }
}