using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChatBox_SignalR.Startup))]
namespace ChatBox_SignalR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
