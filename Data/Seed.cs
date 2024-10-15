using Microsoft.AspNetCore.Identity;
using Shafeh.Models;

namespace Shafeh.Data;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[] { "Admin", "Member", "KolelAdmin", "KolelMember" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ShafehContext>();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                var email = configuration["AdminDefault:Email"];
                var password = configuration["AdminDefault:Password"];

                if (email == null) throw new Exception("Admin Email not found");

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    if (password == null) throw new Exception("Admin password not found");

                    var user = new IdentityUser
                    {
                        UserName = email,
                        Email = email,
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(user, password);

                    var person = new Person() {
                        Name = "Admin",
                        UserId = user.Id,
                    };

                    context.Person.Add(person);
                    await context.SaveChangesAsync();

                    await userManager.AddToRoleAsync(user, "Admin");

                    transaction.Commit();
                }
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
