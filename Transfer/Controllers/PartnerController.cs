using AutoMapper;
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
using Transfer.BLL;
using Transfer.BLL.Interfaces;
using Transfer.DAL;
using Transfer.Models;
using Transfer.ViewModels;

namespace Transfer.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("api/[controller]")]
    public class PartnerController : Controller
    {
        private readonly ILogger<PartnerController> logger;
        private readonly IPartnerService service;
        private readonly IMapper mapper;

        public PartnerController(ILogger<PartnerController> logger,
            IPartnerService service,
            IMapper mapper)
        {
            this.logger = logger;
            this.service = service;
            this.mapper = mapper;
        }

        [HttpPost]
        // [Authorize]
        public async Task<IActionResult> Post([FromBody]PartnerAddViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   // var newPartner = mapper.Map<PartnerAddViewModel, Partner>(model);

                    var savedPartner = service.Add(model.Partner, model.VehicleId); //AddAsync ?

                    if (savedPartner != null)
                    {
                        return Created($"/api/partner/{savedPartner.Id}", savedPartner);
                    }
                    else
                    {
                        logger.LogError("savedPartner was null");
                        return BadRequest(ModelState);
                    }
                }
                else
                {
                    logger.LogError("ModelState is not valid");
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to save new partner: {ex}");
                return BadRequest($"Failed to save new partner beacuse of exception ");
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<PartnerViewModel> Get(int id)
        {
            var partner = service.GetById(id);
            if (partner != null)
            {
                return Ok(mapper.Map<Partner, PartnerViewModel>(partner));
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<PartnerViewModel>> Get()
        {
            var partners = service.GetAll();
            if (partners != null)
            {
                return Ok(mapper.Map<IEnumerable<Partner>, IEnumerable<PartnerViewModel>>(partners));
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<PartnerViewModel> Put(int id, [FromBody] PartnerUpdateViewModel model)
        {
           // var partner = mapper.Map<Partner>(model.Partner);
            var updatedPartner = service.Update(model, model.Partner.Id);
            if (updatedPartner != null)
            {
                logger.LogInformation("partner updated");
                return Ok(updatedPartner);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            //var partner = service.GetById(id);
            service.Remove(id);
            return Ok(); // kako ovdje provjeru 
        }
    }
}
