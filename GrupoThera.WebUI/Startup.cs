using Microsoft.Owin;
using Owin;
using System.Web.Services.Description;

[assembly: OwinStartup(typeof(GrupoThera.WebUI.Startup))]
namespace GrupoThera.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }

        
    }
}
