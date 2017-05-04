namespace IdentityServerMvcSample.IdentityServer.Web
{
    using System.Collections.Generic;
    using System.Linq;

    using IdentityServer3.Core.Models;

    public static class Scopes
    {
        public static List<Scope> Get()
        {
            return StandardScopes.All.Union(
                new[] { new Scope { Name = "api1" } }).ToList();
        }
    }
}