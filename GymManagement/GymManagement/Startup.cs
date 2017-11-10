using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GymManagement.Startup))]
namespace GymManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
