using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.Models;
using Transfer.ViewModels;

namespace Transfer.Controllers
{
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        private readonly SignInManager<TransferUser> signInManager;
        private readonly UserManager<TransferUser> userManager;

        public RegisterController(
            SignInManager<TransferUser> signInManager,
            UserManager<TransferUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new TransferUser
                {
                    UserName = model.userName,
                    Name = model.firstName + ' ' + model.lastName,
                    PhoneNumber = model.phoneNumber,
                    Email = model.email,
                    Country = model.country
                };
                var result = await userManager.CreateAsync(user, model.password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return Ok(user);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return BadRequest();
        }
    }
}
