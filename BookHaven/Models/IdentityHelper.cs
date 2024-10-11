#nullable disable
using Microsoft.AspNetCore.Identity;

namespace BookHaven.Models
{
    public class IdentityHelper
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static async Task CreateRoles(IServiceProvider provider, params string[] roles)
        {
            RoleManager<IdentityRole> roleManager = provider.GetService<RoleManager<IdentityRole>>();

            foreach (string role in roles)
            {
                bool doesRoleExit = await roleManager.RoleExistsAsync(role);
                if (!doesRoleExit)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
