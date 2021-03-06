﻿using ChatBox_SignalR.Models;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBox_SignalR
{
    public class ChatHub : Hub
    {
        static List<User> listUser = new List<User>();
        public void SendMsg(string msg)
        {
            var id = Context.ConnectionId;
            if (listUser.Count(x => x.ConnectionId == id) == 0)
            {
                listUser.Add(new User { ConnectionId = id });
            }
            var name = Context.Request.User.Identity.Name;
            //gui tin nhan cho admin
            Clients.User("admin@gmail.com").Send(id, msg, listUser);
            Clients.All.SendAll(id, msg);
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            if (stopCalled == true)
            {
                var idConnec = Context.ConnectionId;
                var check = listUser.FirstOrDefault(x => x.ConnectionId == idConnec);
                if (check != null)
                {
                    listUser.Remove(check);
                    Clients.User("admin@gmail.com").OnUserDisConnected(idConnec, check.ConnectionId);
                }
            }
            return base.OnDisconnected(stopCalled);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id cua ket noi gui tin nhan den</param>
        /// <param name="msg"></param>
        public void SendMsgPrivate(string id, string msg)
        {
            //id cua ket noi hien hanh
            var idReturn = Context.ConnectionId;
            Clients.Client(id).SendPrivate(id, msg, idReturn);
        }
    }
}