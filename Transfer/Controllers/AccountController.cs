using Transfer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Transfer.ViewModels;

namespace Transfer.Controllers
{
     [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> logger;
        private readonly SignInManager<TransferUser> signInManager;
        private readonly UserManager<TransferUser> userManager;

        private readonly IConfiguration config;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(ILogger<AccountController> logger,
            SignInManager<TransferUser> signInManager,
            UserManager<TransferUser> userManager,
            IConfiguration config,
            RoleManager<IdentityRole> roleManager)
        {
            this.logger = logger;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.config = config;
            this.roleManager = roleManager;
        }

        [HttpPost]
        // public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        public async Task<IActionResult> Post([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Username);

                if (user != null)
                {
                    var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false); // don't wanna sign them in with cookie

                    if (result.Succeeded)
                    {
                        // Create the token
                        var claims = new List<Claim>
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email), //subject of the jwt
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //prevent from being replayed
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName) //Provides a human readable value that identifies the subject of the token. This value is not guaranteed to be unique within a tenant and is designed to be used only for display purposes
                        };

                        var userClaims = await userManager.GetClaimsAsync(user);
                        var userRoles = await userManager.GetRolesAsync(user);
                        claims.AddRange(userClaims); 

                        foreach( var userRole in userRoles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, userRole));
                            var role = await roleManager.FindByNameAsync(userRole);
                            if(role != null)
                            {
                                var roleClaims = await roleManager.GetClaimsAsync(role);
                                foreach (Claim roleClaim in roleClaims)
                                {
                                    claims.Add(roleClaim);
                                }
                            }
                        }

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                           issuer: config["Tokens:Issuer"],
                           audience: config["Tokens:Audience"],
                           claims: claims,
                           expires: DateTime.UtcNow.AddMinutes(30),
                           signingCredentials: creds
                            );

                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return Created("", results);
                    }
                }
            }
            return BadRequest();
        }
    }
}
