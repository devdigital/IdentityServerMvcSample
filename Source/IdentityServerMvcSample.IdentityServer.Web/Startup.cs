using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(IdentityServerMvcSample.IdentityServer.Web.Startup))]

namespace IdentityServerMvcSample.IdentityServer.Web
{
    using System.Collections.Generic;

    using IdentityServer3.Core.Configuration;
    using IdentityServer3.Core.Services.InMemory;

    using Serilog;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .LiterateConsole(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({Name:l}){NewLine} {Message}{NewLine}{Exception}")
                .CreateLogger();

            var options = new IdentityServerOptions
            {
                Factory = new IdentityServerServiceFactory()
                    .UseInMemoryClients(Clients.Get())
                    .UseInMemoryScopes(Scopes.Get())
                    .UseInMemoryUsers(new List<InMemoryUser>()),

                RequireSsl = false
            };

            app.UseIdentityServer(options);
        }
    }
}
