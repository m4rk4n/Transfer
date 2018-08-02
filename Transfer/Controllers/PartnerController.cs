using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.DAL;
using Transfer.Models;

namespace Transfer.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("api/[controller]")]
    public class PartnerController : Controller
    {
        private readonly ILogger<PartnerController> logger;
        private readonly SignInManager<TransferUser> signInManager;
        private readonly UserManager<TransferUser> userManager;
        private readonly IConfiguration config;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly TransferContext ctx;

        public PartnerController(ILogger<PartnerController> logger,
            SignInManager<TransferUser> signInManager,
            UserManager<TransferUser> userManager,
            IConfiguration config,
            RoleManager<IdentityRole> roleManager,
            TransferContext ctx)
        {
            this.logger = logger;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.config = config;
            this.roleManager = roleManager;
            this.ctx = ctx;
        }

        [HttpPost]
        // [Authorize]
        public async Task<IActionResult> Post([FromBody]Partner model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // mapper!!!
                    Partner newPartner = new Partner
                    {
                        Name = model.Name,
                        Phone = model.Phone,
                        Email = model.Email
                    };

                    

                    var currentUser = await userManager.FindByNameAsync(User.Identity.Name); // hmm, debug through this one


                    ctx.Partners.Add(newPartner);
                    if (ctx.SaveChanges() > 0)
                    {
                        // kad udje, nece imat id, al ce mu kontext dodjelit id nakon Add()
                        //   return Created($"/api/orders/{newOrder.Id}", mapper.Map<Order, OrderViewModel>(newOrder));
                        // HTTP response 201 when you create a new object

                        return Created($"/api/partner/{newPartner.Id}", newPartner);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to save new partner: {ex}");
                return BadRequest($"Failed to save new partner beacuse of exception, end user better not see this one :)");
            }
            logger.LogError($"Failed to save new partner: Not an excception");
            return BadRequest("Failed to save new partner, not an exception");
        }
    }
}
