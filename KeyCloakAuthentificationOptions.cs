namespace WorkshopManagementServiceBackend
{
    using Microsoft.AspNetCore.Authentication;

    public class KeyCloakAuthentificationOptions :
        AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "Bearer";
        public string Scheme => DefaultScheme;
        public string TokenHeaderName { get; set; } = "Authorization";
        public string KeyCloakServerUrl { get; set; } = "http://justinrauch.myftp.org:8480";
        public string Realm { get; set; } = "WMS";

        public string ClientId { get; set; } = "workshop_client";
        public string ClientSecret { get; set; } = "Ip7GUqM8mRuIHMcq3tOuuHCaejSwSk3S";
    }
}
