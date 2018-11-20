using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeChatRepository
{
    public class SeedDb
    {
        public static void Initialize(IServiceProvider serviceProvider) {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                AppUser user = new AppUser()
                {
                    //Name = "Administrator",
                    Email = "admin@mechat.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "admin"
                };

                var result = userManager.CreateAsync(user, "admin@123");
                context.SaveChanges();
            }
        }
    }
}
