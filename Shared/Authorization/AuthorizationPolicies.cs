using Microsoft.Extensions.DependencyInjection;

namespace Shared.Authorization;

public static class AuthorizationPolicies
{

    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("Owner", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "Owner"));

            options.AddPolicy("Company", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "Company"));
            options.AddPolicy("User", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "User"));
        });
    }
    
}