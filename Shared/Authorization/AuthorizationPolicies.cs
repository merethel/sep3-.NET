using Microsoft.Extensions.DependencyInjection;

namespace Shared.Authorization;

public static class AuthorizationPolicies
{

    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("Owner", policy => policy.RequireClaim("Role", "Owner"));

            options.AddPolicy("Company", policy => policy.RequireClaim("Role", "Company"));
            options.AddPolicy("User", policy => policy.RequireClaim("Role", "User"));
            options.AddPolicy("CompAndUse", policy => policy.RequireClaim("Role", "User", "Company"));
        });
    }
}