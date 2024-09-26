using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.Models;
using SarreCarterLab1.Data;
using SarreCarterLab1.Models;

namespace Assignment1.Data
{
    public static class DbInitializer
    {
        public static AppSecrets appSecrets { get; set; }
        public static async Task<int> SeedUsersAndRoles(IServiceProvider serviceProvider)
        {
            // create the database if it doesn't exist
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Check if roles already exist and exit if there are
            if (roleManager.Roles.Count() > 0)
            {
                Console.WriteLine("Roles already exist.");
                return 1;
            }
                
            // Seed roles
            int result = await SeedRoles(roleManager);
            if (result != 0)
            {
                Console.WriteLine("Error seeding roles. Error: " + result);
                return 2;
            }

            // Check if users already exist and exit if there are
            if (userManager.Users.Count() > 0)
            {
                Console.WriteLine("Users already exist. Error: ");
                return 3;
            }

            // Seed users
            result = await SeedUsers(userManager);
            if (result != 0)
            {
                Console.WriteLine("Error seeding users. Error: " + result);
                return 4;
            }

            return 0;
        }

        private static async Task<int> SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // Create Admin Role
            var result = await roleManager.CreateAsync(new IdentityRole("Manager"));
            if (!result.Succeeded)
            {
                Console.WriteLine("Error creating admin role. Error: " + result);
                return 1;
            }

            // Create Member Role
            result = await roleManager.CreateAsync(new IdentityRole("Employee"));
            if (!result.Succeeded)
            {
                Console.WriteLine("Error creating member role. Error: " + result);
                return 2;
            }

            return 0;
        }

        private static async Task<int> SeedUsers(UserManager<ApplicationUser> userManager)
        {
            // Create Manager User
            var managerUser = new ApplicationUser
            {
                UserName = "the.manager@mohawkcollege.ca",
                Email = "the.manager@mohawkcollege.ca",
                FirstName = "The",
                LastName = "Manager",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(managerUser, appSecrets.ManagerPassword);
            if (!result.Succeeded)
            {
                Console.WriteLine("Error creating manager user. Error: " + result);
                return 1;
            }

            // Assign user to Manager role
            result = await userManager.AddToRoleAsync(managerUser, "Manager");
            if (!result.Succeeded)
            {
                Console.WriteLine("Error assigning managaer role to manager user. Error: " + result);
                return 2;
            }

            // Create Employee User
            var employeeUser = new ApplicationUser
            {
                UserName = "the.employee@mohawkcollege.ca",
                Email = "the.employee@mohawkcollege.ca",
                FirstName = "The",
                LastName = "Employee",
                EmailConfirmed = true
            };
            result = await userManager.CreateAsync(employeeUser, appSecrets.EmployeePassword);
            if (!result.Succeeded)
            {
                Console.WriteLine("Error creating employee user. Error: " + result);
                return 3;
            }

            // Assign user to Employee role
            result = await userManager.AddToRoleAsync(employeeUser, "Employee");
            if (!result.Succeeded)
            {
                Console.WriteLine("Error assigning employee role to employee user. Error: " + result);
                return 4;
            }

            return 0;
        }
    }
}
