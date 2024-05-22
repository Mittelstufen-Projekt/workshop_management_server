using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System;

namespace WorkshopManagementServiceBackend.Extensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddKeycloakAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication()
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = Convert.ToBoolean($"{configuration["Keycloak:require-https"]}");
                    x.MetadataAddress = $"{configuration["Keycloak:server-url"]}realms /{configuration["Keycloak:realm"]}/ protocol / openid - connect / token / introspect";
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        RoleClaimType = "groups",
                        NameClaimType = $"{configuration["Keycloak:name_claim"]}",
                        ValidAudience = $"{configuration["Keycloak:audience"]}",
                        ValidateIssuer = Convert.ToBoolean($"{configuration["Keycloak:validate-issuer"]}"),
                    };
                });

            services.AddAuthorization(o =>
            {
                o.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            return services;
        }
    }
}
