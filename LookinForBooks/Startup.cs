using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LookinForBooks.Startup))]
namespace LookinForBooks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
