using ChatBox_SignalR.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ChatBox_SignalR
{
    public class ChatHub : Hub
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        static List<User> listUser = new List<User>();
        public void SendMsg(string msg)
        {
            var id = Context.ConnectionId;
            List<string> list = new List<string>();
            list.Add(msg);
            if (listUser.Count == 0)
            {
                listUser.Add(
                    new User
                    {
                        ConnectionId = id,
                        Msg = new List<string>() { msg }
                    }
                    );
            }
            else
            {
                bool check = false;
                for (int i = 0; i < listUser.Count; i++)
                {
                    if (listUser[i].ConnectionId == id)
                    {
                        check = true;
                        listUser[i].Msg.Add(msg);
                    }
                }
                if (check == false)
                {
                    listUser.Add(new User { ConnectionId = id, Msg = new List<string>() { msg } });
                }
            }

            var name = Context.Request.User.Identity.Name;
            //gui tin nhan cho admin
            string listUserJson = new JavaScriptSerializer().Serialize(listUser);
            Clients.User("admin@gmail.com").Send(id, msg, listUserJson);
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
            Clients.Clients(new List<string> { id, idReturn }).SendPrivate(id, msg, idReturn);
        }
    }
}