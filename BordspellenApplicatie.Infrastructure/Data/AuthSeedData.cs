using BordspellenApplicatie.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BordspellenApplicatie.Infrastructure.Data;
public class AuthSeedData(ILogger<AuthSeedData> logger, RoleManager<IdentityRole> roleManager)
{
    private readonly ILogger<AuthSeedData> logger = logger;
    private readonly RoleManager<IdentityRole> roleManager = roleManager;

    public async Task SeedDatabase()
    {

        if (!roleManager.Roles.Any())
        {
            logger.LogInformation("Preparing to add identity roles");

            foreach (var val in Enum.GetValues<UserRole>())
            {
                await roleManager.CreateAsync(new IdentityRole(val.ToString()));
            }


            logger.LogInformation("Roles added");
        }
        else
        {
            logger.LogInformation("Roles already present");
        }
    }
}