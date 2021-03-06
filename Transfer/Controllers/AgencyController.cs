﻿using AutoMapper;
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
using Transfer.BLL.Interfaces;
using Transfer.DAL;
using Transfer.Models;
using Transfer.ViewModels;

namespace Transfer.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("api/[controller]")]
    public class AgencyController : Controller
    {
        private readonly ILogger<PartnerController> logger;
        private readonly IAgencyService service;
        private readonly IMapper mapper;

        public AgencyController(ILogger<PartnerController> logger,
                                IAgencyService service,
                                IMapper mapper)
        {
            this.logger = logger;
            this.service = service;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Agency model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Agency newAgency = new Agency
                    {
                        Name = model.Name,
                        Phone = model.Phone,
                        Email = model.Email,
                        Address = model.Address
                    };

                    var savedAgency = service.Add(newAgency);
                    if (savedAgency != null)
                    {
                        return Created($"/api/agency/{newAgency.Id}", newAgency);
                    }
                    else
                    {
                        return BadRequest(ModelState);
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
        }

        [HttpGet]
        public ActionResult<IEnumerable<AgencyWithIdViewModel>> Get()
        {
            var agencies = service.GetAll();
            if (agencies != null)
            {
                return Ok(mapper.Map<IEnumerable<Agency>, IEnumerable<AgencyWithIdViewModel>>(agencies));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<AgencyWithIdViewModel> Get(int id)
        {
            var partner = service.GetById(id);
            if (partner != null)
            {
                return Ok(mapper.Map<Agency, AgencyWithIdViewModel>(partner));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] AgencyUpdateViewModel model)
        {
            // if it fails, update join table manually
            var updatedAgency = service.Update(model, model.Agency.Id); // disposed context
            if (updatedAgency != null )
            {
                return Ok(updatedAgency);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            service.Remove(id);
            return NoContent(); // kako ovdje provjeru 
        }
    }
}
