using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System.IdentityModel.Tokens;

namespace AzureSocialLoginB2C
{
    public partial class Startup
    {
        public static string ClientId = ConfigurationManager.AppSettings["ida:ClientId"];
        public static string ClientSecret = ConfigurationManager.AppSettings["ida:ClientSecret"];
        public static string AadInstance = ConfigurationManager.AppSettings["ida:AadInstance"];
        public static string Tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        public static string RedirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];

        // B2C policy identifiers
        public static string SignUpSignInPolicyId = ConfigurationManager.AppSettings["ida:SignUpSignInPolicyId"];

        public static string DefaultPolicy = SignUpSignInPolicyId;

        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    MetadataAddress = String.Format(AadInstance, Tenant, DefaultPolicy),
                    ClientId = ClientId,
                    RedirectUri = RedirectUri,
                    PostLogoutRedirectUri = RedirectUri,

                    TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name"
                    },

                });
        }
    }
}
