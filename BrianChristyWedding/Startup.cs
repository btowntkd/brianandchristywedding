using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BrianChristyWedding.Startup))]
namespace BrianChristyWedding
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
