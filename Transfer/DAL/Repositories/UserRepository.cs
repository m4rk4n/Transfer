using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.DAL.Repositories.Interfaces;
using Transfer.Models;

namespace Transfer.DAL.Repositories
{
    // is not really used, tried to use identity through all layers, couldn't do it
    public class UserRepository : GenericRepository<TransferUser>, IUserRepository
    {
        private readonly UserManager<TransferUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserRepository(TransferContext context)
                      : base(context)
        {
            this.Context = context ?? throw new ArgumentNullException("DbContext cannot be null.");
        }

        public UserRepository(TransferContext context,
                                UserManager<TransferUser> userManager,
                                RoleManager<IdentityRole> roleManager)  : base (context)
        {
            this.Context = context ?? throw new ArgumentNullException("DbContext cannot be null.");
            this.userManager = userManager;
            this.roleManager = roleManager;
        }


        public async Task CreateAsync(TransferUser user)
        {
            if (user != null)
            {
                this.Context.Users.Add(user);
                await this.Context.SaveChangesAsync();
            }
        }

        //public async Task<bool> CreateUser(RegisterViewModel model)
        //{
        //    var user = new TransferUser
        //    {
        //        Name = model.Name,
        //        Email = model.Email,
        //        UserName = model.Username
        //    };

        //    var result = await userManager.CreateAsync(user, model.Password); // await needed
        //    if (result.Succeeded)
        //        return true;
        //    return false;
        //}



        //public async Task<bool> CreateRole(string role)
        //{

        //    bool x = await roleManager.RoleExistsAsync(role);
        //    if (!x)
        //    {
        //        var newRole = new IdentityRole();
        //        newRole.Name = role.ToString();
        //        var result = await roleManager.CreateAsync(newRole);

        //        if (result.Succeeded)
        //            return true;
        //    }
        //    return false; // creation of new role failed
        //}

    }
}
