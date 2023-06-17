using IntegriraniSistemiH1.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace IntegriraniSistemiH1.Data
{
    public class ApplicataionDbContextSeeder
    {
        public static void Seed(IServiceProvider applicationServices)
        {
            using (IServiceScope serviceScope = applicationServices.CreateScope())
            {
                ApplicationDbContext context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                if (context.Database.EnsureCreated())
                {
                    PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();

                    ApplicationUser admin = new ApplicationUser()
                    {
                        Id = Guid.NewGuid().ToString("D"),
                        Email = "admin@test.test",
                        NormalizedEmail = "admin@test.test".ToUpper(),
                        EmailConfirmed = true,
                        UserName = "admin@test.test",
                        NormalizedUserName = "admin@test.test".ToUpper(),
                        SecurityStamp = Guid.NewGuid().ToString("D")
                    };

                    admin.PasswordHash = hasher.HashPassword(admin, "adminpass");

                    IdentityRole identityRoleAdmin = new IdentityRole()
                    {
                        Id = Guid.NewGuid().ToString("D"),
                        Name = "Admin",
                        NormalizedName = "Admin".ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString("D")

                    };

                    IdentityUserRole<string> identityUserRoleAdmin = new IdentityUserRole<string>()
                    {
                        RoleId = identityRoleAdmin.Id,
                        UserId = admin.Id
                    };

                    context.Roles.Add(identityRoleAdmin);
                    context.Users.Add(admin);
                    context.UserRoles.Add(identityUserRoleAdmin);
                    context.SaveChanges();
                }
            }
        }
    }
}
