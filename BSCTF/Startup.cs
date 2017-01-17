using Microsoft.Owin;

[assembly: OwinStartup(typeof(BSCTF.Startup))]

namespace BSCTF
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
