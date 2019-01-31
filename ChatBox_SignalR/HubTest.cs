using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Threading.Tasks;

namespace ChatBox_SignalR
{
    public class HubTest : IHub, IDisposable
    {
        public HubCallerContext Context { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IHubCallerConnectionContext<dynamic> Clients { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IGroupManager Groups { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task OnConnected()
        {
            throw new NotImplementedException();
        }

        public virtual Task OnDisconnected(bool stopCalled)
        {
            throw new NotImplementedException();
        }

        public Task OnReconnected()
        {
            throw new NotImplementedException();
        }
    }
}