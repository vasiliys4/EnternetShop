using Microsoft.AspNetCore.Identity;

namespace EnternetShop.Models.Identity
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<UserForDB> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new UserForDB { UserName = "admin@gmail.com", Email = "admin@gmail.com" };
            if ((await userManager.FindByEmailAsync("admin@gmail.com")) == null)
            {
                await userManager.CreateAsync(defaultUser, "_Aa123456");
                var roleName = "Admin";
                await roleManager.CreateAsync(new IdentityRole(roleName));
                await userManager.AddToRoleAsync(defaultUser, roleName);
            }
        }
    }
}
