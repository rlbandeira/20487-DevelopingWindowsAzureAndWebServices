using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueYonder.Companion.Client.Helpers
{
    internal static class AuthorizationHelper
    {
        public static IUser GetUserByPolicy(IEnumerable<IUser> users, string policy)
        {
            foreach (var user in users)
            {
                string userIdentifier = Base64UrlDecode(user.Identifier.Split('.')[0]);
                if (userIdentifier.EndsWith(policy.ToLower())) return user;
            }

            return null;
        }

        public static string Base64UrlDecode(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(s);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
            return decoded;
        }

        public static async Task<AuthenticationResult> GetToken()
        {
            AuthenticationResult authResult = null;
            try
            {
                if(UserAuth.Instance.IsAutharized)
                    authResult = await App.PublicClientApp.AcquireTokenSilentAsync(App.ApiScopes, GetUserByPolicy(App.PublicClientApp.Users, App.PolicySignUpSignIn), App.Authority, false);
                else
                    authResult = await App.PublicClientApp.AcquireTokenAsync(App.ApiScopes, AuthorizationHelper.GetUserByPolicy(App.PublicClientApp.Users, App.PolicySignUpSignIn), UIBehavior.SelectAccount, string.Empty, null, App.Authority);

            }
            catch (Exception ex)
            {

            }

            if (authResult != null)
            {
                return authResult;
            }

            return null;
        }

    }
}
