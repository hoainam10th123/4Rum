using ForumAppCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var roles = new List<AppRole>
            {
                new AppRole{Id = Guid.NewGuid(), Name = "Admin"},
                new AppRole{Id = Guid.NewGuid(), Name = "HR"},
                new AppRole{Id = Guid.NewGuid(), Name = "User"}
            };

            if (!await roleManager.Roles.AnyAsync())
            {
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            var users = new List<AppUser> {
                new AppUser { UserName = "hoainam10th", DisplayName = "Nguyễn Hoài Nam", DateOfBirth = DateTime.Parse("2/17/1991 12:15:12 PM")},
                new AppUser{ UserName="ubuntu", DisplayName = "Ubuntu Nguyễn", DateOfBirth = DateTime.Parse("2/17/1990 12:15:12 PM")},
                new AppUser{UserName="lisa", DisplayName = "Lisa", DateOfBirth = DateTime.Parse("2/17/2000 12:15:12 PM")}
            };

            foreach (var user in users)
            {
                //user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "Hoainam10th");
                await userManager.AddToRoleAsync(user, "User");
            }

            var admin = new AppUser { UserName = "admin", DisplayName = "Administrator", DateOfBirth = DateTime.Parse("2/17/1991 12:15:12 PM") };
            await userManager.CreateAsync(admin, "Hoainam10th");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "User", "HR" });
        }
    }

    public class DataSeedLaguage
    {
        public static async Task SeedTagLaguageAsync(DataContext context)
        {
            if (!await context.TagsLanguages.AnyAsync())
            {
                var countries = new List<TagsLanguage>
            {
                new TagsLanguage {Name = "C#" },
                new TagsLanguage {Name = "C++" },
                new TagsLanguage {Name = "C" },
                new TagsLanguage {Name = "Dart" },
                new TagsLanguage { Name = "Flutter" },
                new TagsLanguage {Name = "Angular" },
                new TagsLanguage {Name = "TypeScript" },
                new TagsLanguage {Name = "ReactJs" },
                new TagsLanguage {Name = "VueJs" },
                new TagsLanguage {Name = "html" },
                new TagsLanguage {Name = "css" },
                new TagsLanguage {Name = "scss" },
                new TagsLanguage {Name = "css" },
                new TagsLanguage {Name = "javascript" },
                new TagsLanguage { Name = "java" },
                new TagsLanguage { Name = "Sql" },
                new TagsLanguage { Name = "Swift" },
                new TagsLanguage { Name = "kotlin" },
                new TagsLanguage { Name = "Android" },
                new TagsLanguage { Name = "IOS" },
                new TagsLanguage { Name = "ReactNative" }
            };
                context.AddRange(countries);
                await context.SaveChangesAsync();
            }
        }
    }
}
