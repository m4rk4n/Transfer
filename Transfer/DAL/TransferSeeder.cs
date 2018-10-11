using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.Models;

namespace Transfer.DAL
{
    public class TransferSeeder
    {
        private readonly TransferContext ctx;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<TransferUser> userManager;

        public TransferSeeder(TransferContext ctx,
                                RoleManager<IdentityRole> roleManager,
                                UserManager<TransferUser> userManager)
        {
            this.ctx = ctx;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task Seed()
        {
            ctx.Database.EnsureCreated();


            if (ctx.Vehicles.Count() == 0)
            {
                ctx.Vehicles.Add(
                    new Vehicle
                    {
                        Name = "Auto1",
                        Note = "poyy",
                        RegistrationDate = DateTime.Now,
                        PlateNumber = "12321"
                    });
            }

            ctx.SaveChanges();
            if (ctx.Partners.Count() == 0)
            {
                ctx.Partners.Add(
                    new Partner
                    {
                        Name = "Partner Prvi",
                        Phone = "3456787",
                        Email = "neki@mail.com",
                        VehicleId = 1
                    });
            }


            ctx.SaveChanges();

            bool x = await roleManager.RoleExistsAsync("Admin");
            if (!x)
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                await roleManager.CreateAsync(role);
            }

            ctx.SaveChanges();

            var user = await userManager.FindByEmailAsync("admin@admin.com");

            if (user == null)
            {
                user = new TransferUser()
                {
                    Name = "Admin",
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com"
                };
                var result = await userManager.CreateAsync(user, "P@ssw0rd!");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed To create default admin user");
                }
                else if (result.Succeeded)
                {
                    var result1 = await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            // user2 has no roles
            var user2 = await userManager.FindByEmailAsync("test2@test2.com");
            if (user2 == null)
            {
                user2 = new TransferUser()
                {
                    Name = "Test2",
                    UserName = "test2@test2.com",
                    Email = "test2@test2.com"
                };
                var result = await userManager.CreateAsync(user2, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create test user");
                }
            }

            ctx.SaveChanges();
        }
    }
}
