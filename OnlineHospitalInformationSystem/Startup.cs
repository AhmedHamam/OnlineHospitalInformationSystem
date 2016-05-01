using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineHospitalInformationSystem.Startup))]
namespace OnlineHospitalInformationSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
