using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(IdentityServerMvcSample.IdentityServer.Web.Startup))]

namespace IdentityServerMvcSample.IdentityServer.Web
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Security.Cryptography.X509Certificates;

    using IdentityServer3.Core.Configuration;

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
                SiteName = "Example IdentityServer",
                SigningCertificate = LoadCertificate(),

                Factory = new IdentityServerServiceFactory()
                    .UseInMemoryClients(Clients.Get())
                    .UseInMemoryScopes(Scopes.Get())
                    .UseInMemoryUsers(Users.Get()),

                RequireSsl = false
            };

            app.UseIdentityServer(options);
        }

        private static X509Certificate2 LoadCertificate()
        {
            var resource = GetResource(
                typeof(Startup).Assembly, 
                "IdentityServerMvcSample.IdentityServer.Web.idsrv3test.pfx");

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\idsrv3test.pfx");
            return new X509Certificate2(path, "idsrv3test");
        }

        private static byte[] GetResource(Assembly assembly, string resourceName)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    return null;
                }

                var result = new byte[stream.Length];
                stream.Read(result, 0, result.Length);
                return result;
            }
        }
    }
}
