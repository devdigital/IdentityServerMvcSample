namespace IdentityServerMvcSample.IdentityServer.Web
{
    using System.Collections.Generic;

    using IdentityServer3.Core.Models;

    public static class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    Enabled = true,
                    ClientName = "MVC Client",
                    ClientId = "mvc",
                    Flow = Flows.Implicit,

                    RedirectUris = new List<string>
                    {
                        "http://localhost:53036/"
                    },

                    AllowAccessToAllScopes = true
                }
            };
        }
    }
}