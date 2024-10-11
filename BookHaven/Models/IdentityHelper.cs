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

        public static async Task CreateDefaultUser(IServiceProvider provider, string role)
        {
            var userManager = provider.GetService<UserManager<IdentityUser>>();

            // If no users are present, make the default user
            int numUsers = (await userManager.GetUsersInRoleAsync(role)).Count();
            if (numUsers == 0) // If no users are in a specified role
            {
                var defaultUser = new IdentityUser()
                {
                    Email = "admin@bookhaven.com",
                    UserName = "Admin"
                };

                await userManager.CreateAsync(defaultUser, "Admin1@BookHaven");

                await userManager.AddToRoleAsync(defaultUser, role);
            }
        }
    }
}
