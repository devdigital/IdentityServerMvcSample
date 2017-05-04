namespace IdentityServerMvcSample.IdentityServer.Web
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using IdentityServer3.Core;
    using IdentityServer3.Core.Services.InMemory;

    public static class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Username = "admin",
                    Password = "admin",
                    Subject = "1",

                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Admin"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Admin")
                    }
                }
            };
        }
    }
}