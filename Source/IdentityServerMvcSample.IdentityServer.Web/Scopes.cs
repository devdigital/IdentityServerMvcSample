namespace IdentityServerMvcSample.IdentityServer.Web
{
    using System.Collections.Generic;

    using IdentityServer3.Core.Models;

    public static class Scopes
    {
        public static List<Scope> Get()
        {
            return new List<Scope> { new Scope { Name = "api1" } };
        }
    }
}