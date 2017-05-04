using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(IdentityServerMvcSample.Mvc.Startup))]

namespace IdentityServerMvcSample.Mvc
{
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web.Helpers;

    using Microsoft.IdentityModel.Protocols;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.Notifications;
    using Microsoft.Owin.Security.OpenIdConnect;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = "sub";
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions { AuthenticationType = "Cookies" });

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    Authority = "http://localhost:54281/",
                    ClientId = "mvc",
                    RedirectUri = "http://localhost:53036/",
                    ResponseType = "id_token",
                    SignInAsAuthenticationType = "Cookies",
                    UseTokenLifetime = false,
                    Notifications = new OpenIdConnectAuthenticationNotifications
                    {
                        SecurityTokenValidated = n => this.ClaimsTransformation(n)
                    }
                });
        }

        private Task ClaimsTransformation(SecurityTokenValidatedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> securityTokenValidatedNotification)
        {
            var oldIdentity = securityTokenValidatedNotification.AuthenticationTicket.Identity;

            // we want to keep first name, last name, subject and roles
            var givenName = oldIdentity.FindFirst("given_name");
            var familyName = oldIdentity.FindFirst("family_name");
            var sub = oldIdentity.FindFirst("sub");
            var roles = oldIdentity.FindAll("role");

            // create new identity and set name and role claim type
            var newIdentity = new ClaimsIdentity(oldIdentity.AuthenticationType, "given_name", "role");

            newIdentity.AddClaim(givenName);
            newIdentity.AddClaim(familyName);
            newIdentity.AddClaim(sub);
            newIdentity.AddClaims(roles);

            // add some other app specific claim
            newIdentity.AddClaim(new Claim("app_specific", "some data"));

            securityTokenValidatedNotification.AuthenticationTicket = new AuthenticationTicket(
                newIdentity,
                securityTokenValidatedNotification.AuthenticationTicket.Properties);

            return Task.FromResult(0);
        }
    }
}
