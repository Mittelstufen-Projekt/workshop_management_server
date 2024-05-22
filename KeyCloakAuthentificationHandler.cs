using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using WorkshopManagementServiceBackend.Models;

namespace WorkshopManagementServiceBackend
{
    public class KeyCloakAuthentificationHandler :
        AuthenticationHandler<KeyCloakAuthentificationOptions>
    {
        public KeyCloakAuthentificationHandler
            (IOptionsMonitor<KeyCloakAuthentificationOptions> options,
            ILoggerFactory logger, UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        { }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //check header first
            if (!Request.Headers
                .ContainsKey(Options.TokenHeaderName))
            {
                return AuthenticateResult.Fail($"Missing header: {Options.TokenHeaderName}");
            }

            //get the header and validate
            string token = Request
                .Headers[Options.TokenHeaderName]!;

            

            var isValid = await ValidateTokenWithKeyCloak(token);
            if (!isValid)
            {
                return AuthenticateResult.Fail("Invalid token");
            }

            var claims = new List<Claim>{};
            var identity = new ClaimsIdentity(claims, Options.Scheme);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Options.Scheme);

            return AuthenticateResult.Success(ticket);
        }
        private async Task<bool> ValidateTokenWithKeyCloak(string token)
        {
            using (var httpClient = new HttpClient())
            {
                using var client = new HttpClient();

                var requestUri = $"{Options.KeyCloakServerUrl}/realms/{Options.Realm}/protocol/openid-connect/token/introspect";
                if (token.StartsWith("Bearer "))
                {
                    token = token.Substring(7);
                }

                var formData = new Dictionary<string, string>
                {
                    { "client_id", Options.ClientId },
                    { "client_secret", Options.ClientSecret },
                    { "token", token }
                };

                using var content = new FormUrlEncodedContent(formData);

                var response = await client.PostAsync(requestUri, content);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonDocument = JsonDocument.Parse(responseContent);
                var active = jsonDocument.RootElement.GetProperty("active");
                var activeTest = active.GetBoolean();
                Console.Write("Done");
                return activeTest;
            }
        }
    }

}
